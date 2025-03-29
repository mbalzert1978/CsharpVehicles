using System.Diagnostics.CodeAnalysis;

namespace Api.Abstraction;

internal interface IResult<T, TError>
{
    bool IsOk { get; }
    T Unwrap();
    TError UnwrapErr();
    IResult<TOut, TError> Bind<TOut>([DisallowNull] Func<T, IResult<TOut, TError>> operation);
    IResult<TOut, TError> Map<TOut>([DisallowNull] Func<T, TOut> operation);
    IResult<T, TF> MapErr<TF>([DisallowNull] Func<TError, TF> operation);
}
