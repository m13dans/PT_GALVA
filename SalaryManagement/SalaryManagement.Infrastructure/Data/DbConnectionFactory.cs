using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SalaryManagement.Infrastructure.Data;

public class DbConnectionFactory
{
    private const string _defaultConnection =
        "Data Source=.;Database=PT_Galva_SalaryManagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;";
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SQLConnection") ?? _defaultConnection;

    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
