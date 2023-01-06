using Microsoft.Extensions.DependencyInjection;
using PhotoStudio.Infrastructure.Commons.Configurations;

namespace PhotoStudio.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomHandlerExceptionConfiguration(this IServiceCollection services, IConfiguration configuration) {

            services.Configure<CustomExceptionHandlerOptions>(configuration.GetSection(CustomExceptionHandlerOptions.SectionName));
            //services.Configure<CustomExceptionHandlerOptions>(options => { options.AllwaysReturnOK = true; options.IncludeDetails = false; });
            return services;

        }
    }
}
