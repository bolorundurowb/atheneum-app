namespace atheneum_app.Library
{
    public static class Constants
    {
        private const string ProdBaseUrl = "https://atheneum01.herokuapp.com";

        private const string StagingBaseUrl = "https://atheneum-staging.herokuapp.com";

#if DEBUG
        public static string V1BaseUrl = $"{StagingBaseUrl}/v1";
#else
        public static string V1BaseUrl = $"{ProdBaseUrl}/v1";
#endif

        public const int ResetCodeLength = 6;
    }
}
