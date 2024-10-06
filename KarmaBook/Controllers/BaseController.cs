using KarmaBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
      /* protected IActionResult CreateResponse<T>(bool success, int statusCode, string message, T data, object pagination)
        {
            var response = new ResponseResult<T>(success, statusCode, message, data, pagination);
            return StatusCode(statusCode,data);
        }

        protected IActionResult Success<T>(T data,string message="Operation Successful",object pagination=null, int statusCode = 200)
        {
            var response = CreateResponse(true, statusCode, message, data, pagination);
            return StatusCode(statusCode, response);
        }

        protected IActionResult Failure<T>(string message="Operation Failed ",object meta=null, int statusCode = 400)
        {
            var response = CreateResponse(false, statusCode, message,default(T),meta);
            return StatusCode(statusCode, response);
        }*/

    }
}
