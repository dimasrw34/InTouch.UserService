using System.Data;
using Npgsql;

namespace InTouch.Infrastructure.Data;

public class NpgsqlConnectionAsync(NpgsqlConnection connection) : IDbConnectionAsync
{
    private readonly NpgsqlConnection _connection = connection;

    #region Реализация IDbConnetction

    public void Dispose() => _connection.Dispose();

    public IDbTransaction BeginTransaction() =>
        _connection.BeginTransaction();

    public IDbTransaction BeginTransaction(IsolationLevel il) =>
        _connection.BeginTransaction(il);

    public void ChangeDatabase(string databaseName) =>
        _connection.ChangeDatabase(databaseName);

    public void Close() =>
        _connection.Close();

    public IDbCommand CreateCommand() =>
        _connection.CreateCommand();

    public void Open() => _connection.Open();

    public string ConnectionString
    {
        get => _connection.ConnectionString;
        set => _connection.ConnectionString = value;
    }

    public int ConnectionTimeout
    {
        get => _connection.ConnectionTimeout;
    }

    public string Database
    {
        get => _connection.Database; 
    }

    public ConnectionState State
    {
        get => _connection.State; 
    }

    #endregion

    #region Реализация IDbConnectionAsync

    public async Task OpenAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
        await _connection.OpenAsync(cancellationToken);
    public async Task CloseAsync() =>
        await _connection.CloseAsync();

    public async ValueTask<NpgsqlTransactionAsync> BeginTransactionAsync(
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await Task.Run(() =>
        {
            return new NpgsqlTransactionAsync(_connection.BeginTransactionAsync(IsolationLevel.ReadCommitted));
        }, cancellationToken);
    }
    #endregion
}