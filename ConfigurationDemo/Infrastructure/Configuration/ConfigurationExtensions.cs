using ConfigurationDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationDemo.Infrastructure.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddDemoDbProvider(
            this IConfigurationBuilder configuration, Action<DbContextOptionsBuilder> setup)
        {
            configuration.Add(new DemoDbSource(setup));
            return configuration;
        }
    }
}
