using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StellarChat.Shared.Abstractions.Pagination;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo.Repositories;

internal class MongoRepository<TDocument, TIdentifiable> : IMongoRepository<TDocument, TIdentifiable>
        where TDocument : IIdentifiable<TIdentifiable>
{
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<TDocument>(collectionName);
    }

    public IMongoCollection<TDocument> Collection { get; }

    public IQueryable<TDocument> AsQueryable() => Collection.AsQueryable();

    public Task<TDocument> GetAsync(TIdentifiable id)
        => GetAsync(e => e.Id.Equals(id));

    public Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate)
        => Collection.Find(predicate).SingleOrDefaultAsync();

    public async Task<IReadOnlyList<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate)
        => await Collection.Find(predicate).ToListAsync();

    public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression) 
        => Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();

    public Task<Paged<TDocument>> BrowseAsync<TQuery>(Expression<Func<TDocument, bool>> predicate,
        TQuery query) where TQuery : IPagedQuery
            => Collection.AsQueryable().Where(predicate).PaginateAsync(query);

    public Task AddAsync(TDocument document)
        => Collection.InsertOneAsync(document);

    public async Task AddManyAsync(ICollection<TDocument> documents)
        => await Collection.InsertManyAsync(documents);

    public Task UpdateAsync(TDocument document)
        => UpdateAsync(document, e => e.Id.Equals(document.Id));

    public Task UpdateAsync(TDocument document, Expression<Func<TDocument, bool>> predicate)
        => Collection.ReplaceOneAsync(predicate, document);

    public Task DeleteAsync(TIdentifiable id)
        => DeleteAsync(e => e.Id.Equals(id));

    public Task DeleteAsync(Expression<Func<TDocument, bool>> predicate)
        => Collection.DeleteOneAsync(predicate);

    public Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate)
        => Collection.Find(predicate).AnyAsync();

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression) 
        => await Collection.DeleteManyAsync(filterExpression);
}
