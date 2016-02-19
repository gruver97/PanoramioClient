using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using PanoramioClient.Enumerations;
using PanoramioClient.Services;

namespace PanoramioClient.ViewModel
{
    public class PushpinViewModel : ViewModelBase, IPushpinViewModel
    {
        private readonly IPanoramioService _panoramioService;
        private LoadingStatesEnumeration _loadingStates;

        public PushpinViewModel([Dependency] IPanoramioService panoramioService)
        {
            _panoramioService = panoramioService;
            ThumbnailsImages = new ObservableCollection<BitmapImage>();
        }

        public ObservableCollection<BitmapImage> ThumbnailsImages { get; }

        public async Task LoadImagesAsync(BasicGeoposition location)
        {
            try
            {
                LoadingStates = LoadingStatesEnumeration.Loading;
                var bounding = new BoundingBox(location, 0.01, 0.01);
                var result =
                    await
                        _panoramioService.GetImagesUrlAsync(bounding.MinX, bounding.MaxX, bounding.MinY, bounding.MaxY)
                            .ConfigureAwait(true);
                if (result != null && result.Any())
                {
                    ThumbnailsImages.Clear();
                    foreach (var imageFile in result)
                    {
                        ThumbnailsImages.Add(new BitmapImage(new Uri(imageFile)));
                    }
                    LoadingStates = LoadingStatesEnumeration.Loaded;
                }
                else 
                LoadingStates = LoadingStatesEnumeration.Error;
            }
            catch (Exception e)
            {
                LoadingStates = LoadingStatesEnumeration.Error;

            }
        }

        public LoadingStatesEnumeration LoadingStates
        {
            get { return _loadingStates; }
            private set
            {
                if (_loadingStates == value) return;
                _loadingStates = value;
                RaisePropertyChanged();
            }
        }
    }
}