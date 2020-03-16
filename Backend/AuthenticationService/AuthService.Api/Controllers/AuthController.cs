using System.Threading.Tasks;
using AuthService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/Auth")]
    public class AuthController : BaseController
    {
        private readonly Services.AuthService _authService;

        public AuthController(Services.AuthService authService)
        {
            _authService = authService;
        }
        
        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequestModel model)
        {
            var token = await _authService.SignInAsync(model.Login, model.Password);
            return JsonResult(token);
        }

        [Route("SignOut")]
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] SignOutRequestModel model)
        {
            var token = await _authService.SignOutAsync(model.Email, model.FirstName, model.LastName, model.Age,
                model.Password);
            return JsonResult(token);
        }
    }
}
