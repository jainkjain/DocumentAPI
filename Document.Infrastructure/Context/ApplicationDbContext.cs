using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Document.Infrastructure.Context
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Domain.Entities.Document> Documents { get; set; }
    }

}
