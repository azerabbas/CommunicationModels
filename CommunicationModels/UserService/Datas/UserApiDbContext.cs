using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Datas
{
    public class UserApiDbContext : DbContext
    {
        public UserApiDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
