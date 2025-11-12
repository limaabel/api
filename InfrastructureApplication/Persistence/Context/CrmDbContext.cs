using EngineApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureApplication.Persistence.Context;

public class CrmDbContext : DbContext
{
	public CrmDbContext(DbContextOptions<CrmDbContext> options)
		: base(options)
	{
	}

	// DbSets = Tables
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Activity> Activities { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
	public DbSet<SyncLog> SyncLogs { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Customer>(entity =>
		{
			entity.ToTable("crm_customers", schema: "querson");
			entity.HasKey(c => c.CustomerId);

			entity.Property(c => c.Name)
				  .IsRequired()
				  .HasMaxLength(200);

			entity.Property(c => c.Email)
				  .HasMaxLength(200);

			entity.Property(c => c.Phone)
				  .HasMaxLength(50);
		});

		modelBuilder.Entity<Activity>(entity =>
		{
			entity.ToTable("crm_activities", schema: "querson");
			entity.HasKey(a => a.ActivityId);

			entity.Property(a => a.ActivityType).IsRequired().HasMaxLength(50);
			entity.Property(a => a.Subject).IsRequired().HasMaxLength(200);

			entity.HasOne(a => a.Customer)
				  .WithMany(c => c.Activities)
				  .HasForeignKey(a => a.CustomerId);

		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.ToTable("crm_orders", schema: "querson");

			entity.HasKey(o => o.OrderId);

			entity.Property(o => o.Status)
				  .HasMaxLength(50);

			// Define the foreign key relationship (1:N)
			entity.HasOne(o => o.Customer)
				  .WithMany(c => c.Orders)
				  .HasForeignKey(o => o.CustomerId)
				  .OnDelete(DeleteBehavior.Cascade); // Optional but recommended

			// Optional: define precision or constraints
			entity.Property(o => o.TotalAmount)
				  .HasColumnType("decimal(18,2)");
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.ToTable("crm_products", schema: "querson");
			entity.HasKey(p => p.ProductId);

			entity.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(200);

			entity.Property(p => p.Description)
				.HasMaxLength(500);

			entity.Property(p => p.UnitPrice)
				.HasColumnType("decimal(18,2)");
		});

		modelBuilder.Entity<OrderItem>(entity =>
		{
			entity.ToTable("crm_orderitems", schema: "querson");
			entity.HasKey(oi => oi.OrderItemId);

			entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

			entity.HasOne(oi => oi.Order)
				  .WithMany(o => o.OrderItems)
				  .HasForeignKey(oi => oi.OrderId);

			entity.HasOne(oi => oi.Product)
				  .WithMany(p => p.OrderItems)
				  .HasForeignKey(oi => oi.ProductId);
		});


		modelBuilder.Entity<FinancialTransaction>(entity =>
		{
			entity.ToTable("crm_financialtransactions", schema: "querson");

			entity.HasKey(ft => ft.TransactionId);

			entity.Property(ft => ft.TransactionType)
				  .HasMaxLength(50)
				  .IsRequired();

			entity.Property(ft => ft.Status)
				  .HasMaxLength(50)
				  .HasDefaultValue("Open");

			entity.Property(ft => ft.Amount)
				  .HasColumnType("decimal(18,2)");

			entity.HasOne(ft => ft.Customer)
				  .WithMany(c => c.FinancialTransactions)
				  .HasForeignKey(ft => ft.CustomerId);

			entity.HasOne(ft => ft.Order)
				  .WithMany(o => o.FinancialTransactions)
				  .HasForeignKey(ft => ft.OrderId)
				  .IsRequired(false);
		});


		modelBuilder.Entity<SyncLog>(entity =>
		{
			entity.ToTable("crm_synclog", schema: "querson");
			entity.HasKey(s => s.SyncId);

			entity.Property(s => s.EntityType)
				  .IsRequired()
				  .HasMaxLength(50);

			entity.Property(s => s.Operation)
				  .IsRequired()
				  .HasMaxLength(50);

			entity.Property(s => s.Status)
				  .IsRequired()
				  .HasMaxLength(50);
		});
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var entries = ChangeTracker
			.Entries()
			.Where(e => e.Entity is BaseEntity && (
				e.State == EntityState.Added ||
				e.State == EntityState.Modified));

		foreach (var entityEntry in entries)
		{
			((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
		}

		return base.SaveChangesAsync(cancellationToken);
	}


}