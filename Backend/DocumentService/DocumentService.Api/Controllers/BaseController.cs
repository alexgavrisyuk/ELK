using DocumentService.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocumentService.Api.Controllers
{
    public class BaseController : Controller
    {
        protected JsonResult JsonResult(object data)
        {
            var response = new Message
            {
                IsSuccess = true,
                Data = JsonConvert.SerializeObject(data,
                    new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()})
            };
            return Json(response);
        }
    }
}