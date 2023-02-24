using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tatuaz.Gateway.Requests.Commands.Identity;
using Tatuaz.Shared.Pipeline.Factories.Results.Identity;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Services;

namespace Tatuaz.Gateway.Handlers.Commands.Identity;

public class SetBackgroundPhotoCommandHandler : IRequestHandler<SetBackgroundPhotoCommand, TatuazResult<Unit>>
{
    private readonly IPhotoService _photoService;

    public SetBackgroundPhotoCommandHandler(IPhotoService photoService)
    {
        _photoService = photoService;
    }
    public Task<TatuazResult<Unit>> Handle(SetBackgroundPhotoCommand request, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        request.SetBackgroundPhotoDto.Photo!.CopyTo(stream);
        if (!_photoService.ValidatePhotoHeaders(stream))
        {
            return Task.FromResult(SetBackgroundPhotoResultFactory.InvalidFileFormat<Unit>());
        }


        return Task.FromResult();
    }
}
