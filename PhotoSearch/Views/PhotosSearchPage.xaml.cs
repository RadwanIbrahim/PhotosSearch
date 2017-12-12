using System;
using Prism.Windows.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace PhotoSearch.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotosSearchPage : SessionStateAwarePage
    {
        public PhotosSearchPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            this.SizeChanged += OnSizeChanged;
        }


        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (PhotosGridView.Visibility == Visibility.Collapsed)
                SearchGridTransform.Y = e.NewSize.Height / 2;
        }
    }
}
