using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public class TokenService
    {
        private const string DateFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz";
        private const string AuthTokenKey = "Atheneum_Token";
        private const string AuthExpiryKey = "Atheneum_Expiry";
        private const string AuthEmailKey = "Atheneum_Email";
        private const string AuthFirstNameKey = "Atheneum_FirstName";
        private const string AuthLastNameKey = "Atheneum_LastName";

        public async Task<bool> IsLoggedIn()
        {
            var (token, expiresAt) = await GetAuthToken();
            ;

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return expiresAt > DateTime.UtcNow;
        }

        public (string, string) GetUserDetails()
        {
            var firstName = Preferences.Get(AuthFirstNameKey, string.Empty);
            var lastName = Preferences.Get(AuthLastNameKey, string.Empty);
            return (firstName, lastName);
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

        public static async Task<(string, DateTime)> GetAuthToken()
        {
            try
            {
                var token = await SecureStorage.GetAsync(AuthTokenKey);
                var expiryString = await SecureStorage.GetAsync(AuthExpiryKey);
                DateTime.TryParseExact(expiryString, DateFormat, null, DateTimeStyles.AssumeUniversal, out var expiry);
                return (token, expiry);
            }
            catch (Exception)
            {
                return (null, DateTime.MinValue);
            }
        }

        public static async Task SetAuthToken(string token)
        {
            try
            {
                await SecureStorage.SetAsync(AuthTokenKey, token);
                await SecureStorage.SetAsync(AuthExpiryKey, DateTime.UtcNow.AddDays(364).ToString(DateFormat));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void ResetAuthToken()
        {
            try
            {
                SecureStorage.Remove(AuthTokenKey);
                SecureStorage.Remove(AuthExpiryKey);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void SetUserDetails(string firstName, string lastName, string emailAddress)
        {
            Preferences.Set(AuthFirstNameKey, firstName);
            Preferences.Set(AuthLastNameKey, lastName);
            Preferences.Set(AuthEmailKey, emailAddress);
        }
    }
}
