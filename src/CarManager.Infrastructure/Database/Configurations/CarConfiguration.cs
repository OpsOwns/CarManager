namespace CarManager.Infrastructure.Database.Configurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnOrder(1);

        builder.Property(x => x.Power).IsRequired();
        builder.Property(x => x.Engine).IsRequired();

        builder.OwnsOne(x => x.Brand, x =>
        {
            x.Property(z => z.Generation).HasColumnName("Generation").IsRequired();
            x.Property(z => z.Make).HasColumnName("Make").IsRequired();
            x.Property(z => z.Model).HasColumnName("Model").IsRequired();
            x.Property(z => z.ProductionYear).HasColumnName("ProductionYear").IsRequired();
        });

        builder.Property(x => x.FuelType).HasColumnName("FuelType").HasConversion(p => p.Key, v
            => FuelType.GetValueByKey(v)!).IsRequired();

        builder.OwnsOne(x => x.ImageLink, x => { x.Property(z => z.Value).HasColumnName("UrlLink").IsRequired(); });

        builder.ToTable("Cars", Schema.CarManager);
    }
}