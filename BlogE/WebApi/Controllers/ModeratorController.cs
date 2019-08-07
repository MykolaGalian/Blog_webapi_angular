using System;
using System.Web.Http;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Contracts;
using Microsoft.AspNet.Identity;

namespace WebApi.Controllers
{
    [Authorize(Roles = "moderator,admin")]
    [RoutePrefix("api/moder")]    
    public class ModeratorController : ApiController
    {
        private readonly IBLLUnitOfWork _uow;

        public ModeratorController(IBLLUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("block/post/{postId}")]
        public async Task<IHttpActionResult> BlockPost([FromUri]int postId)
        {

            DTOPost post = await _uow.PostService.GetPostByPosId(postId);
            if (post == null)
                return NotFound();

            if (post.IsBlocked)
                return BadRequest("Post already blocked.");

            var userName = User.Identity.GetUserName();

            if (userName == null || !User.IsInRole("admin"))
                return this.Unauthorized();
            if (userName == null || !User.IsInRole("moderator"))
                return this.Unauthorized();

            await _uow.ModeratorService.BlockPost(postId, userName);

            return Ok("Post blocked");
        }

        [HttpGet]
        [Route("unblock/post/{postId}")]
        public async Task<IHttpActionResult> UnblockPost([FromUri]int postId)
        {
            DTOPost post = await _uow.PostService.GetPostByPosId(postId);
            if (post == null)
                return NotFound();

            if (!post.IsBlocked)
                return BadRequest("Post not blocked.");

            var userId = this.User.Identity.GetUserId();
            if (userId == null || !User.IsInRole("admin"))
                return this.Unauthorized();


            await _uow.ModeratorService.UnblockPost(postId);
            return Ok("Post unblocked");
        }
    }
}