using System.Text.Json.Serialization;

namespace AtheneumApp.Library.Models.View
{
    public class AuthorViewModel
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}