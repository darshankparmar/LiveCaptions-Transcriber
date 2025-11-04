using CsvHelper.Configuration.Attributes;

namespace LiveCaptionsTranscriber.models
{
    public class TranscriptionHistoryEntry
    {
        public required string Timestamp { get; set; }
        [Ignore]
        public required string TimestampFull { get; set; }
        public required string SourceText { get; set; }
        public required string TranslatedText { get; set; }
        public required string TargetLanguage { get; set; }
        public required string ApiUsed { get; set; }
    }
}
