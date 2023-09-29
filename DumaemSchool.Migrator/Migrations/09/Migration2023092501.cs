using DumaemSchool.Database;
using FluentMigrator;

namespace DumaemSchool.Migrator.Migrations._09;

[Migration(2023092501)]
public sealed class Migration2023092501 : BaseMigration
{
    public Migration2023092501(NpgsqlConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    public override void Up()
    {
        var table = Create
            .Table("restoration_codes")
            .InSchema("identity");

        table
            .WithColumn("id")
            .AsInt32()
            .PrimaryKey()
            .Identity();

        table
            .WithColumn("user_id")
            .AsInt32()
            .NotNullable()
            .ForeignKey("fk_restoration_code_user", "identity", "AspNetUsers", "Id");

        table
            .WithColumn("code")
            .AsInt32()
            .NotNullable();

        table
            .WithColumn("date_created")
            .AsDateTime()
            .NotNullable()
            .WithDefault(SystemMethods.CurrentDateTime);

        table
            .WithColumn("session_id")
            .AsGuid()
            .NotNullable();
    }
}