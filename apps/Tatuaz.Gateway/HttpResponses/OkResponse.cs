namespace Tatuaz.Gateway.HttpResponses;

/// <summary>
///     Wrapper used for returning success responses.
/// </summary>
/// <param name="Value">Payload of response.</param>
/// <param name="Success">Indicates if request was successful. Should be always true for this type of response.</param>
/// <typeparam name="T">Type of returned payload.</typeparam>
public record OkResponse<T>
(
    T? Value,
    bool Success = true
);