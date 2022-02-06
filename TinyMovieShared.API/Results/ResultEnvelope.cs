namespace TinyMovieShared.API.Result
{
    public class ResultEnvelope
    {
        public bool IsSuccess { get; private set; }

        public string Message { get; private protected set; }

        public List<string> Errors { get; private protected set; }

        public int? StatusCode { get; protected set; }

        public object Data { get; private set;  }

        protected ResultEnvelope(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static ResultEnvelope Success(object data) => new ResultEnvelope(true) { Data = data};

        public static ResultEnvelope Success(object data, string message) => new ResultEnvelope(true) { Data = data, Message = message };

        public static ResultEnvelope Success() => new ResultEnvelope(true);

        public static ResultEnvelope Success(string message) => new ResultEnvelope(true) { Message = message};

        public static ResultEnvelope Failure(string message) => new ResultEnvelope(false) { Message = message };

        public static ResultEnvelope Failure(string message, List<string> errors) => new ResultEnvelope(false) { Message = message, Errors = errors };

        public static ResultEnvelope Failure(string message, List<string> errors, int statusCode) => new ResultEnvelope(false) { Message = message, Errors = errors, StatusCode = statusCode };

        public static ResultEnvelope Failure(string message, int statusCode) => new ResultEnvelope(false) { Message = message, StatusCode = statusCode };

    }
}
