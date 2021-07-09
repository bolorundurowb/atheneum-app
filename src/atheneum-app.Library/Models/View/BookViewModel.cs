using System.Collections.Generic;
using Refit;

namespace atheneum_app.Library.Models.View
{
    public class BookViewModel
    {
        [AliasAs("_id")]
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Isbn { get; set; }

        public string Isbn13 { get; set; }

        public string CoverArt { get; set; }

        public int? PublishYear { get; set; }

        public bool IsAvailable { get; set; }

        public List<BorrowingHistoryItem> BorrowingHistory { get; set; }
    }
}