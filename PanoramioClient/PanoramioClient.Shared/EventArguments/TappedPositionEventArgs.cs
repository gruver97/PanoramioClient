using System;
using Windows.Devices.Geolocation;

namespace PanoramioClient.EventArguments
{
    public class TappedPositionEventArgs : EventArgs
    {
        public TappedPositionEventArgs(BasicGeoposition geoposition)
        {
            Geoposition = geoposition;
        }

        public BasicGeoposition Geoposition { get; }
    }
}