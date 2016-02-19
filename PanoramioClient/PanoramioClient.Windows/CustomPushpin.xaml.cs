using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using PanoramioClient.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PanoramioClient
{
    public sealed partial class CustomPushpin : UserControl
    {
        private readonly BasicGeoposition _location;

        public CustomPushpin()
        {
            InitializeComponent();
        }

        public CustomPushpin(BasicGeoposition location) : this()
        {
            _location = location;
        }

        private async void CustomPushpin_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPushpinViewModel;
            if (viewModel != null)
            {
                await viewModel.LoadImagesAsync(_location).ConfigureAwait(true);
            }
        }

        private void LoadedImagesFlip_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}