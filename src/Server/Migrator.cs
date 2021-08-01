using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using SukiG.Server.Database;
using System;
using System.Linq;

namespace SukiG.Server
{
    public static class Migrator
    {
        public static void Migrate(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
