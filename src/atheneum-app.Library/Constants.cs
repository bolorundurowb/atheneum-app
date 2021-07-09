namespace atheneum_app.Library
{
    public static class Constants
    {
        private const string BaseUrl = "https://atheneum01.herokuapp.com";

        public static string V1BaseUrl = $"{BaseUrl}/v1";

        public const int ResetCodeLength = 6;

        public const string DefaultBookCoverUrl =
            "https://res.cloudinary.com/dg2dgzbt4/image/upload/v1625842121/external_assets/open_source/icons/default-book-cover.png";
    }
}