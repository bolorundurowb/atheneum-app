namespace atheneum_app.Models.Binding
{
    public class CreateManualBookBindingModel
    {
        public string Title { get; set; }

        public string Summary { get; set; }
        
        public string Isbn { get; set; }

        public string Isbn13 { get; set; }
        
        public int? PublishYear { get; set; }

        public string[] AuthorIds { get; set; }
        
        public string PublisherId { get; set; }

        public string CoverArt { get; set; }

        public int? PageCount { get; set; }
        
        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}