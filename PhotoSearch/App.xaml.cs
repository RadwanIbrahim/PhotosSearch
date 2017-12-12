using Prism.Unity.Windows;
using Prism.Windows.AppModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Unity;
using PhotoSearch.Services;
using PhotoSearch.Services.FlickrServices;
using Prism.Windows.Navigation;
using PhotoSearch.Services.Cache;

namespace PhotoSearch
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            base.OnRegisterKnownTypesForSerialization();
            SessionStateService.RegisterKnownType(typeof(Models.IPhoto));
            SessionStateService.RegisterKnownType(typeof(Models.FlickrPhoto));
            SessionStateService.RegisterKnownType(typeof(Models.PhotoSize));
        }
        protected async override Task OnInitializeAsync(IActivatedEventArgs args)
        {

            Container.RegisterInstance<INavigationService>(NavigationService);
            Container.RegisterInstance<ISessionStateService>(SessionStateService);
            Container.RegisterInstance<ICacheService>(new MemoryCacheService());
            Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(ResourceLoader.GetForCurrentView()));
            Container.RegisterType<IPhotosSearchService, FlickrPhotosSearchService>();

            await base.OnInitializeAsync(args);
        }

      
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
           
            NavigationService.Navigate("PhotosSearch",null);

            Window.Current.Activate();
            return Task.CompletedTask;

        }
        protected override Task OnSuspendingApplicationAsync()
        {
            return base.OnSuspendingApplicationAsync();

        }
    }
}
