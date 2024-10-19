using System;
using System.Data;
using System.Threading.Tasks;

namespace InTouch.Infrastructure.Data;

public class UnitOfWork(IDbTransaction transaction) : IUnitOfWork
{
    public IUnitOfWorkState Sate { get; private set; } = IUnitOfWorkState.Open;
    public IDbTransaction Transaction { get; private set; } = transaction;

    public async Task Commit()
    {
        try
        {
            await Task.Run(() =>
            {
                Transaction.Commit();
            });
            Sate = IUnitOfWorkState.Committed;
            return;
        }
        catch (Exception e)
        {
            Transaction.Rollback();
            throw;
        }
    }

    public void Rollback()
    {
        Transaction.Rollback();
        Sate = IUnitOfWorkState.RolledBack;
    }
}