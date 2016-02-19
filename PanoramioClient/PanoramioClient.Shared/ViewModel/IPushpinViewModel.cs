using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace PanoramioClient.ViewModel
{
    public interface IPushpinViewModel
    {
        ObservableCollection<BitmapImage> ThumbnailsImages { get; }
        Task LoadImagesAsync(BasicGeoposition location);
    }
}