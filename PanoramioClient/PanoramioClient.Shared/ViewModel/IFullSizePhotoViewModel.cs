using Windows.UI.Xaml.Media.Imaging;

namespace PanoramioClient.ViewModel
{
    public interface IFullSizePhotoViewModel
    {
        BitmapImage Source { get; set; }
    }
}