using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Extensions;

namespace Shop.Application.Utilities
{
    public class RequestResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var requestResult = new RequestResult(false, RequestResultStatusCode.BadRequest, message: "داده های وارد شده نامعتبر هستند!", errors: context.ModelState.GetAllErrors());
                context.Result = new JsonResult(requestResult);
            }
            
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkResult okResult:
                {
                    var requestResult = new RequestResult(true, RequestResultStatusCode.Success);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case OkObjectResult okObjectResult:
                {
                    var requestResult = new RequestResult<object>(true, RequestResultStatusCode.Success, okObjectResult.Value);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case BadRequestResult badRequestResult:
                {
                    var requestResult = new RequestResult(false, RequestResultStatusCode.BadRequest);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case BadRequestObjectResult badRequestObjectResult:
                {
                    var message = badRequestObjectResult.Value.ToString();
                    if (badRequestObjectResult.Value is SerializableError errors)
                    {
                        var errorMessages = errors.SelectMany(r => (string[])r.Value).Distinct();
                        message = string.Join(" | ", errorMessages);
                    }
                
                    var requestResult = new RequestResult(true, RequestResultStatusCode.Success, message);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case ContentResult contentResult:
                {
                    var requestResult = new RequestResult(true, RequestResultStatusCode.Success, contentResult.Content);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case NotFoundResult notFoundResult:
                {
                    var requestResult = new RequestResult(false, RequestResultStatusCode.NotFound);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case NotFoundObjectResult notFoundObjectResult:
                {
                    var requestResult = new RequestResult<object>(false, RequestResultStatusCode.NotFound, notFoundObjectResult.Value);
                    context.Result = new JsonResult(requestResult);
                    break;
                }
                case ObjectResult { StatusCode: null, Value: not RequestResult } objectResult:
                {
                    // if (objectResult.Value is RequestResult)
                    // {
                    //     context.Result = new JsonResult(objectResult.Value);
                    // }
                    // else
                    // {
                        var requestResult = new RequestResult<object>(false, RequestResultStatusCode.NotFound, objectResult.Value);
                        context.Result = new JsonResult(requestResult);
                    // }
                    break;
                }
            }
            
            base.OnResultExecuting(context);
        }
    }
}