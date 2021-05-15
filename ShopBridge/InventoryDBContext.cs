using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;
using System.Diagnostics.CodeAnalysis;

namespace ShopBridge
{
    [ExcludeFromCodeCoverage]
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
    }
}
