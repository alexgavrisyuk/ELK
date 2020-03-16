using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderService.Api.Infrastructure;

namespace OrderService.Api.Controllers
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