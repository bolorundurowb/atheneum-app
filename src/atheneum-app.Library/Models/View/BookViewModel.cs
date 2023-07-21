using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AtheneumApp.Library.Models.View
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

        public int? PageCount { get; set; }

        public bool IsAvailable { get; set; }
        
        public string Source { get; set; }

        public DateTime? CreatedAt { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public PublisherViewModel Publisher { get; set; }

        public List<BorrowingHistoryItem> BorrowingHistory { get; set; }
        
        // GENERATED FIELDS
        public string PrimaryAuthorName => Authors?.FirstOrDefault()?.Name;

        public string PublisherName => Publisher?.Name;

        public string PublisherYearValue => PublishYear.HasValue ? PublishYear.Value.ToString() : "N/A";

        public string PageCountValue => PageCount.HasValue ? PageCount.Value.ToString() : "N/A";

        public string SourceValue => string.IsNullOrEmpty(Source) ?  "N/A" : Source;

        public string Availability => IsAvailable ? "Available" : "Lent Out";

        public string AllAuthorNames => string.Join(", ", Authors?.Select(x => x.Name) ?? Enumerable.Empty<string>());

        public string CreatedAtText => CreatedAt.HasValue ? CreatedAt.Value.ToString("dd MMM \\'yy h:mmtt") : "N/A";
    }
}
