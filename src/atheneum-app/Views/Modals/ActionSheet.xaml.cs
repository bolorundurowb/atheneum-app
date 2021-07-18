using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionSheet : Popup
    {
        public ActionSheet()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}