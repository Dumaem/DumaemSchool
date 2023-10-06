using DumaemSchool.Core.DataManipulation;

namespace DumaemSchool.Database.ListGetters;

public interface IListSqlGenerator<T> where T : class
{
    public ListQuery GetListSql(ListParam param);
}