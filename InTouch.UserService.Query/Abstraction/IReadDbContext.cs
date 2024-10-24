using System.Threading.Tasks;
using MongoDB.Driver;

namespace InTouch.UserService.Query;

public interface IReadDbContext
{
    string ConnectionString { get; }

    IMongoCollection<TQueryModel> GetCollection<TQueryModel>() where TQueryModel : IQueryModel;

    Task CreateCollectionAsync();
}