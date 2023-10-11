using System.Data;
using Dapper;

namespace DumaemSchool.Database.TypeHandlers;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        if (value is not DateTime dateTime)
            throw new InvalidCastException();

        return DateOnly.FromDateTime(dateTime);
    }
}