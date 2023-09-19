using DumaemSchool.Database;
using FluentMigrator;
using Npgsql;

namespace DumaemSchool.Migrator.Migrations;

public abstract class BaseMigration : ForwardOnlyMigration, IDisposable
{
    protected NpgsqlConnection Connection { get; }

    protected BaseMigration(NpgsqlConnectionProvider connectionProvider)
    {
        Connection = connectionProvider.GetConnection();
    }

    public void Dispose()
    {
        Connection.Dispose();
        GC.SuppressFinalize(this);
    }
}