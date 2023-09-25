using DumaemSchool.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Auth;

internal sealed class SchoolIdentityContext : IdentityDbContext<SchoolUser, SchoolRole, int>
{
    public SchoolIdentityContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<RestorationCode> RestorationCodes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        builder.Entity<RestorationCode>(e =>
        {
            e.HasKey(x => x.Id);
            e.ToTable("restoration_codes");
        });

        builder.Entity<SchoolUser>()
            .HasMany<RestorationCode>()
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        base.OnModelCreating(builder);
    }
}