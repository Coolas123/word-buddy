namespace Infrastructure.MassTransit.ViewModels
{
    public sealed class SaveWordContextRequest
    {
        public Guid DictionaryId { get; init; }
        public Guid DictionaryRowId { get; init; }
        public string WordContext { get; init; }

        public SaveWordContextRequest(Guid dictionaryId, Guid dictionaryRowId, string wordContext) {
            DictionaryId = dictionaryId;
            DictionaryRowId = dictionaryRowId;
            WordContext = wordContext;
        }
    }
}
