using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using PanoramioClient.Services;

namespace PanoramioClient.ViewModel
{
    public class PushpinViewModel : ViewModelBase, IPushpinViewModel
    {
        private readonly IPanoramioService _panoramioService;
        private readonly Task _initializeTask;

        public PushpinViewModel([Dependency] IPanoramioService panoramioService)
        {
            _panoramioService = panoramioService;
            ThumbnailsImages = new ObservableCollection<BitmapImage>();
        }

        public ObservableCollection<BitmapImage> ThumbnailsImages { get; }
        public async Task LoadImagesAsync(BasicGeoposition location)
        {
            var bounding = new BoundingBox(location,0.3,0.3);
            var result = await _panoramioService.GetImagesUrlAsync(bounding.MinX, bounding.MaxX, bounding.MinY, bounding.MaxY).ConfigureAwait(true);
            if (result != null)
            {
                ThumbnailsImages.Clear();
                ThumbnailsImages.Add(new BitmapImage(new Uri(result)));
            }
        }
    }
}