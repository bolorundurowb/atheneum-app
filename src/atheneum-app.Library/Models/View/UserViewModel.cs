using Refit;

namespace atheneum_app.Library.Models.View
{
    public class UserViewModel
    {
        [AliasAs("_id")]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}