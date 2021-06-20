namespace atheneum_app.Library.Models.Binding
{
    public class CreateIsbnBookBindingModel
    {
        public string Isbn { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }
    }
}