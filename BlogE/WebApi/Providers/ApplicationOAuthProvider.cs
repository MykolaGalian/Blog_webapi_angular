using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BLL.Contracts;
using BLL.DTO;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;


namespace WebApi.Providers
{   
    //ServerProvider for authorization of user
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {        
        private IAuthenticationManager _authenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        
        private readonly IBLLUnitOfWork _unitOfWork;

        public ApplicationOAuthProvider(IBLLUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        //Validates the user object
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
                        
             context.Validated();
        }

        //Checks for the user in DB using the repository and if user is valid initiates “ClaimsIdentity” to generate token.
        // i.e. handling all the token generation
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //GetClaim with BearerTokens
            ClaimsIdentity claim = await _unitOfWork.UserManagerService.GetClaim(context.UserName, context.Password);

            DTOUser user = await _unitOfWork.UserInfoService.GetUserByLogin(context.UserName);

            if (claim == null || user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            claim.AddClaim(new Claim("UserName", user.Login));

            if (await _unitOfWork.UserManagerService.IsUserInRoleAdmin(user.Id))
            {
                claim.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }
            if (await _unitOfWork.UserManagerService.IsUserInRoleModerator(user.Id))
            {
                claim.AddClaim(new Claim(ClaimTypes.Role, "moderator"));
            }


            //cancellation any claims identity associated the the caller
            _authenticationManager.SignOut();

            //grant a claims-based identity (token response) to the recipient of the response 
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true },claim);  // claim with BearerTokens  
            context.Validated(claim);    

        }

    }
}