using System.Data;
using Dapper;
using InTouch.UserService.Core;

namespace InTouch.Infrastructure;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
{
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction ;

        public GenericRepository(IDbTransaction dbTransaction)
        {
                _dbConnection = _dbTransaction.Connection ?? default;
                _dbTransaction = dbTransaction;
        }
        
        public async Task InsertAsync(TEntity entity)
        {
                var sql = "select";
                await _dbConnection.QueryFirstOrDefaultAsync(sql, null);
        }
}