using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using UserManagement.API.Entities;

namespace UserManagement.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
