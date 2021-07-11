using System.Text.Json.Serialization;

namespace atheneum_app.Library.Models.View
{
    public class UserViewModel
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}