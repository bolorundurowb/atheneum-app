using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AtheneumApp.Utils.Extensions
{
    [ContentProperty(nameof(Source))]
    public abstract class BaseSourceExtensions : IMarkupExtension
    {
        private static readonly Dictionary<string, ImageSource> Cache = new Dictionary<string, ImageSource>();

        protected abstract string ResourceFolder { get; }

        public abstract string ResourcePrefix { get; }

        public abstract string ResourceSuffix { get; }

        public string Source { get; set; }

        protected ImageSource GetImageSource(string value)
        {
            if (value == null) 
                return null;

            var resourceKey = $"AtheneumApp.Resources.{ResourceFolder}.{ResourcePrefix}_{value}{ResourceSuffix}";

            if (Cache.TryGetValue(resourceKey, out var source)) 
                return source;

            var imageSource = ImageSource.FromResource(resourceKey);
            Cache.Add(resourceKey, imageSource);
            return imageSource;
        }

        public object ProvideValue(IServiceProvider serviceProvider) => Source == null ? null : GetImageSource(Source);
    }
}
