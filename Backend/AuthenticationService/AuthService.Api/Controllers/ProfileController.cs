using System.Threading.Tasks;
using AuthService.Api.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers
{
    [Route("api/Profile")]
    public class ProfileController : BaseController
    {
        private readonly ProfileService _profileService;

        public ProfileController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfileAsync([FromQuery] string email)
        {
            var user = await _profileService.GetAsync(email);
            return JsonResult(user);
        }

        [HttpPut]
        public async Task<IActionResult> EditProfileAsync([FromBody] EditProfileRequestModel model)
        {
            var user = await _profileService.EditProfileAsync(model.Id, model.Email, model.FirstName, model.LastName,
                model.Age);
            return JsonResult(user);
        }
    }
}