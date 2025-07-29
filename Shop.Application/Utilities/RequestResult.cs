using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;

namespace Shop.Application.Utilities
{
    public class RequestResult
    {
        public bool IsSuccess { get; set; }
        public RequestResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object AdditionalData { get; set; }
        public RequestResult(bool isSuccess, RequestResultStatusCode statusCode, string message = null, List<string> errors = null, object additionalData = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
            Errors = errors ?? new List<string>();
            AdditionalData = additionalData;
        }

        #region Implicit Operators

        public static implicit operator RequestResult(OkResult result)
        {
            return new RequestResult(true, RequestResultStatusCode.Success);
        }

        public static implicit operator RequestResult(ContentResult result)
        {
            return new RequestResult(true, RequestResultStatusCode.Success, result.Content);
        }

        public static implicit operator RequestResult(BadRequestResult result)
        {
            return new RequestResult(false, RequestResultStatusCode.BadRequest);
        }

        public static implicit operator RequestResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(r => (string[])r.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }

            return new RequestResult(false, RequestResultStatusCode.BadRequest, message);
        }

        public static implicit operator RequestResult(NotFoundResult result)
        {
            return new RequestResult(false, RequestResultStatusCode.NotFound);
        }

        #endregion
    }

    public class RequestResult<TData> : RequestResult where TData : class
    {
        public TData Data { get; set; }
        
        public RequestResult(bool isSuccess, RequestResultStatusCode statusCode,  TData data, string message = null, List<string> errors = null, object additionalData = null) : base(isSuccess, statusCode, message, errors, additionalData)
        {
            Data = data;
        }
        
        #region Implicit Operators

        public static implicit operator RequestResult<TData>(TData data)
        {
            return new RequestResult<TData>(true, RequestResultStatusCode.Success, data);
        }
        
        public static implicit operator RequestResult<TData>(ContentResult result)
        {
            return new RequestResult<TData>(true, RequestResultStatusCode.Success, null, result.Content);
        }

        public static implicit operator RequestResult<TData>(BadRequestResult result)
        {
            return new RequestResult<TData>(false, RequestResultStatusCode.BadRequest, null);
        }

        public static implicit operator RequestResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(r => (string[])r.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }

            return new RequestResult<TData>(false, RequestResultStatusCode.BadRequest, null, message);
        }

        public static implicit operator RequestResult<TData>(NotFoundResult result)
        {
            return new RequestResult<TData>(false, RequestResultStatusCode.NotFound, null);
        }
        
        #endregion
    }

    public enum RequestResultStatusCode
    {
        [Display(Name = "درخواست با موفقیت انجام شد.")]
        Success = 200,

        [Display(Name = "درخواست به دلیل نحو نادرست توسط سرور قابل درک نیست.")]
        BadRequest = 400,

        [Display(Name = "درخواست به اطلاعات احراز هویت کاربر نیاز دارد.")]
        Unauthorized = 401,

        [Display(Name = "شما حق دسترسی به محتوا را ندارید!")]
        Forbidden = 403,

        [Display(Name = "سرور نمی تواند داده را پیدا کند.")]
        NotFound = 404,

        [Display(Name = "درخواست به دلیل تضاد با وضعیت فعلی منبع تکمیل نشد.")]
        Conflict = 409,

        [Display(Name = "محصول در تجمیع استفاده شده است.")]
        UseInCollection = 410,

        [Display(Name = "درخواست به دلیل نحو نادرست توسط سرور قابل درک نیست.")]
        UseInAssignment = 411,


        [Display(Name = "داده در سیستم استفاده شده است، سرور قادر به حذف آن نیست!")]
        InternalServerError = 500,
    }
}