using Refit;

namespace atheneum_app.Library.Models.View
{
    public class AuthorViewModel
    {
        [AliasAs("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}