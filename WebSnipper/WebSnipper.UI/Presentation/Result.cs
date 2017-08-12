namespace WebSnipper.UI.Presentation
{
    public class Result
    {
        public bool IsSuccess => Error == null;
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        private Result(string error = null)
        {
            Error = error;
        }

        public static Result Ok() => new Result();
        public static Result Fail(string msg) => new Result(msg);
    }
}