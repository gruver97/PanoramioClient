using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PanoramioClient.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PanoramioClient.Views
{
    public sealed partial class ImagePushpin : UserControl
    {
        private readonly BasicGeoposition _location;
        private bool _isLoaded = false;
        public ImagePushpin()
        {
            InitializeComponent();
        }

        public ImagePushpin(BasicGeoposition location) : this()
        {
            _location = location;
        }

        private async void ImagePushpin_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isLoaded) return;
            var pushpinViewModel = DataContext as IPushpinViewModel;
            if (pushpinViewModel != null)
            {
                _isLoaded = true;
                await pushpinViewModel.LoadImagesAsync(_location).ConfigureAwait(true);
            }
        }
    }
}