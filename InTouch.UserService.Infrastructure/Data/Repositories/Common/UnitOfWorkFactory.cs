

namespace InTouch.Infrastructure;

public class UnitOfWorkFactory (IDbConfig config, IConnectionFactory connectionFactory): IUnitOfWorkFactory
{
    private readonly IDbConfig _config = config;

    
    public  IUnitOfWork CreateUnitOfWork()
    {
        var connection = connectionFactory.GetConnection();
        return new UnitOfWork(connection);
    }
}