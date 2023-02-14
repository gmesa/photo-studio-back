using Microsoft.EntityFrameworkCore;
using PhotoStudio.Infrastructure.Commons.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using PhotoStudio.Infrastructure.Data.Configurations;
using PhotoStudio.Infrastructure.Data.DBContext;
using PhotoStudio.Application.Services;
using PhotoStudio.Application.Interfaces;
using PhotoStudio.Infrastructure.Data;
using PhotoStudio.Domain.Interfaces;
using PhotoStudio.Application.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using PhotoStudio.WebApi.Extensions;
using PhotoStudio.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using System.Text.Json.Serialization;
using PhotoStudio.WebApi.Logs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfigManager, ConfigManager>();

var config = new ConfigManager(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Host.ConfigureSerilog();
builder.Services.AddLogging(logger => logger.AddSerilog());

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
})   

.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddAutoMapperApp();

builder.Services.AddCustomHandlerExceptionConfiguration(builder.Configuration);

builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = false; });

builder.Services.AddSwaggerGen().ConfigureOptions<ConfigureSwaggerGenOptions>();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
})
.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("cors", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });

});


builder.Services.AddDbContext<PhotoStudioContext>(options =>
{
    options.UseSqlServer(config.ServicesConnectionString);
    options.EnableSensitiveDataLogging(config.EnableSensitiveDataLogging);

});
builder.Services
.AddScoped<IMaterialManager, MaterialManager>()
.AddScoped<ISizeManager, SizeManager>()
.AddScoped<IPhotoBookManager, PhotoBookManager>()
.AddTransient(typeof(ICommandRepository<>), typeof(CommandRepository<>))
.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));


var app = builder.Build();

app.MapGet("/", (context) => Task.FromResult("Hello World!"));
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
       // options.RoutePrefix = string.Empty;
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }

    });
//}
app.UseStaticFiles();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseRouting();
app.UseCors("cors");
app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();

public partial class Program { }
