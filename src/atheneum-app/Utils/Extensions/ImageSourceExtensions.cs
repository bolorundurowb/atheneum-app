namespace AtheneumApp.Utils.Extensions
{
    public class ImageSourceExtensions : BaseSourceExtensions
    {
        protected override string ResourceFolder => "Images";

        public override string ResourcePrefix => "im";

        // we want the extension specifiable
        public override string ResourceSuffix => string.Empty;
    }
}
