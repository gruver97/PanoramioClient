using System;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Store;
using Windows.Devices.Sensors;
using Windows.UI.Core;
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
        private SimpleOrientationSensor _simpleOrientationSensor;

        public FullSizePhotoViewModel([Dependency] INavigationService navigationService)
        {
            _navigationService = navigationService;
#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            _simpleOrientationSensor = SimpleOrientationSensor.GetDefault();
            if (_simpleOrientationSensor != null)
            {
                _simpleOrientationSensor.OrientationChanged += FullSizePhotoViewModel_OrientationChanged;
                SetOrientationState(_simpleOrientationSensor.GetCurrentOrientation());
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

        private async void SetOrientationState(SimpleOrientation orientation)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, ()=>
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
            });
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _navigationService.GoBack();
            _simpleOrientationSensor.OrientationChanged -= FullSizePhotoViewModel_OrientationChanged;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            e.Handled = true;
        }

#endif
    }
}