using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using PlcBase.Features.Payment.Entities;

namespace PlcBase.Common.Data.Context.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder.HasIndex(c => new { c.vnp_TxnRef, }).IsUnique();
    }
}
