using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.HttpResponses;

/// <summary>
///     Wrapper used for returning failed responses.
/// </summary>
/// <param name="Errors">List of errors.</param>
/// <param name="Success">Indicates if request was successful. Should be always false for this type of response.</param>
public record ErrorResponse
(
    TatuazError[] Errors,
    bool Success = false
);
