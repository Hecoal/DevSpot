using DevSpot.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevSpot.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public DbSet<JobPosting>JobPosts { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options): base(options)
        {
            
        }
    }
}
