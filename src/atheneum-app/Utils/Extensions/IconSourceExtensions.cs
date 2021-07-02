using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Utils.Extensions
{
    [ContentProperty(nameof(Source))]
    public class IconSourceExtensions : IMarkupExtension
    {
        private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

        public string Source { get; set; }

        public static ImageSource GetImageSource(string value)
        {
            if (value == null)
            {
                return null;
            }

            var resourceKey = "atheneum_app.Resources.Icons." + value + ".png";

            if (Cache.ContainsKey(resourceKey))
            {
                return Cache[resourceKey];
            }

            var imageSource = ImageSource.FromResource(resourceKey);
            Cache.Add(resourceKey, imageSource);

            return imageSource;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            return GetImageSource(Source);
        }
    }
}