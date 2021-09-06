using Microsoft.EntityFrameworkCore;

namespace RazorPagesUser.Data
{
    public class RazorPagesUserContext : DbContext
    {
        public RazorPagesUserContext (DbContextOptions<RazorPagesUserContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesUser.Models.User> User { get; set; }
    }
}
