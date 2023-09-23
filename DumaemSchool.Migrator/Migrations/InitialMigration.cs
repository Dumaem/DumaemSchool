using DumaemSchool.Database;
using FluentMigrator;

namespace DumaemSchool.Migrator.Migrations;

[Migration(2023091901)]
public class InitialMigration : BaseMigration
{
    public InitialMigration(NpgsqlConnectionProvider connectionProvider) : base(connectionProvider)
    {
    }

    public override void Up()
    {
    }
}