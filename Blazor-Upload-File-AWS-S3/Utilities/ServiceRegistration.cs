using Businesslayer.Managers;
using Businesslayer.Services;
using Sharedlayer.Interfaces;
using Sharedlayer.Interfaces.Services;

namespace Blazor_Upload_File_AWS_S3.Utilities
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDocumentManager, DocumentManager>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
