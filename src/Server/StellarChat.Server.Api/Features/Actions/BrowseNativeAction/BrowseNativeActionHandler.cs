namespace StellarChat.Server.Api.Features.Actions.BrowseNativeAction;

internal sealed class BrowseNativeActionHandler : IQueryHandler<BrowseNativeActions, IEnumerable<NativeActionResponse>>
{
    private readonly INativeActionRepository _nativeActionRepository;

    public BrowseNativeActionHandler(INativeActionRepository nativeActionRepository) 
        => _nativeActionRepository = nativeActionRepository;

    public async ValueTask<IEnumerable<NativeActionResponse>> Handle(BrowseNativeActions query, CancellationToken cancellationToken)
        => (await _nativeActionRepository.BrowseAsync())
                .Adapt<IEnumerable<NativeActionResponse>>();
}
