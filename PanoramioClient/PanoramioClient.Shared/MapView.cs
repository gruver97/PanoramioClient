using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Controls.Maps;

#endif

#if WINDOWS_APP
using Bing.Maps;
#endif

namespace PanoramioClient
{
    public class MapView : Grid, INotifyPropertyChanged
    {
        private const string ServiceToken = "At7qRNkt_HfigdbkSKAWQ7lM65smVgQ_DV4lLnKH3mUHMFnHMZOEhX48knVU2IoN";
#if WINDOWS_APP
        private Map _map = new Map();
#endif

#if WINDOWS_PHONE_APP
        private readonly MapControl _map = new MapControl();
        private double _zoomLevel;
#endif

        public MapView()
        {
#if WINDOWS_APP
            _map.Credentials = ServiceToken;
#endif
#if WINDOWS_PHONE_APP
            _map.MapServiceToken = ServiceToken;
#endif
            Children.Add(_map);
        }

        public double MaxZoomLevel => _map.MaxZoomLevel;
        public double MinZoomLevel => _map.MinZoomLevel;

        public double ZoomLevel
        {
            get { return _map.ZoomLevel; }
            set { _map.ZoomLevel = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}