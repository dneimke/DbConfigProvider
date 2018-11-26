using ConfigurationDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationDemo.Infrastructure.Configuration
{
    public class DemoDbProvider : ConfigurationProvider
    {
        private readonly Action<DbContextOptionsBuilder> _options;

        public DemoDbProvider(Action<DbContextOptionsBuilder> options)
        {
            _options = options;

        }

        public async override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _options(builder);

            using (var context = new ApplicationDbContext(builder.Options))
            {
                EnsureItems(context);

                var items = await context.ApplicationConfigurationItem
                    .AsNoTracking()
                    .ToListAsync();

                foreach (var item in items)
                {
                    Data.Add(item.Title, item.Value);
                }
            }
        }

        private static void EnsureItems(ApplicationDbContext context)
        {
            if (!context.ApplicationConfigurationItem.Any())
            {
                var list = new List<ApplicationConfigurationItem>
                    {
                        new ApplicationConfigurationItem
                        {
                            Title = "One",
                            Value = "First Item"
                        },
                        new ApplicationConfigurationItem
                        {
                            Title = "Two",
                            Value = "Second Item"
                        },
                    };

                context.ApplicationConfigurationItem.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}
