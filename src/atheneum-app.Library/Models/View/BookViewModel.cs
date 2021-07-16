using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

namespace atheneum_app.Library.Models.View
{
    public class BookViewModel
    {
        [JsonPropertyName("_id")] 
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Isbn { get; set; }

        public string Isbn13 { get; set; }

        public string CoverArt { get; set; }

        public int? PublishYear { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime? CreatedAt { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public List<BorrowingHistoryItem> BorrowingHistory { get; set; }

        public string PrimaryAuthorName => Authors?.FirstOrDefault()?.Name;

        public string PublisherName => Publisher?.Name;

        public string Availability => IsAvailable ? "Available" : "Lent Out";

        public string AllAuthorNames => string.Join(", ", Authors?.Select(x => x.Name) ?? Enumerable.Empty<string>());

        public string CreatedAtText => CreatedAt.HasValue ? CreatedAt.Value.ToString(CultureInfo.InvariantCulture) : "N/A";
    }
}