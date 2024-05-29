namespace StellarChat.Server.Api.Features.Actions.GetNativeAction;

internal sealed class GetNativeActionHandler : IQueryHandler<GetNativeAction, NativeActionResponse>
{
    private readonly INativeActionRepository _nativeActionRepository;

    public GetNativeActionHandler(INativeActionRepository nativeActionRepository) 
        => _nativeActionRepository = nativeActionRepository;

    public async ValueTask<NativeActionResponse> Handle(GetNativeAction query, CancellationToken cancellationToken)
        => (await _nativeActionRepository.GetAsync(query.Id))
            .Adapt<NativeActionResponse>() ?? throw new NativeActionNotFoundException(query.Id);
}
