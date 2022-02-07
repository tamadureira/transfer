using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Transfer.Infra.Mappings
{
    public class TransferMapping : IEntityTypeConfiguration<Domain.Transfer>
    {
        public void Configure(EntityTypeBuilder<Domain.Transfer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ExternalID)
                .HasColumnName("ExternalID")
                .IsRequired()
                .HasColumnType("uniqueIdentifier");

            builder.Property(c => c.Amount)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(c => c.ExpectedOn)
                .HasColumnName("ExpectedOn")
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.Status)
                .HasColumnName("Status")
                .HasColumnType("int");

            builder.ToTable("HistoryTransfers");
        }
    }
}
