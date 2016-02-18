using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;

namespace PanoramioClient.ViewModel
{
    public class PushpinViewModel : ViewModelBase, IPushpinViewModel
    {
        public PushpinViewModel()
        {
            ThumbnailsImages = new ObservableCollection<BitmapImage>
            {
                new BitmapImage(new Uri("http://prostotech.com/uploads/posts/2016-02/1455617597_google-thumb.jpg")),
                new BitmapImage(new Uri("http://prostotech.com/uploads/posts/2016-02/1455617597_google-thumb.jpg")),
                new BitmapImage(new Uri("http://prostotech.com/uploads/posts/2016-02/1455617597_google-thumb.jpg")),
                new BitmapImage(new Uri("http://prostotech.com/uploads/posts/2016-02/1455617597_google-thumb.jpg")),
                new BitmapImage(new Uri("http://prostotech.com/uploads/posts/2016-02/1455617597_google-thumb.jpg"))
            };
        }

        public ObservableCollection<BitmapImage> ThumbnailsImages { get; }
    }
}