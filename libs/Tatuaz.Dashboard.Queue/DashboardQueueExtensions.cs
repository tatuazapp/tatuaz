using Microsoft.Extensions.DependencyInjection;
using Tatuaz.Dashboard.Queue.Contracts.Statistics;
using Tatuaz.Dashboard.Queue.Producers.Identity;
using Tatuaz.Dashboard.Queue.Producers.Photo;
using Tatuaz.Dashboard.Queue.Producers.Post;
using Tatuaz.Dashboard.Queue.Producers.Statistics;

namespace Tatuaz.Dashboard.Queue;

public static class DashboardQueueExtensions
{
    public static IServiceCollection RegisterDashboardQueueServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<ListCategoriesProducer>();
        services.AddScoped<SetBackgroundPhotoProducer>();
        services.AddScoped<SetForegroundPhotoProducer>();
        services.AddScoped<DeleteBackgroundPhotoProducer>();
        services.AddScoped<DeleteForegroundPhotoProducer>();
        services.AddScoped<GetUserProducer>();
        services.AddScoped<AddPhotoProducer>();
        services.AddScoped<DeletePhotoProducer>();
        services.AddScoped<FinalizePostProducer>();
        services.AddScoped<GetRegisteredStatsProducer>();
        services.AddScoped<UploadPostPhotosProducer>();
        services.AddScoped<SetBioProducer>();
        services.AddScoped<SetAccountTypeProducer>();
        services.AddScoped<GetTopArtistsProducer>();
        services.AddScoped<SearchPostsProducer>();

        return services;
    }
}
