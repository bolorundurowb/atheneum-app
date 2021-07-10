using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWishList : Popup
    {
        public AddWishList()
        {
            InitializeComponent();
        }
    }
}