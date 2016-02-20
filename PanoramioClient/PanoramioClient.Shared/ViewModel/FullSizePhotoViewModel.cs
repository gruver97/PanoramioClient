using System;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using PanoramioClient.Enumerations;
using Windows.Devices.Sensors;

#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;
#endif

namespace PanoramioClient.ViewModel
{
    public class FullSizePhotoViewModel : ViewModelBase, IFullSizePhotoViewModel
    {
        private readonly INavigationService _navigationService;
        private OrientationEnumeration _currentOrientation;
        private BitmapImage _source;
#if WINDOWS_PHONE_APP
        private SimpleOrientationSensor _simpleOrientationSensor;
#endif

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
#if WINDOWS_APP
            GoBackCommand = new RelayCommand(() => _navigationService.GoBack());
            SetOrientationState(DisplayInformation.GetForCurrentView().CurrentOrientation);
            DisplayInformation.GetForCurrentView().OrientationChanged += FullSizePhotoViewModel_OrientationChanged;
#endif
        }

#if WINDOWS_APP
        private void FullSizePhotoViewModel_OrientationChanged(DisplayInformation sender, object args)
        {
            SetOrientationState(DisplayInformation.GetForCurrentView().CurrentOrientation);
        }
#endif


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

#if WINDOWS_APP
        public RelayCommand GoBackCommand { get; }

#endif
#if WINDOWS_APP
        private async void SetOrientationState(DisplayOrientations orientation)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (orientation)
                {
                    case DisplayOrientations.None:
                        CurrentOrientation = OrientationEnumeration.Landscape;
                        break;
                    case DisplayOrientations.Landscape:
                        CurrentOrientation = OrientationEnumeration.Landscape;
                        break;
                    case DisplayOrientations.Portrait:
                        CurrentOrientation = OrientationEnumeration.Portrait;
                        break;
                }
            });
        }
#endif
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