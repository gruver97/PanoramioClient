using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using PanoramioClient.Enumerations;
using PanoramioClient.Services;

namespace PanoramioClient.ViewModel
{
    public class PushpinViewModel : ViewModelBase, IPushpinViewModel
    {
        private readonly IPanoramioService _panoramioService;
        private readonly INavigationService _navigationService;
        private LoadingStatesEnumeration _loadingStates;

        public PushpinViewModel([Dependency] IPanoramioService panoramioService, [Dependency] INavigationService navigationService)
        {
            _panoramioService = panoramioService;
            _navigationService = navigationService;
            ThumbnailsImages = new ObservableCollection<BitmapImage>();
            NavigateToFullViewCommand = new RelayCommand<BitmapImage>(NavigateToFullView);
        }

        private void NavigateToFullView(BitmapImage navigationParameter)
        {
            _navigationService.NavigateTo("FullSizePhotoPage",navigationParameter);
        }

        public ObservableCollection<BitmapImage> ThumbnailsImages { get; }

        public async Task LoadImagesAsync(BasicGeoposition location)
        {
            try
            {
                LoadingStates = LoadingStatesEnumeration.Loading;
                var bounding = new BoundingBox(location, 0.01, 0.01);
                var result = await DownloadImagesAsync(bounding).ConfigureAwait(true);
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
            catch (Exception)
            {
                LoadingStates = LoadingStatesEnumeration.Error;
            }
        }

        private async Task<IEnumerable<string>> DownloadImagesAsync(BoundingBox bounding)
        {
            return await
                _panoramioService.GetImagesUrlAsync(bounding.MinX, bounding.MaxX, bounding.MinY, bounding.MaxY)
                    .ConfigureAwait(false);
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

        public RelayCommand<BitmapImage> NavigateToFullViewCommand { get; private set; }
    }
}