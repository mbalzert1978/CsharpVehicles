namespace Api.Common.ResultType;

internal static class Factories
{
    public static Result<T, TError> Ok<T, TError>(T value) => new(true, value, default);

    public static Result<T, TError> Err<T, TError>(TError error) => new(false, default, error);
}
