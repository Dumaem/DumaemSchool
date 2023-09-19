using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DumaemSchool.Database;

public class NpgsqlConnectionProvider
{
    private readonly ApplicationContext _context;

    public NpgsqlConnectionProvider(ApplicationContext context)
    {
        _context = context;
    }
    
    public NpgsqlConnection GetConnection()
    {
        return _context.Database.GetDbConnection() as NpgsqlConnection ?? throw new NotSupportedException();
    }
}