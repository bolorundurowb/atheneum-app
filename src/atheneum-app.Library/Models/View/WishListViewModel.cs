using Refit;

namespace atheneum_app.Library.Models.View
{
    public class WishListViewModel
    {
        [AliasAs("_id")]
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string AuthorName { get; set; }
        
        public string Isbn { get; set; }
    }
}