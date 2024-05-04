namespace StellarChat.Client.Web.Shared;

public class Paged<T> : PagedBase
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public bool Empty => Items is null || !Items.Any();
    public bool HasNextPage => CurrentPage * ResultsPerPage < TotalResults;
    public bool HasPreviousPage => CurrentPage > 1;

    public Paged()
    {
        TotalPages = 1;
    }

    public Paged(IReadOnlyList<T> items,
        int currentPage, int resultsPerPage,
        int totalPages, long totalResults) :
        base(currentPage, resultsPerPage, totalPages, totalResults)
    {
        Items = items;
    }

    public static Paged<T> Create(IReadOnlyList<T> items,
        int currentPage, int resultsPerPage,
        int totalPages, long totalResults)
        => new(items, currentPage, resultsPerPage, totalPages, totalResults);

    public static Paged<T> From(PagedBase result, IReadOnlyList<T> items)
        => new(items, result.CurrentPage, result.ResultsPerPage,
            result.TotalPages, result.TotalResults);

    public static Paged<T> AsEmpty => new();

    public Paged<TResult> Map<TResult>(Func<T, TResult> map)
        => Paged<TResult>.From(this, Items.Select(map).ToList());
}
