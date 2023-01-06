using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public interface IConfigManager
    {
        /// <summary>
        /// Gets or sets PhotoStudio  database connection string
        /// </summary>
        string ServicesConnectionString { get;}

        /// <summary>
        /// Gets or sets Enable senstivie data loggin
        /// </summary>
        bool EnableSensitiveDataLogging { get;}
        
        /// <summary>
        /// Gets or set PolicyOptions to app clients
        /// </summary>
        PolicyOptions PolicyOptions { get; }

        /// <summary>
        /// Gets or set the Exception handler options
        /// </summary>
        CustomExceptionHandlerOptions CustomExceptionHandlerOptions { get; }


    }
}
