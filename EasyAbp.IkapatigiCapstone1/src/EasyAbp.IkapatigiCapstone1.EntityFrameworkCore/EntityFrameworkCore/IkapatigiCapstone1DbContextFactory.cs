using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.IkapatigiCapstone1.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class IkapatigiCapstone1DbContextFactory : IDesignTimeDbContextFactory<IkapatigiCapstone1DbContext>
{
    public IkapatigiCapstone1DbContext CreateDbContext(string[] args)
    {
        IkapatigiCapstone1EfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IkapatigiCapstone1DbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new IkapatigiCapstone1DbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EasyAbp.IkapatigiCapstone1.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
