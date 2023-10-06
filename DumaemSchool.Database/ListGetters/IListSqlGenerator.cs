using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping;

namespace DumaemSchool.Database.ListGetters;

public interface IListSqlGenerator<T> where T : class
{
    public IEntityMapping<T> Mapping { get; }
    public ListQuery GetListSql(ListParam param);
}