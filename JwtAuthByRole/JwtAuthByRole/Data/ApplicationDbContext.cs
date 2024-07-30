using JwtAuthByRole.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JwtAuthByRole.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
