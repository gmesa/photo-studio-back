using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PhotoStudio.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAutoMapperApp(this IServiceCollection services)
        {

            var mapConfig = new MapperConfiguration(mc => mc.AddProfile(new PhotoStudioMapperConfiguration()));
            IMapper mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
