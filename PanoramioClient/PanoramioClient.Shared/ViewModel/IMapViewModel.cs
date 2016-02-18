using Windows.Devices.Geolocation;
using GalaSoft.MvvmLight.Command;

namespace PanoramioClient.ViewModel
{
    public interface IMapViewModel
    {
        RelayCommand<BasicGeoposition> LocationTappedCommand { get; }
    }
}