using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using BLL.DTO;
using BLL.Contracts;
using Microsoft.AspNet.Identity;


namespace WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/admins")]
    public class AdminController : ApiController
    {
        private readonly IBLLUnitOfWork _uow;

        public AdminController(IBLLUnitOfWork uow)
        {
            _uow = uow;
        }       
        

        [HttpGet]
        [Route("block/user/{accountLogin}")]
        public async Task<IHttpActionResult> BlockAccount([FromUri]string accountLogin)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            DTOUser user = await _uow.UserManagerService.GetUserByLogin(accountLogin);
            if (user == null)
                return NotFound();

            if (user.IsBlocked)
                return BadRequest("User already blocked.");

            var userId = this.User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("admin"))
                return this.Unauthorized();

            var admin = await _uow.UserInfoService.GetUserById(User.Identity.GetUserId<int>());   
            if (admin != null  )
                await _uow.AdminService.BlockAccount(user.Id, admin.Login);
            else return BadRequest("Not found ");

            return Ok("Account blocked");
        }


        [HttpGet]
        [Route("unblock/user/{accountLogin}")]
        public async Task<IHttpActionResult> UnblockAccount([FromUri]string accountLogin)
        {
            DTOUser user = await _uow.UserInfoService.GetUserByLogin(accountLogin);
            if (user == null)
                return NotFound();
            if (!user.IsBlocked)
                return BadRequest("User is not blocked.");

            var userId = this.User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("admin"))
                return this.Unauthorized();

            await _uow.AdminService.UnblockAccount(user.Id);
            return Ok("Account unblocked");
        }    
    }
}





   
