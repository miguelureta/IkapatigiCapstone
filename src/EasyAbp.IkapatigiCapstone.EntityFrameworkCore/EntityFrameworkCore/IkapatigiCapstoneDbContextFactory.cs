using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.IkapatigiCapstone.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class IkapatigiCapstoneDbContextFactory : IDesignTimeDbContextFactory<IkapatigiCapstoneDbContext>
{
    public IkapatigiCapstoneDbContext CreateDbContext(string[] args)
    {
        IkapatigiCapstoneEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IkapatigiCapstoneDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new IkapatigiCapstoneDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../EasyAbp.IkapatigiCapstone.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
