using ATMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMSystem.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserHistoryModel> History { get; set; }
        public DbSet<UserRegisterModel> Registers { get; set; }
    }
}
