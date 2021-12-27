using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fixture.Core.Models;

namespace Fixture.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {


            //foreach (var eType in builder..GetEntityTypes())
            //{
            //    EntityTypeBuilder x = modelBuilder.SharedTypeEntity<Dictionary<string, object>>(eType.Name, b =>
            //    {
            //        foreach (var p in eType.GetProperties())
            //        {
            //            if (p.GetType() == typeof(int))
            //            {
            //                b.IndexerProperty<int>(p.Name);
            //            }
            //            else if (p.GetType() == typeof(string))
            //            {
            //                b.IndexerProperty<string>(p.Name);
            //            }

            //        }

            //    }).SharedTypeEntity(eType.Name, eType.GetType());

            //    x.HasNoKey();

            //}


            //builder
            //    .HasKey(a => a.version);


            //builder
            //    .Property(m => m.payload);
                

            //builder
            //    .Property(m => m.payload.markets)
            //    .IsRequired();

                

           
        }
    }
}