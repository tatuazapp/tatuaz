using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tatuaz.Shared.Pipeline.Messages;

namespace Tatuaz.Gateway.Controllers;

[ApiController, Route("[controller]")]
public class TatuazControllerBase : ControllerBase
{
    protected IMediator Mediator { get; set; }

    public TatuazControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    public ActionResult<TData> ResultToActionResult<TData>(TatuazResult<TData> result)
    {
        if (result.Successful)
        {
            return Ok(result);
        }

        return new ObjectResult(HttpHelpers.ToErrorsObject(result.Errors))
        {
            StatusCode = result.HttpStatusCode as int?
        };
    }
}
