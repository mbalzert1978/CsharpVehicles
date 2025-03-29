namespace Api.Common.ResultType;

[Serializable]
internal class ResultException : Exception
{
    public ResultException() { }

    public ResultException(string? message)
        : base(message) { }

    public ResultException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
