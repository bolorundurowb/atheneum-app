using System;

namespace AtheneumApp.Library.Models.View
{
    public class BorrowingHistoryItem
    {
        public string BorrowedBy { get; set; }

        public DateTimeOffset BorrowedAt { get; set; }

        public DateTimeOffset? ReturnedAt { get; set; }
    }
}