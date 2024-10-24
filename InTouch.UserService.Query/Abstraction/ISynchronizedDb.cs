using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InTouch.UserService.Query;

public interface ISynchronizedDb
{
    Task UpsertAsync<TQueryModel>(TQueryModel queryModel, Expression<Func<TQueryModel,bool>> upsertFilter)
        where TQueryModel : IQueryModel;

    Task DeleteAsync<TQueryModel>(Expression<Func<TQueryModel, bool>> deleteFilter)
        where TQueryModel : IQueryModel;
}