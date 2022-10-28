using Tatuaz.Shared.Pipeline.Factories.ErrorCodes;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Shared.Pipeline.Factories.Errors;

public static class CommonErrorFactory
{
    public static TatuazError InternalError(string? message = null)
        => message is null
            ? new TatuazError(CommonErrorCodes.InternalError, "Internal error occured.")
            : new TatuazError(CommonErrorCodes.InternalError, message);
    public static TatuazError DatabaseError(string? message = null)
        => message is null
            ? new TatuazError(CommonErrorCodes.DatabaseError, "Database error occured.")
            : new TatuazError(CommonErrorCodes.DatabaseError, message);
    public static TatuazError QueueError(string? message = null)
        => message is null
            ? new TatuazError(CommonErrorCodes.QueueError, "Queue error occured.")
            : new TatuazError(CommonErrorCodes.QueueError, message);
}
