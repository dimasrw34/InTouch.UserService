using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Polly;
using Polly.Retry;

namespace InTouch.UserService.Query.Data;

public sealed class NoSqlDbContext : ISynchronizedDb, IReadDbContext
{
    #region Constructors

    private const string DatabaseName = "Users";
    private const int RetryCount = 2;

    private static readonly ReplaceOptions DefaultReplaceOptions = new()
    {
        IsUpsert = true
    };

    private static readonly CreateIndexOptions DefaultCreateIndexOptions = new()
    {
        Unique = true,
        Sparse = true
    };

    private readonly IMongoDatabase _database;
    private readonly ILogger<NoSqlDbContext> _logger;
    private readonly AsyncRetryPolicy _mongoRetryPolicy;

    public NoSqlDbContext(IOptions<ConnectionString> options, ILogger<NoSqlDbContext> logger)
    {
        ConnectionString = "";

        var mongoClient = new MongoClient("");
        _database = mongoClient.GetDatabase(DatabaseName);
        _logger = logger;
        _mongoRetryPolicy = CreateRetryPolicy(logger);
    }
    
    #endregion Constructors
    
    public Task UpsertAsync<TQueryModel>(TQueryModel queryModel, Expression<Func<TQueryModel, bool>> upsertFilter) where TQueryModel : IQueryModel
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync<TQueryModel>(Expression<Func<TQueryModel, bool>> deleteFilter) where TQueryModel : IQueryModel
    {
        throw new NotImplementedException();
    }

    public string ConnectionString { get; }
    public IMongoCollection<TQueryModel> GetCollection<TQueryModel>() where TQueryModel : IQueryModel
    {
        throw new NotImplementedException();
    }

    public Task CreateCollectionAsync()
    {
        throw new NotImplementedException();
    }

    private static AsyncRetryPolicy CreateRetryPolicy(ILogger logger)
    {
        return Policy
            .Handle<MongoException>()
            .WaitAndRetryAsync(RetryCount, SleepDurationProvider);

        void OnRetry(Exception ex, TimeSpan _) =>
            logger.LogError(ex, "An unexpected exception occurred while saving to MongoDB: {Message}", ex.Message);

        TimeSpan SleepDurationProvider(int retryAttempt)
        {
            // Retry with jitter
            // A well-known retry strategy is exponential backoff, allowing retries to be made initially quickly,
            // but then at progressively longer intervals: for example, after 2, 4, 8, 15, then 30 seconds.
            // REF: https://github.com/App-vNext/Polly/wiki/Retry-with-jitter#simple-jitter
            var sleepDuration =
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(Random.Shared.Next(0, 1000));

            logger.LogWarning("----- MongoDB: Retry #{Count} with delay {Delay}", retryAttempt, sleepDuration);

            return sleepDuration;
        }
    }
}