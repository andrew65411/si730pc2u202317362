using Microsoft.EntityFrameworkCore;
using Sales.Domain;

namespace Sales.Infrastructure
{
    /// <summary>
    /// DbContext para el esquema hialpesa en MySQL.
    /// </summary>
    public class HialpesaDbContext : DbContext
    {
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public HialpesaDbContext(DbContextOptions<HialpesaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración manual para PurchaseOrder con snake_case y pluralización
            var purchaseOrder = modelBuilder.Entity<PurchaseOrder>();
            purchaseOrder.ToTable("purchase_orders");
            purchaseOrder.HasKey(e => e.Id);
            purchaseOrder.Property(e => e.Id).HasColumnName("id");
            purchaseOrder.Property(e => e.Customer).HasColumnName("customer");
            purchaseOrder.Property(e => e.PoNumber).HasColumnName("po_number");
            // Mapeo del value object Fabric como int (id del enum)
            purchaseOrder.OwnsOne(e => e.Fabric, fabric =>
            {
                fabric.Property(f => f.Value)
                    .HasColumnName("fabric_id")
                    .HasConversion<int>()
                    .IsRequired();
            });
            purchaseOrder.Property(e => e.Vendor).HasColumnName("vendor");
            purchaseOrder.Property(e => e.ShipTo).HasColumnName("ship_to");
            purchaseOrder.Property(e => e.Quantity).HasColumnName("quantity");
            purchaseOrder.Property(e => e.CreatedAt).HasColumnName("created_at");
            purchaseOrder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
