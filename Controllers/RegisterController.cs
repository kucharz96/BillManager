using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.Models;
using BillManager.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillManager.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersService _userService;
        private readonly ILogger<RegisterController> _logger;
        public RegisterController(IUsersService UserService, 
            ILogger<RegisterController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)

        {
            this._userService = UserService;
            this._logger = logger;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._userManager = userManager;

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseAfterAutDTO> Login([FromBody]UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Mail, user.Password, false,lockoutOnFailure : true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged.");
                return _userService.GetIdAndRoleForUserById(user.Mail);
            }
            else
            {
                _logger.LogInformation("User logged failed.");
                return new ResponseAfterAutDTO
                {
                    Code = 400,
                    Message = "Login failed",
                    Status = "Failed"
                };
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseAfterAutDTO> Register([FromBody] UserDTO user)
        {
            var newUser = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.Mail,
                PhoneNumber = user.TelNumber,
                IsPaid = false
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    var res = await _roleManager.CreateAsync(role);
                    if (res.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }


                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        var role = new IdentityRole("User");
                        var res = await _roleManager.CreateAsync(role);


                    }
                    await _userManager.AddToRoleAsync(newUser, "User");

                }
                _logger.LogInformation("User created");
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                _logger.LogInformation("User logged.");
                return _userService.GetIdAndRoleForUserById(user.Mail);

            }
            _logger.LogInformation("User created failed");
            return new ResponseAfterAutDTO
            {
                Code = 400,
                Message = "Register failed",
                Status = "Failed"
            };


        }
    }
}
