using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillManager.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillManager.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IUsersService userService;
        private readonly ILogger logger;
        public UserController(IUsersService UserService, ILogger<UserController> logger)
        {
            this.userService = UserService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/api/information/getAll")]
        public IActionResult GetAllUsers()
        {
            logger.LogInformation("GetAllUsers controller");
            return Ok(userService.GetAllUsers());
        }

        [HttpPut]
        [Route("/api/information/edit")]
        public IActionResult EditUser([FromBody]UserDTO userDTO)
        {
            logger.LogInformation("EditUser controller");
            return Ok(userService.EditUser(userDTO));
        }

    }


}
