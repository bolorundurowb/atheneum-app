using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Utils.Extensions
{
    [ContentProperty(nameof(Source))]
    public abstract class BaseSourceExtensions : IMarkupExtension
    {
        private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

        protected abstract string ResourceFolder { get; }

        public abstract string ResourcePrefix { get; }

        public string Source { get; set; }

        protected ImageSource GetImageSource(string value)
        {
            if (value == null)
            {
                return null;
            }

            var resourceKey = $"atheneum_app.Resources.{ResourceFolder}.{ResourcePrefix}" + value + ".png";

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
            return Source == null ? null : GetImageSource(Source);
        }
    }
}
