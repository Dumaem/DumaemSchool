using Dapper;

namespace DumaemSchool.Database.ListGetters;

public sealed class ListQuery
{
    public required string Sql { get; init; }
    public required DynamicParameters Parameters { get; init; }
}