using RabbitMQ.Client;

namespace TelegramBot.Infrastructure
{
    public class ApiResult
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد.";
        public const string ErrorMessage = "اروری سمت سرور وجود دارد!";
        public const string NotFoundDefaultMessage = "اطلاعات یافت نشد!";

        public static string NotFoundMessage(string value) => $"یافت نشد! {value}";
        public MetaData MetaData { get; set; }
        public bool IsSuccess { get; set; }
        public static ApiResult Success()
        {
            return new ApiResult
            {
                IsSuccess = true,
                MetaData = new MetaData
                {
                    Message = SuccessMessage,
                    statusCode = AppStatusCode.Success,
                },
            };
        }
        public static ApiResult NotFound(string? errorField)
        {
            return new ApiResult
            {
                IsSuccess = false,
                MetaData = new MetaData
                {
                    Message = errorField != null ? NotFoundMessage(errorField) : NotFoundDefaultMessage,
                    statusCode = AppStatusCode.NotFound,
                },
            };
        }
        public static ApiResult Error()
        {
            return new ApiResult
            {
                IsSuccess = false,
                MetaData = new MetaData
                {
                    Message = ErrorMessage,
                    statusCode = AppStatusCode.ServerError,
                },
            };
        }
    }
    public class ApiResult<TData>
    {
        public bool IsSuccess { get; set; }
        public MetaData MetaData { get; set; }
        public TData Data { get; set; }

        public const string SuccessMessage = "عملیات با موفقیت انجام شد.";
        public const string ErrorMessage = "اروری سمت سرور وجود دارد!";
        public const string NotFoundDefaultMessage = "اطلاعات یافت نشد!";

        public static string NotFoundMessage(string value) => $"یافت نشد! {value}";

        public static ApiResult<TData> Success(TData Data)
        {
            return new ApiResult<TData>
            {
                IsSuccess = true,
                MetaData = new MetaData
                {
                    Message = SuccessMessage,
                    statusCode = AppStatusCode.Success,
                },
                Data = Data
            };
        }
        public static ApiResult<TData> NotFound(string? errorField)
        {
            return new ApiResult<TData>
            {
                IsSuccess = false,
                MetaData = new MetaData
                {
                    Message = errorField != null ? NotFoundMessage(errorField) : NotFoundDefaultMessage,
                    statusCode = AppStatusCode.NotFound,
                },
                Data = default(TData)!
            };
        }
        public static ApiResult<TData> Error()
        {
            return new ApiResult<TData>
            {
                IsSuccess = false,
                MetaData = new MetaData
                {
                    Message = ErrorMessage,
                    statusCode = AppStatusCode.ServerError,
                },
                Data = default(TData)!
            };
        }
    }
    public class MetaData
    {
        public string Message { get; set; }
        public AppStatusCode statusCode { get; set; }
    }
    public enum AppStatusCode
    {
        Success = 200,
        NotFound = 404,
        BadRequest = 400,
        UnAuthorize = 401,
        ServerError = 500,
        LogicError = 422,
    }
}
