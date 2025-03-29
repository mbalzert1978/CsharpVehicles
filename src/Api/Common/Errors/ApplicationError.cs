using System;

namespace Api.Common.Errors;

internal enum ApplicationErrorType
{
    GenericError,
}

internal sealed record ApplicationError(ApplicationErrorType Type, string Component, string Detail);
