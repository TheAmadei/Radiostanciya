using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Models
{
    public class ApplContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Record> Records { get; set; }


        public ApplContext(DbContextOptions<ApplContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasMany(p => p.Employees).WithOne(p => p.Position).HasForeignKey(p => p.PositionId);
            modelBuilder.Entity<Employee>().HasMany(p => p.Scheudules).WithOne(p => p.Employee);
            modelBuilder.Entity<Performer>().HasMany(p => p.Records).WithOne(p => p.Performer);
            modelBuilder.Entity<Employee>().HasMany(p => p.Records).WithOne(p => p.Employee);
            modelBuilder.Entity<Genre>().HasMany(p => p.Records).WithOne(p => p.Genre);
            base.OnModelCreating(modelBuilder);
        }
    }
}
