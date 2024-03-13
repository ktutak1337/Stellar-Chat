namespace StellarChat.Shared.Abstractions.Pagination;

public interface IPagedQuery
{
    int Page { get; set; }
    int PageSize { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery { }
