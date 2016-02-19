using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PanoramioClient.ViewModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PanoramioClient
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FullSizePhotoPage : Page
    {
        public FullSizePhotoPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var fullSizePhotoViewModel = DataContext as IFullSizePhotoViewModel;
            if (fullSizePhotoViewModel != null)
                fullSizePhotoViewModel.Source = e.Parameter as BitmapImage;
        }
    }
}