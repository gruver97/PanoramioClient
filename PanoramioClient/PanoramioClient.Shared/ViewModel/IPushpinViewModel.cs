using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;

namespace PanoramioClient.ViewModel
{
    public interface IPushpinViewModel
    {
        ObservableCollection<BitmapImage> ThumbnailsImages { get; }
    }
}