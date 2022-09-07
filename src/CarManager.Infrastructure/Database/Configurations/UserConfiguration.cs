namespace CarManager.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnOrder(1);

        builder.Property(p => p.Email)
            .HasConversion(p => p.Value, p => Email.Create(p).Value).HasColumnOrder(4)
            .IsRequired().HasColumnName("Email");

        builder.OwnsOne(x => x.HashedPassword, z =>
        {
            z.Property(x => x.Hash).HasColumnName("HashedPassword").IsRequired();
            z.Property(x => x.Salt).HasColumnName("SaltPassword").IsRequired();
        });


        builder.Property(x => x.FirstName).HasConversion(p => p.Value,
                p => FirstName.Create(p).Value).HasColumnOrder(2)
            .IsRequired().HasMaxLength(13)
            .HasColumnName("FirstName");

        builder.Property(x => x.LastName).HasConversion(p => p.Value,
                p => LastName.Create(p).Value).HasColumnOrder(3)
            .IsRequired().HasMaxLength(35)
            .HasColumnName("LastName");

        builder.Property(x => x.Role).HasConversion(p => p.Value, p => Role.Create(p).Value).HasColumnOrder(3)
            .IsRequired().HasMaxLength(25)
            .HasColumnName("Role");

        builder.OwnsOne(x => x.RefreshToken, z =>
        {
            z.WithOwner()
                .HasForeignKey("Id");
            z.ToTable("RefreshTokens");
            z.Property(x => x.Value).HasColumnName("RefreshToken").IsRequired();
            z.Property(x => x.Used).HasColumnName("Used").IsRequired();
            z.Property(x => x.ExpireTime).HasColumnName("ExpireDate").IsRequired();
            z.Property(x => x.CreationDate).HasColumnName("CreationDate").IsRequired();
        });

        builder.ToTable("Users", Schema.CarManager);
    }
}