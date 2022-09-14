using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        private readonly DbContextOptions options;

        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Hotel> Hotel { get; set; }
    }
}
