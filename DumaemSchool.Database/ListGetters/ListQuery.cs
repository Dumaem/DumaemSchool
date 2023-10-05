using Dapper;

namespace DumaemSchool.Database.ListGetters;

public sealed class ListQuery
{
    public string Sql { get; init; }
    public DynamicParameters Parameters { get; init; }
}