/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PanoramioClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using PanoramioClient.Services;

namespace PanoramioClient.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IUnityContainer _unityContainer = new UnityContainer();

        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("FullSizePhotoPage", typeof(Views.FullSizePhotoPage));
            _unityContainer.RegisterType<IPushpinViewModel, PushpinViewModel>()
                .RegisterType<IFullSizePhotoViewModel, FullSizePhotoViewModel>()
                .RegisterType<IPanoramioService, PanoramioService>()
                .RegisterInstance<INavigationService>(navigationService);
        }

        public IPushpinViewModel PushpinViewModel => _unityContainer.Resolve<PushpinViewModel>();
        public IFullSizePhotoViewModel FullSizePhotoViewModel => _unityContainer.Resolve<IFullSizePhotoViewModel>();

    }
}