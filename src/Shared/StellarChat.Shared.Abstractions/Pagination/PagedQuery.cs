namespace StellarChat.Shared.Abstractions.Pagination;

public abstract class PagedQuery : IPagedQuery
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; }
    //public string OrderBy { get; set; } = string.Empty;
    //public string SortOrder { get; set; } = string.Empty;
}

public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>> { }
