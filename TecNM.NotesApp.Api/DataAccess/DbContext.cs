using System.Data.Common;
using MySqlConnector;
using TecNM.NotesApp.Api.DataAccess.Interfaces;

namespace TecNM.NotesApp.Api.DataAccess;

public class DbContext: IDbContext
{
    private readonly string _connectionString = "server=localhost; " +
                                                "user=root; pwd=root; " +
                                                "database=NotesApp;" +
                                                "port=3306";
    private MySqlConnection _connection;
    public DbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
            }

            return _connection;
        }
    }
}