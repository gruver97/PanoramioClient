using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace PanoramioClient.ViewModel
{
    public class MapViewModel:ViewModelBase, IMapViewModel
    {
        public RelayCommand<BasicGeoposition> LocationTappedCommand { get; }

        public MapViewModel()
        {
            LocationTappedCommand = new RelayCommand<BasicGeoposition>((position) => { });
        }
    }
}
