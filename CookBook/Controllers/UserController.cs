using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Model;
using CookBook.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("api/user/verify")]
        [HttpPost]
        public async Task<IActionResult> Verify([FromBody]UserViewModel model)
        {
            //check if verification gone through the firebase
            if (string.IsNullOrEmpty(model.Token)) {
                return Unauthorized();
            }
            string jwt = this.userService.GenerateJwtForUser(model);
            return Ok(new { token = jwt });
        }
    }
}