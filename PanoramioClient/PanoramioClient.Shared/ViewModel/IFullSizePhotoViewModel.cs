using Windows.UI.Xaml.Media.Imaging;
using PanoramioClient.Enumerations;

namespace PanoramioClient.ViewModel
{
    public interface IFullSizePhotoViewModel
    {
        BitmapImage Source { get; set; }
        OrientationEnumeration CurrentOrientation { get; }
    }
}