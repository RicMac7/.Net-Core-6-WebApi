using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }
        //private IEnumerable<Users> logins = new List<Users>() {
        //    new Users() {
        //            UserToken = Guid.NewGuid(),
        //                EmailId = "adminakp@gmail.com",
        //                UserName = "Admin",
        //                Password = "Admin",
        //        },
        //        new Users() {
        //            UserToken = Guid.NewGuid(),
        //                EmailId = "adminakp@gmail.com",
        //                UserName = "User1",
        //                Password = "Admin",
        //        }
        //};
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var dbContext = new WebApiContext();

                var usersLogin = dbContext.Users.ToList();

                var Token = new UserTokens();
                var Valid = usersLogin.Any(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = usersLogin.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.EmailId,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        Id = Guid.NewGuid(),
                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"wrong password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            var dbContext = new WebApiContext();

            var usersLogin = dbContext.Users;

            return Ok(usersLogin);
        }
    }
}
