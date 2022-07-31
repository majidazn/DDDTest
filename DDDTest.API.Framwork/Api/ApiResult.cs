using DDDTest.Domain.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DDDTest.API.Framwork.ExceptionMethods;

namespace DDDTest.API.Framwork.Api {
    public class ApiResult {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null) {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result) {
            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result) {
            return new ApiResult(false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result) {
            System.Reflection.PropertyInfo pi = result.Value.GetType().GetProperty("Errors");
            Dictionary<string, string[]> errors = (Dictionary<string, string[]>)(pi.GetValue(result.Value, null));

            var message = result.ToString();
            var errorMessage = errors.SelectMany(p => p.Value).Distinct();
            if (errorMessage.Count() > 0)
                message = string.Join(" | ", errorMessage);

            return new ApiResult(false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result) {
            return new ApiResult(true, ApiResultStatusCode.Success, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result) {
            return new ApiResult(false, ApiResultStatusCode.NotFound);
        }
        #endregion
    }

    public class ApiResult<TData> : ApiResult {
        // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null, int? total = null)
            : base(isSuccess, statusCode, message) {
            Data = data;
            Total = total;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data) {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result) {
            object nullObject = null;
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)nullObject);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result) {
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result) {
            object nullObject = null;
            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, (TData)nullObject);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result) {
            System.Reflection.PropertyInfo pi = result.Value.GetType().GetProperty("Errors");
            Dictionary<string, string[]> errors = (Dictionary<string, string[]>)(pi.GetValue(result.Value, null));

            var message = result.ToString();
            var errorMessage = errors.SelectMany(p => p.Value).Distinct();
            if (errorMessage.Count() > 0)
                message = string.Join(" | ", errorMessage);

            return new ApiResult<TData>(false, ApiResultStatusCode.BadRequest, (TData)result.Value, message);

        }

        public static implicit operator ApiResult<TData>(ContentResult result) {
            object nullObject = null;
            return new ApiResult<TData>(true, ApiResultStatusCode.Success, (TData)nullObject, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result) {
            object nullObject = null;
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)nullObject);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result) {
            return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }
}
