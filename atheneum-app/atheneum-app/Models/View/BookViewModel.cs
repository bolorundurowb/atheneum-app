using System.Collections.Generic;

namespace atheneum_app.Models.View
{
    public class BookViewModel
    {
        public string _id { get; set; }

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