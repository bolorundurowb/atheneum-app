namespace AtheneumApp.Library.Models.Binding
{
    public class CreateManualBookBindingModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Isbn { get; set; }

        public int? PublishYear { get; set; }

        public string Authors { get; set; }

        public string Publisher { get; set; }

        public int? PageCount { get; set; }
    }
}
