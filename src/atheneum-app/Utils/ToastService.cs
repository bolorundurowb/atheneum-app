using Acr.UserDialogs;
using Xamarin.Forms;

namespace atheneum_app.Utils
{
    internal static class ToastService
    {
        public static void Error(string message)
        {
            Display(message, "#AA0B0F");
        }
        
        public static void Success(string message)
        {
            Display(message, "#4D9947");
        }
        
        public static void Info(string message)
        {
            Display(message, "#49ABE8");
        }

        private static void Display(string message, string colorHex)
        {
            UserDialogs.Instance.Toast(new ToastConfig(message)
            {
                BackgroundColor = Color.FromHex(colorHex)
            });
        }
    }
}
