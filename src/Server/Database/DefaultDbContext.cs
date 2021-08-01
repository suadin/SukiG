using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SukiG.Shared.Model;
using System;

namespace SukiG.Server.Database
{
    public class DefaultDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<User> User { get; set; }

        public DbSet<Achivement> Achivement { get; set; }

        public DefaultDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var password = configuration.GetConnectionString("DefaultConnectionPassword");
            optionsBuilder.UseNpgsql($"{connectionString};Password={password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achivement>().Property(e => e.Difficulty)
                .HasConversion(v => v.ToString(), v => (Difficulty)Enum.Parse(typeof(Difficulty), v));
        }
    }
}