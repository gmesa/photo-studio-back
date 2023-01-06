using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using PhotoStudio.Domain.Entities;
using PhotoStudio.Infrastructure.Data.Configurations;


namespace PhotoStudio.Infrastructure.Data.DBContext
{
    public class PhotoStudioContext : CommonDbContext
    {

        public PhotoStudioContext(DbContextOptions<PhotoStudioContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MaterialConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoBookConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
        }

    }

}
