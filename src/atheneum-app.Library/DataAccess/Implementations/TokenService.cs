using System;
using Xamarin.Essentials;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class TokenService
    {
        private const string AuthTokenKey = "Atheneum_Token";
        private const string AuthExpiryKey = "Atheneum_Expiry";
        private const string AuthLoggedInAtKey = "Atheneum_LoggedInAt";
        private const string AuthEmailKey = "Atheneum_Email";
        private const string AuthFirstNameKey = "Atheneum_FirstName";
        private const string AuthLastNameKey = "Atheneum_LastName";

        public void Logout()
        {
            Preferences.Clear(AuthTokenKey);
            Preferences.Clear(AuthExpiryKey);
            Preferences.Clear(AuthLoggedInAtKey);
        }

        public bool IsLoggedIn()
        {
            var token = GetToken();
            var expiresAt = Preferences.Get(AuthExpiryKey, DateTime.MinValue);

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return expiresAt > DateTime.UtcNow;
        }

        public string GetToken()
        {
            return Preferences.Get(AuthTokenKey, null);
        }

        public string GetEmail()
        {
            return Preferences.Get(AuthEmailKey, null);
        }

        public DateTime GetExpiry()
        {
            return Preferences.Get(AuthExpiryKey, DateTime.MinValue);
        }

        public DateTime GetLogin()
        {
            return Preferences.Get(AuthLoggedInAtKey, DateTime.MinValue);
        }

        public void SetAuth(string firstName, string lastName, string emailAddress, string token)
        {
            var expiresAt = DateTime.UtcNow.AddDays(7);

            Preferences.Set(AuthFirstNameKey, firstName);
            Preferences.Set(AuthLastNameKey, lastName);
            Preferences.Set(AuthEmailKey, emailAddress);
            Preferences.Set(AuthTokenKey, token);
            Preferences.Set(AuthExpiryKey, expiresAt);
            Preferences.Set(AuthLoggedInAtKey, DateTime.UtcNow);
        }
    }
}
