using Microsoft.AspNetCore.Mvc;
using TinyMovieShared.API.Result;
using TinyMovieShared.API.Results;

namespace TinyMovieShared.API.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult(ResultEnvelope result)
        {
            if(result.Data == null)
            {
                return HandleNoDataResult(result);
            }

            return HandleDataResult(result);

        }

        private IActionResult HandleNoDataResult(ResultEnvelope result)
        {
            if (result.IsSuccess && result.Message == null)
            {
                return StatusCode(204);
            }
            if (result.IsSuccess && result.Message != null)
            {
                return StatusCode(200, new ResultViewModel() { Message = result.Message });
            }
            if (result.StatusCode.HasValue)
            {
                return StatusCode(result.StatusCode.Value, new ResultViewModel() { Message = result.Message, Errors = result.Errors });
            }

            return BadRequest(new ResultViewModel() { Message = result.Message, Errors = result.Errors });
        }

        private IActionResult HandleDataResult(ResultEnvelope result)
        {
            if (result.IsSuccess)
            {
                return StatusCode(200, new ResultViewModel() { Message = result.Message, Data = result.Data });
            }
            if (result.StatusCode.HasValue)
            {
                return StatusCode(result.StatusCode.Value, new ResultViewModel() { Message = result.Message, Errors = result.Errors, Data = result.Data });
            }

            return BadRequest(new ResultViewModel() { Message = result.Message, Errors = result.Errors });

        }
     
    }
}
