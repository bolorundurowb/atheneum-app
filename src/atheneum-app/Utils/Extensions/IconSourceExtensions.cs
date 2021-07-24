using System.Collections.Generic;
using Xamarin.Forms;

namespace atheneum_app.Utils.Extensions
{
    public class IconSourceExtensions : BaseSourceExtensions
    {
        private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

        protected override string ResourceFolder => "Icons";

        public override string ResourcePrefix => "ic";

        public override string ResourceSuffix => ".png";
    }
}
