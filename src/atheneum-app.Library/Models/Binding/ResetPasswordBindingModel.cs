namespace AtheneumApp.Library.Models.Binding
{
    public class ResetPasswordBindingModel
    {
        public string EmailAddress { get; set; }

        public string ResetCode { get; set; }

        public string Password { get; set; }
    }
}