using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace PhotoStudio.WebApi.Extensions
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in this.apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlDocFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            if (File.Exists(xmlDocFilePath))
                options.IncludeXmlComments(xmlDocFilePath);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {

            var info = new OpenApiInfo()
            {
                Title = "Photo Studio Api dacumentaion",
                Version = description.ApiVersion.ToString()

            };

            if (description.IsDeprecated)
                info.Description += " This api version has been deprecated";

            return info;

        }


    }
}
