using DumaemSchool.Database;
using FluentMigrator;

namespace DumaemSchool.Migrator.Migrations._10;

[Migration(2023101001)]
public sealed class Migration2023101001 : BaseMigration
{
    public Migration2023101001(NpgsqlConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    public override void Up()
    {
        Alter.Table("section_student")
            .AddColumn("date_added")
            .AsDate()
            .NotNullable();
        Alter.Table("schedule")
            .AddColumn("duration")
            .AsCustom("interval")
            .NotNullable();
    }
}