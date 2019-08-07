using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BLL.Contracts;
using BLL.DTO;
using BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebApi.Models.Account;






namespace WebApi.Controllers
{

    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        //using the OWIN context, we get the object IAuthenticationManager 
        private IAuthenticationManager _authenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private readonly IBLLUnitOfWork _uow;

        public AccountsController(IBLLUnitOfWork uow)
        {
            _uow = uow;
        }

  


        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            //cancellation any claims identity associated the the caller
            _authenticationManager.SignOut();
            return Ok(User.Identity.Name + " logged out");
        }

        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (this.User.Identity.GetUserId() != null)
            {
                return this.BadRequest(User.Identity.Name + " already logged in.");
            }

            if (model == null)
            {
                return this.BadRequest("Invalid user data.");
            }            

            if (await _uow.UserInfoService.CheckLoginExist(model.Login))
            {
                ModelState.AddModelError("Login", "Login is already taken.");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            
            DTOUser user = AutoMapper.Mapper.Map<RegisterViewModel, DTOUser>(model);
            user.Roles = new List<string> { "user" };  // only User type
            user.DateRegistration = DateTime.Now;

            OperationDetails operationDetails = await _uow.UserManagerService.Create(user);
            if (!operationDetails.Success)
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                return this.BadRequest(operationDetails.Message);
            }
            else
            {
                return this.Ok(user.Login + " user registered ok.");
            }
        }
    }
   



}
