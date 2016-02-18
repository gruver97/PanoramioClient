using Windows.Devices.Geolocation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PanoramioClient.ViewModel
{
    public class MapViewModel : ViewModelBase, IMapViewModel
    {
        public MapViewModel()
        {
            LocationTappedCommand = new RelayCommand<BasicGeoposition>(position => { });
        }

        public RelayCommand<BasicGeoposition> LocationTappedCommand { get; }
    }
}