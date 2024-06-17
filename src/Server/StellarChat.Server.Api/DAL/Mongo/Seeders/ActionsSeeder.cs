namespace StellarChat.Server.Api.DAL.Mongo.Seeders;

internal sealed class ActionsSeeder(ILogger<ActionsSeeder> logger, TimeProvider clock) : IActionsSeeder
{
    private readonly ILogger<ActionsSeeder> _logger = logger;
    private readonly TimeProvider _clock = clock;

    public async Task SeedAsync(IMongoDatabase database)
    {
        var settingsCollection = database.GetCollection<NativeActionDocument>("actions");
        var cursor = await settingsCollection.FindAsync(FilterDefinition<NativeActionDocument>.Empty);
        var documents = await cursor.ToListAsync();

        if (documents.Any())
        {
            return;
        }

        var now = _clock.GetLocalNow();

        string metaprompt = @"[SUMMARIZATION RULES]
DONT WASTE WORDS
USE SHORT, CLEAR, COMPLETE SENTENCES.
DO NOT USE BULLET POINTS OR DASHES.
USE ACTIVE VOICE.
MAXIMIZE DETAIL, MEANING
FOCUS ON THE CONTENT

[BANNED PHRASES]
This article
This document
This page
This material
[END LIST]

Summarize:
Hello how are you?
+++++
Hello";

        var document = new NativeActionDocument
        {
            Id = Guid.NewGuid(),
            Name = "Summarize",
            Category = "Text",
            Icon = "fas fa-compress-arrows-alt",
            Model = "gpt-3.5-turbo",
            Metaprompt = metaprompt,
            IsRemoteAction = false,
            ShouldRephraseResponse = false,
            Webhook = new WebhookDocument
            {
                Id = Guid.NewGuid(),
                HttpMethod = "POST",
                Url = string.Empty,
                Payload = string.Empty,
                IsRetryEnabled = false,
                RetryCount = 3,
                RetryInterval = 10,
                IsScheduled = false,
                CronExpression = string.Empty,
                Headers = []
            },
            CreatedAt = now,
            UpdatedAt = now,
        };

        _logger.LogInformation("Started seeding 'actions' collection.");

        await settingsCollection.InsertOneAsync(document);

        _logger.LogInformation($"Added a action to the database with 'ID': {document.Id}, and 'Name': {document.Name}.");
        _logger.LogInformation("Finished seeding 'actions' collection.");
    }
}
