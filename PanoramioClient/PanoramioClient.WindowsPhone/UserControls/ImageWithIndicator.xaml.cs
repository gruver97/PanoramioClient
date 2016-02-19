using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PanoramioClient.UserControls
{
    public sealed partial class ImageWithIndicator : UserControl
    {
        public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(
            "ImageUri", typeof (BitmapImage), typeof (ImageWithIndicator), new PropertyMetadata(default(BitmapImage)));

        public ImageWithIndicator()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
        }

        public BitmapImage ImageUri
        {
            get { return (BitmapImage) GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        private void Image_OnImageOpened(object sender, RoutedEventArgs e)
        {
            var resutl = VisualStateManager.GoToState(this, "LoadedState", false);
        }

        private void Image_OnImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }
    }
}