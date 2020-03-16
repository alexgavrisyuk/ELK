using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerService.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/Tests")]
    public class TestController: Controller
    {
        [HttpPost]
        [Route("token")]
        public IActionResult Login([FromBody] object requestModel)
        {
            var model = new
            {
                access_token = "token",
                refresh_token = "token",
                expires_in = DateTime.UtcNow.AddDays(2).ToString()
            };

            return Json(model);
        }

        [HttpGet]
        [Route("Users/Info")]
        public IActionResult About()
        {
            var model = new
            {
                roles = "Admin"
            };
            
            return Json(model);
        }
    }
}