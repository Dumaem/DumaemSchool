using DumaemSchool.Database;
using FluentMigrator;

namespace DumaemSchool.Migrator.Migrations._10;

[Migration(2023_10_11_01)]
public class Migration2023101101 : BaseMigration
{
    public Migration2023101101(NpgsqlConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    public override void Up()
    {
        Alter.Table("section_student")
            .AddColumn("is_actual")
            .AsBoolean()
            .NotNullable()
            .WithDefaultValue(true);
    }
}