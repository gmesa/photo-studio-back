using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public class PolicyOptions
    {
        /// <summary>
        /// The policies for HttpClient
        /// </summary>
        public RetryPolicyOptions RetryPolicyOptions { get; set; }
    }
}
