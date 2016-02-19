﻿#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
#endif
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

#if WINDOWS_APP
using Bing.Maps;

#endif

namespace PanoramioClient
{
    public class MapView : Grid
    {
#if WINDOWS_APP
        private readonly Map _map = new Map();
#endif

#if WINDOWS_PHONE_APP
        private readonly MapControl _map = new MapControl();
#endif

        public MapView()
        {
            var serviceToken = Application.Current.Resources["BingMapServiceToken"].ToString();
#if WINDOWS_APP
            _map.Credentials = serviceToken;
            _map.TappedOverride += _map_TappedOverride;
            _map.DoubleTappedOverride += _map_DoubleTappedOverride;
#endif
#if WINDOWS_PHONE_APP
            _map.MapServiceToken = serviceToken;
            _map.MapTapped += _map_MapTapped;
#endif
            Children.Add(_map);
        }


#if WINDOWS_PHONE_APP

        private void _map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            AddPushpin(args.Location.Position);
            LocationTappedCommand?.Execute(args.Location.Position);
        }
#endif
        private void _map_DoubleTappedOverride(object sender, DoubleTappedRoutedEventArgs e)
        {
            e.Handled = true;
        }


        private void _map_TappedOverride(object sender, TappedRoutedEventArgs e)
        {
#if WINDOWS_APP
             Location clickedLocation = null;
            if (_map.TryPixelToLocation(e.GetPosition(_map), out clickedLocation))
            {
                var basicPosition = new BasicGeoposition
                {
                    Latitude = clickedLocation.Latitude,
                    Longitude = clickedLocation.Longitude
                };
                LocationTappedCommand.Execute(basicPosition);
                AddPushpin(basicPosition);
            }
#endif
            e.Handled = true;
        }


        public double MaxZoomLevel => _map.MaxZoomLevel;
        public double MinZoomLevel => _map.MinZoomLevel;

        public double ZoomLevel
        {
            get { return _map.ZoomLevel; }
            set { _map.ZoomLevel = value; }
        }

        public static readonly DependencyProperty LocationTappedCommandProperty = DependencyProperty.Register(
            "LocationTappedCommand", typeof (ICommand), typeof (MapView), new PropertyMetadata(default(ICommand)));

        public ICommand LocationTappedCommand
        {
            get { return (ICommand) GetValue(LocationTappedCommandProperty); }
            set { SetValue(LocationTappedCommandProperty, value); }
        }

        private void AddPushpin(BasicGeoposition location)
        {
            _map.Children.Clear();
#if WINDOWS_APP
            var pushpin = new CustomPushpin(location);
            var location1 = _map.Center = new Location {Latitude = location.Latitude, Longitude = location.Longitude};
            MapLayer.SetPosition(pushpin, location1);
            _map.Children.Add(pushpin);
#endif
#if WINDOWS_PHONE_APP
            var pushPin = new ImagePushpin(location);
            _map.Center = new Geopoint(location);
            MapControl.SetLocation(pushPin, _map.Center);
            _map.Children.Add(pushPin);
#endif

        }
    }
}