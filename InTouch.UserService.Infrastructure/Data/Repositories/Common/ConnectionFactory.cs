using System.Data;
using System.Data.Common;

namespace InTouch.Infrastructure;

public class ConnectionFactory(IDbConfig config): IConnectionFactory
{
    private readonly IDbConfig _config = config; 

    public IDbConnection GetConnection()
    {
          var _factory = DbProviderFactories.GetFactory(_config.ProviderName);
                var _connection = _factory.CreateConnection();
                _connection.ConnectionString = _config.ConnectionString;
                return _connection;
    }
}