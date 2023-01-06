using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    /// <summary>
    /// Configuration common for applications
    /// </summary>
    public interface IApplicationOptions
    {
       public PolicyOptions PolicyOptions { get;} 
    }
}
