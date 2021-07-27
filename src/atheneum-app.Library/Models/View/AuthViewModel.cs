namespace atheneum_app.Library.Models.View
{
    public class AuthViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string AuthToken { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailVerified { get; set; }
    }
}
