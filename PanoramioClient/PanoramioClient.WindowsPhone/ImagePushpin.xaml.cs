using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PanoramioClient.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PanoramioClient
{
    public sealed partial class ImagePushpin : UserControl
    {
        private BasicGeoposition _location;

        public ImagePushpin()
        {
            this.InitializeComponent();
        }

        public ImagePushpin(BasicGeoposition location):this()
        {
            this._location = location;
        }

        private async void ImagePushpin_OnLoaded(object sender, RoutedEventArgs e)
        {
            var pushpinViewModel = DataContext as IPushpinViewModel;
            if (pushpinViewModel != null)
                await pushpinViewModel.LoadImagesAsync(_location).ConfigureAwait(true);
        }
    }
}
