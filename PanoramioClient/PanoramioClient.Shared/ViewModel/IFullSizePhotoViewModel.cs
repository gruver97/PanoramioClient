using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using PanoramioClient.Enumerations;

namespace PanoramioClient.ViewModel
{
    public interface IFullSizePhotoViewModel
    {
        BitmapImage Source { get; set; }
        OrientationEnumeration CurrentOrientation { get; }
#if WINDOWS_APP
        RelayCommand GoBackCommand { get; }
#endif
    }
}