using System;

using System.Web.Http;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Contracts;
using Microsoft.AspNet.Identity;
using WebApi.Models.Post;
using WebApi.Models.User;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using System.Text;

namespace WebApi.Controllers
{
   
    [Authorize]
    [RoutePrefix("api/post")]
    public class PostsController : ApiController
    {
        private readonly IBLLUnitOfWork _uow;

        public PostsController(IBLLUnitOfWork uow)
        {
            _uow = uow;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("getallposts")]
        public async Task<IHttpActionResult> GetAllPost()
        {

            var posts = (await _uow.PostService.GetAllPosts()).ToList();
            posts.RemoveAll(x => x.IsBlocked == true);      // remove all Blocked post
            if (posts == null)
                NotFound();  //code 404         
        

          IEnumerable <PostViewModel> Posts = AutoMapper.Mapper.Map<IEnumerable<DTOPost>, List<PostViewModel>>(posts);            
          return Ok(Posts);
        }

        [HttpGet]
        [Route("{postId}")]
        public async Task<IHttpActionResult> GetPostById(int postId)
        {

            var post = (await _uow.PostService.GetPostByPosId(postId));
            if (post == null)
                NotFound();  //code 404         
            PostViewModel Post_ = AutoMapper.Mapper.Map<DTOPost, PostViewModel>(post);
            return Ok(Post_);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("getbyteg")]
        public async Task<IHttpActionResult> GetPostByTeg()
        {
            var httpRequest = HttpContext.Current.Request;
            string teg = httpRequest.Params["Teg"];

            var posts = (await _uow.PostService.GetPostsByTeg(teg));
            if (posts == null)
                NotFound();  //code 404         
            IEnumerable<PostViewModel> Post_ = AutoMapper.Mapper.Map<IEnumerable<DTOPost>, List<PostViewModel>>(posts);
            return Ok(Post_);
        }

        [HttpGet]
        [Route("getallpostsModer")]
        public async Task<IHttpActionResult> GetAllPostModer()
        {

            var posts = (await _uow.PostService.GetAllPosts()).ToList();
            if (posts == null)
                NotFound();  //code 404         

            IEnumerable<PostViewModel> Posts = AutoMapper.Mapper.Map<IEnumerable<DTOPost>, List<PostViewModel>>(posts);
            return Ok(Posts);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("getalltegs")]
        public async Task<IHttpActionResult> GetAllTegs()
        {
            var tegs = await _uow.PostService.GetAllTeg();
            if (tegs == null)
                NotFound();  //code 404 
            
            return Ok(tegs);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("image/get")]
        public IHttpActionResult GetImageForPost([FromUri]string imageName, [FromUri] string userLogin)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK); //made HttpResponseMessage 

            var path = "~/Images/" + userLogin + "/posts/" + imageName; //made new path to local server storage for images for post
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/" + userLogin + "/posts/")))
                return null;

            path = HttpContext.Current.Server.MapPath(path); // add path
            var contents = File.ReadAllBytes(path);  // convert required file into bytes -read image from server storage

            var ms = new MemoryStream(contents); // create an instance of MemoryStream by passing the bytes
            result.Content = new StreamContent(ms); // add the MemoryStream object to the HttpResponseMessage Content

            //file type which we trusted - we have to use application/octet-stream
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            var response = ResponseMessage(result);
            return response;

            //www.c-sharpcorner.com/article/sending-files-from-web-api/
        }



        
        [HttpPost]

        [Route("add")]
        public async Task<IHttpActionResult> AddPost()
        {
            var authtor = await _uow.UserInfoService.GetUserById(User.Identity.GetUserId<int>());
            if (authtor == null)
                return this.Unauthorized();

            if (authtor.IsBlocked)
                return BadRequest("Your account blocked.");

            var httpRequest = HttpContext.Current.Request;  //get request object
            HttpPostedFile postedFile = httpRequest.Files["PostPicture"];  //Gets the collection of files uploaded by the client, in multipart MIME format.
            if (postedFile == null || postedFile.ContentLength < 2)
                return BadRequest("No image");

            string filename = AddImage(postedFile);  //add image for post     

            string postBodyCode = httpRequest.Params["Post"];
            string postBody = Base64Decode(postBodyCode); //decoding string with html tag

            DTOPost pos = new DTOPost
            {
                PostBody = postBody,
                Hashtags = httpRequest.Params["Hashtags"],
                PostTitle = httpRequest.Params["Title"],
                DateCreate = DateTime.Now,
                UserInfoId = authtor.Id,
                PostPicture = filename

            };

            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            await _uow.PostService.AddPost(pos);
            return Content(HttpStatusCode.Created, "Post added");
        }

        [HttpPut]
        [Route("{postId}")]
        public async Task<IHttpActionResult> EditPost([FromUri]int postId, [FromBody] PostEditViewModel newPost)
        {
            var authtor = await _uow.UserInfoService.GetUserById(User.Identity.GetUserId<int>());
            if (authtor == null)
                return this.Unauthorized();

            if (authtor.IsBlocked)
                return BadRequest("Your account blocked.");

            DTOPost post = await _uow.PostService.GetPostByPosId(postId);
            if (post == null)
                return NotFound();

            if (post.UserInfo.Id != authtor.Id)
                return BadRequest("It is not your post.");

            if (post.IsBlocked)
                return BadRequest("Post blocked.");

            post.PostBody = newPost.PostBody;
            post.Hashtags = newPost.Hashtags;
            post.LastEdit = DateTime.Now;
            post.UserInfo = null;
            post.Comments = null;

            await _uow.PostService.EditPost(post);
            return Ok("Post edited");
        }

        [HttpDelete]
        [Route("{postId}")]
        public async Task<IHttpActionResult> DeletePost([FromUri]int postId)
        {
            var authtor = await _uow.UserInfoService.GetUserById(User.Identity.GetUserId<int>());
            if (authtor == null)
                return this.Unauthorized();

            if (authtor.IsBlocked)
                return BadRequest("Your account blocked.");

            DTOPost post = await _uow.PostService.GetPostByPosId(postId);
            if (post == null)
                return NotFound();

            if (post.UserInfo.Id != authtor.Id)
                return BadRequest("It is not your post.");

            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);

            await _uow.PostService.DeletePost(postId);
            return Ok("Post removed");
        }

        private string AddImage(HttpPostedFile image)
        {
            string userLogin = User.Identity.GetUserName();

            string imageName = "";

            imageName = new string(Path.GetFileNameWithoutExtension(image.FileName).Take(5).ToArray()); //made image name 
            imageName = imageName + Path.GetExtension(image.FileName);

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Images/" + userLogin + "/posts"))) //made directory  for user post image on webapi/Images/user/posts
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Images/" + userLogin + "/posts"));

            var filePath = HttpContext.Current.Server.MapPath("~/Images/" + userLogin + "/posts/" + imageName); // made new path to local server storage
            image.SaveAs(filePath);

            return imageName;

        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
