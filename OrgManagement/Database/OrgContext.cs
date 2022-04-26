using System;
using Microsoft.EntityFrameworkCore;
using OrgManagement.Database.Models;

namespace OrgManagement.Database
{
    public class OrgContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public OrgContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=orgdb.sqlite");
        }
    }
}
