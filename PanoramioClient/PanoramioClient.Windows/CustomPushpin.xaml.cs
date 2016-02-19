using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bing.Maps;
using Windows.Devices.Geolocation;
using PanoramioClient.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PanoramioClient
{
    public sealed partial class CustomPushpin : UserControl
    {
        private BasicGeoposition _location;

        public CustomPushpin()
        {
            this.InitializeComponent();
        }

        public CustomPushpin(BasicGeoposition location):this()
        {
            this._location = location;
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private async void CustomPushpin_OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IPushpinViewModel;
            if (viewModel != null)
            {
                await viewModel.LoadImagesAsync(_location).ConfigureAwait(true);
            }
        }
    }
}
