namespace Core.Results
{
    public class Response<T> where T : class
    {
        public T? Data { get; private set; }

        public ErrorDto? Error { get; private set; }

        public int StatusCode { get; private set; }

        public int CurrentPage { get; set; }

        public int TotalCount { get; set; }

        public static Response<T> Success(T data, int statusCode, int currentPage, int totalCount)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                CurrentPage = currentPage,
                TotalCount = totalCount
            };
        }

        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
            };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T>
            {
                Data = default,
                StatusCode = statusCode,
            };
        }

        public static Response<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new Response<T>
            {
                Error = errorDto,
                StatusCode = statusCode
            };
        }

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            ErrorDto errorDto = new(errorMessage, isShow);

            return new Response<T>
            {
                Error = errorDto,
                StatusCode = statusCode
            };
        }
    }
}
