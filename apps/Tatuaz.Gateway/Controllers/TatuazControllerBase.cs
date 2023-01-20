using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Controllers;

/// <summary>
/// Base controller for all controllers
/// </summary>
[ApiController]
[Route("[controller]")]
public class TatuazControllerBase : ControllerBase
{
    /// <summary>
    /// Constructor receiving the mediator from DI
    /// </summary>
    /// <param name="mediator"></param>
    public TatuazControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    /// <summary>
    /// Used to communicate with handlers
    /// </summary>
    protected IMediator Mediator { get; }

    /// <summary>
    /// Wrapper for mapping TatuazResult to IActionResult
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public IActionResult ResultToActionResult<TData>(TatuazResult<TData> result)
    {
        if (result.Successful)
        {
            return new ObjectResult(HttpHelpers.ToOkObject(result))
            {
                StatusCode = (int)result.HttpStatusCode
            };
        }

        return new ObjectResult(HttpHelpers.ToErrorsObject(result.Errors))
        {
            StatusCode = (int)result.HttpStatusCode
        };
    }
}
