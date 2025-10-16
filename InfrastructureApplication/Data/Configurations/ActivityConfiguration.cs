using EngineApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureApplication.Data.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
	public void Configure(EntityTypeBuilder<Activity> builder)
	{
		builder.ToTable("crm_activities");

		builder.HasKey(a => a.ActivityId);

		builder.Property(a => a.ActivityType)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(a => a.Subject)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(a => a.Notes)
			.HasColumnType("nvarchar(max)");

		builder.Property(a => a.PerformedBy)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(a => a.CreatedAt)
			.HasDefaultValueSql("SYSDATETIME()");

		// Relationships
		builder.HasOne(a => a.Customer)
			.WithMany(c => c.Activities)
			.HasForeignKey(a => a.CustomerId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}