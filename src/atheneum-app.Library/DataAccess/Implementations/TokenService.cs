using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace atheneum_app.Library.DataAccess.Implementations
{
    public static class TokenService
    {
        private const string DateFormat = "yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz";

        private const string AuthTokenKey = "Atheneum_Token";
        private const string AuthExpiryKey = "Atheneum_Expiry";
        private const string AuthFirstNameKey = "Atheneum_FirstName";
        private const string AuthLastNameKey = "Atheneum_LastName";
        private const string AuthEmailKey = "Atheneum_Email";

        public static async Task<bool> IsLoggedIn()
        {
            var token = await GetAuthToken();
            var expiresAt = await GetAuthExpiry();
            var isLoggedIn = !string.IsNullOrWhiteSpace(token) && expiresAt > DateTime.UtcNow;

            // expired auth, remove credentials
            if (!isLoggedIn)
            {
                ResetAuthToken();
            }

            return isLoggedIn;
        }

        public static (string, string) GetUserDetails()
        {
            var firstName = Preferences.Get(AuthFirstNameKey, string.Empty);
            var lastName = Preferences.Get(AuthLastNameKey, string.Empty);
            return (firstName, lastName);
        }

        public static string GetToken()
        {
            return Preferences.Get(AuthTokenKey, null);
        }

        public static string GetEmail()
        {
            return Preferences.Get(AuthEmailKey, null);
        }

        public static Task<string> GetAuthToken()
        {
            try
            {
                return SecureStorage.GetAsync(AuthTokenKey);
            }
            catch (Exception)
            {
                return Task.FromResult((string) null);
            }
        }

        public static async Task<DateTime> GetAuthExpiry()
        {
            try
            {
                var expiryString = await SecureStorage.GetAsync(AuthExpiryKey);
                DateTime.TryParseExact(expiryString, DateFormat, null, DateTimeStyles.AssumeUniversal, out var expiry);
                return expiry;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
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

        public static void ResetAuthToken()
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

        public static void SetUserDetails(string firstName, string lastName)
        {
            Preferences.Set(AuthFirstNameKey, firstName);
            Preferences.Set(AuthLastNameKey, lastName);
        }

        public static void SetEmail(string emailAddress)
        {
            Preferences.Set(AuthEmailKey, emailAddress);
        }
    }
}
