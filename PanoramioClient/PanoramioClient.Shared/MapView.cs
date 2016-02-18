using Windows.UI.Xaml.Controls;

#if WINDOWS_PHONE_APP
using Windows.UI.Xaml.Controls.Maps;
#endif

#if WINDOWS_APP
using Bing.Maps;
#endif

namespace PanoramioClient
{
    public class MapView:Grid
    {
#if WINDOWS_APP
        private Map _map = new Map();
#endif

#if WINDOWS_PHONE_APP
        private MapControl _map = new MapControl();
#endif

        public MapView()
        {
            Children.Add(_map);
        }
    }
}