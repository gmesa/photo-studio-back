using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Commons.Configurations
{
    public class CustomExceptionHandlerOptions
    {
        public static string SectionName = "CustomExceptionHandlerOptions";
        public bool AllwaysReturnOK { get; set; }

        public bool IncludeDetails { get; set; }
    }
}
