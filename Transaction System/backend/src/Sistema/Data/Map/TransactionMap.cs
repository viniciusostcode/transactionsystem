using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema.Models.Enums;
using System.Reflection.Emit;

namespace Sistema.Data.Map
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionModel>
    {
        public void Configure(EntityTypeBuilder<TransactionModel> builder)
        {
           

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price);
            builder.Property(x => x.Situation);
            builder.Property(x => x.Profit);
            builder.Property(x => x.Date);
            builder.Property(x => x.Coin);
            builder.Property(x => x.UserId);
            builder.HasOne(x => x.User);
        }
    }
}
