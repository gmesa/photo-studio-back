using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public class ConfigManager : ConfigManagerBase, IConfigManager, IApplicationOptions
    {

        public ConfigManager(IConfiguration configuration) : base(configuration) => Configuration = configuration;  
        
        public IConfiguration Configuration { get; }

        public string ServicesConnectionString
        {
            get => GetApplicationSettingValue("ServicesConnectionString");

        }
        public PolicyOptions PolicyOptions { get => GetSection<PolicyOptions>("PolicyOptions"); }

        public bool EnableSensitiveDataLogging { get => bool.TryParse(GetApplicationSettingValue("ServicesConnectionString"), out bool enable) && enable; }

        public CustomExceptionHandlerOptions CustomExceptionHandlerOptions { get => GetSection<CustomExceptionHandlerOptions>("CustomExceptionHandlerOptions"); }
    }
}
