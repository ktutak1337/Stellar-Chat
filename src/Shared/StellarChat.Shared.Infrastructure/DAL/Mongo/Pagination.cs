using MongoDB.Driver;
using MongoDB.Driver.Linq;
using StellarChat.Shared.Abstractions.Pagination;
using StellarChat.Shared.Infrastructure.DAL.Mongo;

namespace StellarChat.Shared.Infrastructure.DAL.Mongo;

public static class Pagination
{
    public static async Task<Paged<T>> PaginateAsync<T>(this IMongoQueryable<T> collection, IPagedQuery query)
        => await collection.PaginateAsync(query.Page, query.PageSize);

    public static async Task<Paged<T>> PaginateAsync<T>(this IMongoQueryable<T> collection,
        int page, int resultsPerPage)
    {
        if (page <= 0)
        {
            page = 1;
        }
        if (resultsPerPage <= 0)
        {
            resultsPerPage = 10;
        }
        var isEmpty = await collection.AnyAsync() == false;
        if (isEmpty)
        {
            return Paged<T>.AsEmpty;
        }
        var totalResults = await collection.CountAsync();
        var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);
        var data = await collection.Limit(page, resultsPerPage).ToListAsync();

        return Paged<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
    }

    public static IMongoQueryable<T> Limit<T>(this IMongoQueryable<T> collection, IPagedQuery query)
        => collection.Limit(query.Page, query.PageSize);

    public static IMongoQueryable<T> Limit<T>(this IMongoQueryable<T> collection,
        int page, int resultsPerPage)
    {
        if (page <= 0)
        {
            page = 1;
        }
        if (resultsPerPage <= 0)
        {
            resultsPerPage = 10;
        }
        var skip = (page - 1) * resultsPerPage;
        var data = collection.Skip(skip)
            .Take(resultsPerPage);

        return data;
    }
}
