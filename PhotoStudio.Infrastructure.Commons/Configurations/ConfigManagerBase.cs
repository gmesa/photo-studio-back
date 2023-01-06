using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public class ConfigManagerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigManagerBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected virtual string GetApplicationSettingValue(string key) {
            string prefix = "applicationSettings:";
            string defaultSetting = prefix + key;
          return  _configuration[defaultSetting] ?? string.Empty;
        }

        protected virtual TSection GetSection<TSection>(string key)
        {
            return (TSection) _configuration.GetSection(key);
        }
    }
}
