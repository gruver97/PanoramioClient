using Windows.Devices.Sensors;
#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif

using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using PanoramioClient.Enumerations;

namespace PanoramioClient.ViewModel
{
    public class FullSizePhotoViewModel : ViewModelBase, IFullSizePhotoViewModel
    {
        private readonly INavigationService _navigationService;
        private OrientationEnumeration _currentOrientation;
        private BitmapImage _source;

        public FullSizePhotoViewModel([Dependency] INavigationService navigationService)
        {
            _navigationService = navigationService;
#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (SimpleOrientationSensor.GetDefault() != null)
            {
                SimpleOrientationSensor.GetDefault().OrientationChanged += FullSizePhotoViewModel_OrientationChanged;
                SetOrientationState(SimpleOrientationSensor.GetDefault().GetCurrentOrientation());
            }
#endif
        }

        public BitmapImage Source
        {
            get { return _source; }
            set
            {
                if (_source == value) return;
                _source = value;
                RaisePropertyChanged();
            }
        }

        public OrientationEnumeration CurrentOrientation
        {
            get { return _currentOrientation; }
            private set
            {
                if (_currentOrientation == value) return;
                _currentOrientation = value;
                RaisePropertyChanged();
            }
        }

#if WINDOWS_PHONE_APP
        private void FullSizePhotoViewModel_OrientationChanged(SimpleOrientationSensor sender,
            SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            SetOrientationState(args.Orientation);
        }

        private void SetOrientationState(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.NotRotated:
                    CurrentOrientation = OrientationEnumeration.Portrait;
                    break;
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    CurrentOrientation = OrientationEnumeration.Landscape;
                    break;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    CurrentOrientation = OrientationEnumeration.Portrait;
                    break;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    CurrentOrientation = OrientationEnumeration.Landscape;
                    break;
            }
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _navigationService.GoBack();
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            e.Handled = true;
        }

#endif
    }
}