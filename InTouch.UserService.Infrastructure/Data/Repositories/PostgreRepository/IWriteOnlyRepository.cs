using Ardalis.Result;
using InTouch.UserService.Core;
using InTouch.UserService.Domain;

namespace InTouch.Infrastructure.Data;

public interface IWriteOnlyRepository
{
    Task<Guid> AddAsync(BaseEntity baseEntity);
}