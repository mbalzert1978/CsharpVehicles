using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Api.Abstraction;

namespace Api.Common.ResultType;

internal readonly record struct Result<T, TError> : IResult<T, TError>
{
    private const string OK = "Ok";
    private const string ERR = "Err";
    private const string INVALID_STATE_ERROR = "Either value or error must be null, but not both.";
    private static readonly CompositeFormat UnwrapErrorTemplate = CompositeFormat.Parse(
        "Called `{0}` on an ['{1}'] value: {2}."
    );

    private readonly T? _value;
    private readonly TError? _error;

    [NotNull]
    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(_error))]
    public bool IsOk { get; init; }

    internal Result(bool isOk, T? value, TError? error)
    {
        (IsOk, _value, _error) = (isOk, value, error) switch
        {
            (true, T, null) => (true, value, default(TError)),
            (false, null, TError) => (false, default(T), error),
            _ => throw new ArgumentException(INVALID_STATE_ERROR),
        };
    }

    [return: NotNull]
    public T Unwrap() =>
        IsOk
            ? _value
            : throw new UnwrapFailedException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    UnwrapErrorTemplate,
                    nameof(Unwrap),
                    ERR,
                    nameof(_error)
                )
            );

    [return: NotNull]
    public TError UnwrapErr() =>
        !IsOk
            ? _error
            : throw new UnwrapFailedException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    UnwrapErrorTemplate,
                    nameof(UnwrapErr),
                    OK,
                    nameof(_value)
                )
            );

    public IResult<TOut, TError> Bind<TOut>([DisallowNull] Func<T, IResult<TOut, TError>> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return IsOk ? operation(_value) : Factories.Err<TOut, TError>(_error);
    }

    public IResult<TOut, TError> Map<TOut>([DisallowNull] Func<T, TOut> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return IsOk
            ? Factories.Ok<TOut, TError>(operation(_value))
            : Factories.Err<TOut, TError>(_error);
    }

    public IResult<T, TF> MapErr<TF>([DisallowNull] Func<TError, TF> operation)
    {
        ArgumentNullException.ThrowIfNull(operation);
        return IsOk ? Factories.Ok<T, TF>(_value) : Factories.Err<T, TF>(operation(_error));
    }
}
