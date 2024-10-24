using System;
using System.Threading.Tasks;

namespace InTouch.UserService.Query;

/// <summary>
/// Интерфейс репозитория только для чтения
/// </summary>
/// <typeparam name="TQueryModel"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IReadOnlyRepository<TQueryModel, in TKey>
    where TQueryModel : IQueryModel<TKey>
    where TKey : IEquatable<TKey>
{
    Task<TQueryModel> GetByIdAsync(TKey id);
}