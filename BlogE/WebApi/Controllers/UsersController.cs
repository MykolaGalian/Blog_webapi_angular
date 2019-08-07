

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BLL.DTO;
using BLL.Contracts;
using Microsoft.AspNet.Identity;
using WebApi.Models.Post;
using WebApi.Models.User;
using System.IO;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IBLLUnitOfWork _uow;

        public UsersController(IBLLUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IHttpActionResult> MyProfile()
        {       
                        
            var profile = await _uow.UserInfoService.GetUserById(User.Identity.GetUserId<int>());
            var emailData = await _uow.UserManagerService.GetUserByLogin(profile.Login);
            profile.Email = emailData.Email;

            if (profile == null)
                return this.Unauthorized();

            if (profile.IsBlocked)
                return BadRequest("Your account blocked.");

            ProfileViewModel profileView = AutoMapper.Mapper.Map<DTOUser, ProfileViewModel>(profile);
           
            var posts = (await _uow.PostService.GetPostsByUserId(profile.Id)).ToList();
            posts.RemoveAll(x => x.IsBlocked == true);      // remove all Blocked post

            profileView.Posts = AutoMapper.Mapper.Map<IEnumerable<DTOPost>, List<PostViewModel>>(posts);
            profileView.IsAdmin = User.IsInRole("admin");
            profileView.IsModerator = User.IsInRole("moderator");

            return Ok(profileView);
        }

        [HttpGet]
        [Route("allprofiles")]
        public async Task<IHttpActionResult> AllProfiles()
        {

            var profiles = await _uow.UserInfoService.GetAllUserInfo();
            if (profiles == null)
                return this.BadRequest(this.ModelState);
            List<ProfileViewModel> profileViews = AutoMapper.Mapper.Map<List<DTOUser>, List<ProfileViewModel>>(profiles);

            
            return Ok(profileViews);
        }



       
        [HttpPost]
        [Route("avatar/set")]  // set avatar image for user
        public async Task<IHttpActionResult> SetAvatar()
        {
            int userId = User.Identity.GetUserId<int>();
            string userLogin = User.Identity.GetUserName();
            if (userId == 0)
                return BadRequest("Not authorized");

            
            var httpRequest = HttpContext.Current.Request;
            var ImageFromReq = httpRequest.Files["Image"];  //Gets the collection of files uploaded by the client, in multipart MIME format
            if (ImageFromReq == null)
                return BadRequest("No image");

            string imageName = "";
            imageName = new string(Path.GetFileNameWithoutExtension(ImageFromReq.FileName).Take(5).ToArray()); //made image name 
            imageName = imageName + Path.GetExtension(ImageFromReq.FileName); // +.jpg

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/" + userLogin)))  //made directory  for user avatar image on webapi/Images
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + userLogin));

            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + userLogin + "/" + imageName); // made new path to local server storage
            ImageFromReq.SaveAs(filePath);
            await _uow.UserInfoService.SetUserAvatar(userId, imageName);

            return Content(HttpStatusCode.Created, "avatar is set");
        }

        
        [HttpGet]
        [AllowAnonymous]
        [Route("image/get")] // return avatar user pictures
        public IHttpActionResult GetImage([FromUri]string imageName, [FromUri] string userLogin)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);  //made HttpResponseMessage 

            var path = "~/Images/" + userLogin + "/" + imageName; //made new path to local server storage
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/" + userLogin)))
                return null;

            path = HttpContext.Current.Server.MapPath(path);  // add path
            var contents = File.ReadAllBytes(path); // convert required file into bytes -read image from server storage

            var ms = new MemoryStream(contents);  // create an instance of MemoryStream by passing the bytes
            result.Content = new StreamContent(ms);   // add the MemoryStream object to the HttpResponseMessage Content

            //file type which we trusted - we have to use application/octet-stream
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            var response = ResponseMessage(result);
            return response;

            //www.c-sharpcorner.com/article/sending-files-from-web-api/
        }

        [HttpPut]
        [Route("profile/edit")]
        public async Task<IHttpActionResult> EditProfile([FromBody]ChangeProfileViewModel newProfile)
        {
            if (User.Identity.GetUserId() == null)
                return this.Unauthorized();

            int userId = User.Identity.GetUserId<int>();
            if (userId != newProfile.Id)
                return BadRequest("It's not your profile!");
            
            if ((await _uow.UserInfoService.GetUserById(userId)).IsBlocked)
                return BadRequest("Your account has been blocked.");

            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            DTOUser profileEdit = AutoMapper.Mapper.Map<ChangeProfileViewModel, DTOUser>(newProfile);
            
            await _uow.UserInfoService.Update(profileEdit);

            return Ok("Profile has been updated.");
        }

        [HttpPut]
        [Route("profile/password")]
        public async Task<IHttpActionResult> ChangePassword([FromBody]ChangePassViewModel changePassViewModel)
        {
            if (User.Identity.GetUserId() == null)
                return this.Unauthorized();
            DTOUser user = new DTOUser(){Login = User.Identity.GetUserName(), Password = changePassViewModel.OldPassword };

            if (!await _uow.UserManagerService.CheckUserByLoginPas(user))
                return BadRequest("Validation failed.");


            if (changePassViewModel.ConfirmPassword != changePassViewModel.NewPassword)
                ModelState.AddModelError("passwords", "Passwords are not equal");

            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            await _uow.UserManagerService.ChangePassword(User.Identity.GetUserId<int>(), changePassViewModel.OldPassword, changePassViewModel.NewPassword);

            return Ok("Password has been updated.");
        }

        [HttpDelete]
        [Route("profile/edit")]
        public async Task<IHttpActionResult> DeleteProfile()
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (User.Identity.GetUserId() == null)
                return this.Unauthorized();

            await _uow.UserInfoService.Delete(User.Identity.GetUserId<int>());

            return Ok("Profile deleted.");
        }




    }
}