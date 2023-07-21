using Refit;

namespace AtheneumApp.Library.Models.View
{
    public class WishListViewModel
    {
        [AliasAs("_id")]
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string AuthorName { get; set; }
        
        public string CoverArt { get; set; }
        
        public string Isbn { get; set; }
    }
}