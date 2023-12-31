﻿using Dapper;

namespace DumaemSchool.Database.ListGetters;

public sealed class ListQuery
{
    public required string SelectSql { get; init; }
    public required string CountSql { get; init; }
    public required DynamicParameters Parameters { get; init; }
}