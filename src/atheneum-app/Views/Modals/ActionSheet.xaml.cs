using System;
using atheneum_app.Library.Enums;
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

        private void Scan(object sender, EventArgs e)
        {
            Dismiss(ActionType.ByScan);
        }

        private void Isbn(object sender, EventArgs e)
        {
            Dismiss(ActionType.ByIsbn);
        }

        private void Manual(object sender, EventArgs e)
        {
            Dismiss(ActionType.Manual);
        }

        private void Cancel(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}