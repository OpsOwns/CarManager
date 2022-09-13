namespace CarManager.Infrastructure.Database.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnOrder(1);

        builder.Property(p => p.EmailContact)
            .HasConversion(p => p.Value, p => Email.Create(p).Value).HasColumnOrder(4)
            .IsRequired().HasColumnName("ContactEmail");

        builder.Property(p => p.Phone).HasConversion(p => p.Value, p => Phone.Create(p).Value).IsRequired()
            .HasColumnName("Phone");


        builder.Property(x => x.FirstName).HasConversion(p => p.Value,
                p => FirstName.Create(p).Value).HasColumnOrder(2)
            .IsRequired().HasMaxLength(13)
            .HasColumnName("FirstName");

        builder.Property(x => x.LastName).HasConversion(p => p.Value,
                p => LastName.Create(p).Value).HasColumnOrder(3)
            .IsRequired().HasMaxLength(35)
            .HasColumnName("LastName");

        builder.OwnsOne(x => x.Address, x =>
        {
            x.Property(z => z.City).HasColumnName("City").IsRequired();
            x.Property(z => z.Residence).HasColumnName("Residence").IsRequired();
            x.Property(z => z.Street).HasColumnName("Street").IsRequired();
        });

        builder.HasMany(p => p.Cars).WithOne()
            .OnDelete(DeleteBehavior.Cascade)
            .Metadata.PrincipalToDependent?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.ToTable("Customer", Schema.CarManager);
    }
}