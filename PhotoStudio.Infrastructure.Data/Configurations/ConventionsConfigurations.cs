using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudio.Infrastructure.Data.Configurations
{

    public class CustomSetBuilder : SqlServerConventionSetBuilder
    {
        public CustomSetBuilder(RelationalConventionSetBuilderDependencies dependencies, ISqlGenerationHelper sqlGenerationHelper) : base(null, dependencies, sqlGenerationHelper)
        {

        }

        public override ConventionSet CreateConventionSet()
        {
            var conventions = base.CreateConventionSet();
            var et = conventions.ForeignKeyAddedConventions.FirstOrDefault(f => f is ForeignKeyIndexConvention);
            if (et != null)
                conventions.ForeignKeyAddedConventions.Remove(et);

            return conventions;
        }
    }
}
