using System;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class AuthTokenService
    {
        private const string AuthTokenKey = "Atheneum_Token";
        private const string AuthExpiryKey = "Atheneum_Expiry";
        
        public void Logout()
        {
            Preferences.Clear();
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

        public void SetToken(string token)
        {
            var today = DateTime.UtcNow;
            var expiresAt = today.AddDays(30);
            
            Preferences.Set(AuthTokenKey, token);
            Preferences.Set(AuthExpiryKey, expiresAt);
        }
    }
}