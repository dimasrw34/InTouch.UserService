using System.Data;

namespace InTouch.Infrastructure;

public interface IConnectionFactory
{
    IDbConnection GetConnection();
}