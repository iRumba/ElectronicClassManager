using System;
using System.Data.Common;
using System.Reflection;
using ElectronicClassManager.Entities.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicClassManager.Db.Configuration;

public static class EfConfigurationExtensions
{
    public static IServiceCollection ConfigureEf(this IServiceCollection services, EfConfiguration conf)
    {
        return services.AddDbContext<EntitiesDbContext>(builder => builder.ConfigureOptionsBuilder(conf));
    }

    public static string CreateConnectionString(this EfConfiguration conf)
    {
        return $"Server={conf.Address};Port={conf.Port};Database={conf.Db};User Id={conf.User};Password={conf.Password};";
    }

    public static DbContextOptionsBuilder ConfigureOptionsBuilder(this DbContextOptionsBuilder builder, EfConfiguration conf)
    {
        return builder.UseNpgsql(conf.CreateConnectionString());
    }
}
