#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Controls.Maps;

#endif
using System;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using PanoramioClient.EventArguments;
#if WINDOWS_APP
using Bing.Maps;

#endif

namespace PanoramioClient
{
    public class MapView : Grid
    {
        private const string ServiceToken = "At7qRNkt_HfigdbkSKAWQ7lM65smVgQ_DV4lLnKH3mUHMFnHMZOEhX48knVU2IoN";
#if WINDOWS_APP
        private readonly Map _map = new Map();
#endif

#if WINDOWS_PHONE_APP
        private readonly MapControl _map = new MapControl();
#endif

        public MapView()
        {
#if WINDOWS_APP
            _map.Credentials = ServiceToken;
            _map.TappedOverride += _map_TappedOverride;
#endif
#if WINDOWS_PHONE_APP
            _map.MapServiceToken = ServiceToken;
            _map.MapTapped += _map_MapTapped;
#endif
            Children.Add(_map);
        }

#if WINDOWS_APP
        private void _map_TappedOverride(object sender, TappedRoutedEventArgs e)
        {
            Location clickedLocation = null;
            if (_map.TryPixelToLocation(e.GetPosition(_map), out clickedLocation))
            {
                OnPositionTapped(
                    new TappedPositionEventArgs(new BasicGeoposition
                    {
                        Latitude = clickedLocation.Latitude,
                        Longitude = clickedLocation.Longitude
                    }));
            }
        }
#endif


#if WINDOWS_PHONE_APP
        private void _map_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            OnPositionTapped(new TappedPositionEventArgs(args.Location.Position));
        }
#endif
        public double MaxZoomLevel => _map.MaxZoomLevel;
        public double MinZoomLevel => _map.MinZoomLevel;

        public double ZoomLevel
        {
            get { return _map.ZoomLevel; }
            set { _map.ZoomLevel = value; }
        }

        public event EventHandler<TappedPositionEventArgs> PositionTapped;

        protected virtual void OnPositionTapped(TappedPositionEventArgs e)
        {
            PositionTapped?.Invoke(this, e);
        }
    }
}