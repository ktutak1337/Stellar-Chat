using MongoDB.Driver;
using StellarChat.Shared.Abstractions.Pagination;
using System.Linq.Expressions;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public interface IMongoRepository<TDocument, in TIdentifiable> where TDocument : IIdentifiable<TIdentifiable>
{
    IMongoCollection<TDocument> Collection { get; }
    IQueryable<TDocument> AsQueryable();
    Task<TDocument> GetAsync(TIdentifiable id);
    Task<TDocument> GetAsync(Expression<Func<TDocument, bool>> predicate);
    Task<IReadOnlyList<TDocument>> FindAsync(Expression<Func<TDocument, bool>> predicate);
    IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);
    Task<Paged<TDocument>> BrowseAsync<TQuery>(Expression<Func<TDocument, bool>> predicate, TQuery query) where TQuery : IPagedQuery;
    Task AddAsync(TDocument document);
    Task AddManyAsync(ICollection<TDocument> documents);
    Task UpdateAsync(TDocument document);
    Task UpdateAsync(TDocument document, Expression<Func<TDocument, bool>> predicate);
    Task DeleteAsync(TIdentifiable id);
    Task DeleteAsync(Expression<Func<TDocument, bool>> predicate);
    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> predicate);
}
