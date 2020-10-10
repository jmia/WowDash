using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers.Authentication
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UsersController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        // POST api/<UsersController>
        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterPlayer([FromBody] RegisterPlayerRequest request)
        {
            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync("Google", request.UserId, isPersistent: true, bypassTwoFactor: true);   // replace with request details?
            if (result.Succeeded)
            {
                // Logged in with provider
                return "successfully logged in with existing user";
            }
            if (result.IsLockedOut)
            {
                // Locked out
                return "somehow we're locked out idk";
            }
            else
            {
                // If the user does not have an account, then create an account.
                var user = new IdentityUser { UserName = request.Email, Email = request.Email, EmailConfirmed = true };

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    var info = new UserLoginInfo("Google", request.UserId, "Google");
                    createResult = await _userManager.AddLoginAsync(user, info);
                    if (createResult.Succeeded)
                    {
                        // User created an account successfully 
                        var userId = await _userManager.GetUserIdAsync(user);

                        var player = new Player();

                        player.Email = user.Email;
                        player.DisplayName = request.DisplayName;
                        player.GoogleId = request.UserId;
                        player.IdentityUserId = userId;

                        _context.Players.Add(player);

                        _context.SaveChanges();

                        await _signInManager.SignInAsync(user, isPersistent: true, info.LoginProvider);

                        return "y'all we made a whole user i think";
                    }
                }

                return "something went wrong along the way, check logs";

            }

        }

        // POST api/<UsersController>
        [HttpPost("login/{token}")]
        public void LogInPlayer([FromBody] string token)
        {
            //if (ValidateGoogleToken(token))
            //{
            //    var user = _userManager.FindByNameAsync()
            //}
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        internal bool ValidateGoogleToken(string token)
        {
            // TODO: How to validate with Google?
            return true;
        }
    }
}
