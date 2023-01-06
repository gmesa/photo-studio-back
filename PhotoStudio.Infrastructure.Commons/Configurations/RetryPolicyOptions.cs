using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public class RetryPolicyOptions
    {
        public int RetryCount { get; set; }
        public int RetryDelay { get; set; }

    }
}
