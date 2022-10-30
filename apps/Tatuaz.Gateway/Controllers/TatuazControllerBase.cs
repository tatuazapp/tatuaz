using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class TatuazControllerBase : ControllerBase
{
    public TatuazControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; set; }

    public ActionResult<TData> ResultToActionResult<TData>(TatuazResult<TData> result)
    {
        if (result.Successful)
        {
            return Ok(HttpHelpers.ToOkObject(result));
        }

        return new ObjectResult(HttpHelpers.ToErrorsObject(result.Errors)) { StatusCode = (int)result.HttpStatusCode };
    }
}
