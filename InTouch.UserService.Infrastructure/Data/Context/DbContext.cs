using System;
using System.Data;
using System.Threading.Tasks;

namespace InTouch.Infrastructure.Data;

public class DbContext : IDbContext, IDisposable
{
    private readonly IDbConnectionFactory _connectionFactory;
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private IUnitOfWork _unitOfWork;

    public DbContext(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IDbContextState State { get; private set; } = IDbContextState.Closed;

    public IDbConnection Connection =>
        _connection ?? (_connection = OpenConnection());

    private IDbConnection OpenConnection()
    {
        State = IDbContextState.Open;
        return _connectionFactory.CreateOpenConnection();
    }

    public IDbTransaction Transaction =>
        _transaction ?? (_transaction = Connection.BeginTransaction());

    public IUnitOfWork UnitOfWork =>
        _unitOfWork ?? (_unitOfWork = new UnitOfWork(Transaction));
    
    public async Task Commit()
    {
        try
        {
            await UnitOfWork.Commit();
            State = IDbContextState.Committed;
        }
        catch
        {
            Rollback();
            throw;
        }
        finally
        {
            Reset();
        }
    }

    public async Task Rollback()
    {
        try
        {
            await UnitOfWork.Rollback();
            State = IDbContextState.RolledBack;
        }
        finally
        {
            Reset();
        }
    }


    public void Dispose()
    {
        Commit();
    }
    
    private void Reset()
    {
        Connection?.Close();
        Connection?.Dispose();
        Transaction?.Dispose();

        _connection = null;
        _transaction = null;
        _unitOfWork = null;
    }
}