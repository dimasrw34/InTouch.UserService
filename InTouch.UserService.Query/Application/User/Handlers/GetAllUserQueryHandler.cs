using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using InTouch.UserService.Core;
using InTouch.UserService.Query.Data.Repositories.Abstractions;
using MediatR;

namespace InTouch.UserService.Query.Application.User.Handlers;

public class GetAllUserQueryHandler(IUserReadOnlyRepository repository, ICacheService cacheService)
    : IRequestHandler<GetAllUserQuery, Result<IEnumerable<UserQueryModel>>>
{
    private const string CacheKey = nameof(GetAllUserQuery);
    
    public async Task<Result<IEnumerable<UserQueryModel>>> Handle(
        GetAllUserQuery request, 
        CancellationToken cancellationToken)
    {
        return Result<IEnumerable<UserQueryModel>>.Success(
            await cacheService.GetOrCreateAsync(CacheKey, repository.GetAllAsync));
    }
}