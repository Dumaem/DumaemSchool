using DumaemSchool.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Auth;

internal sealed class SchoolIdentityContext : IdentityDbContext<SchoolUser, SchoolRole, int>
{
    public SchoolIdentityContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
    }
}