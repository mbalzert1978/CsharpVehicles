namespace Api.Common.ResultType;

[Serializable]
internal sealed class UnwrapFailedException : ResultException
{
    public UnwrapFailedException() { }

    public UnwrapFailedException(string? message)
        : base(message) { }

    public UnwrapFailedException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
