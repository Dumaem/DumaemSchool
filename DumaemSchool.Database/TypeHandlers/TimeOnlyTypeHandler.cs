using System.Data;
using Dapper;

namespace DumaemSchool.Database.TypeHandlers;

public sealed class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = value.ToTimeSpan();
    }

    public override TimeOnly Parse(object value)
    {
        if (value is not TimeSpan timeSpan)
            throw new InvalidCastException();
        
        return TimeOnly.FromTimeSpan(timeSpan);
    }
}