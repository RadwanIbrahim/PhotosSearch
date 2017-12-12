using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using PhotoSearch.Services;
using Prism.Windows.Navigation;
using System.Windows.Input;
using Prism.Commands;
using Windows.UI.Popups;
using Microsoft.Toolkit.Uwp;
using PhotoSearch.Models;
using Windows.UI.Xaml.Controls;
using PhotoSearch.Services.Cache;
using PhotoSearch.Common;
using Prism.Windows.AppModel;

namespace PhotoSearch.ViewModels
{
    public class PhotosSearchPageViewModel : ViewModelBase
    {
        private IncrementalLoadingCollection<IPhotosSearchService, IPhoto> _PhotosCollection;
        public IncrementalLoadingCollection<IPhotosSearchService, IPhoto> PhotosCollection
        {
            get { return _PhotosCollection; }
            set { SetProperty(ref _PhotosCollection, value); }
        }
        private bool _IsSearchStateActive;
        public bool IsSearchStateActive
        {
            get { return _IsSearchStateActive; }
            set { SetProperty(ref _IsSearchStateActive, value); }
        }
        public string SearchTag { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand PhotoClickedCommand { get; set; }

        private readonly IPhotosSearchService _PhotosSearchService;
        private readonly INavigationService _NavigationService;
        private readonly ICacheService _CacheService;
        private readonly ISessionStateService _SessionStateService;
        public PhotosSearchPageViewModel(IPhotosSearchService photosSearchService, INavigationService navigationService, ICacheService cacheService, ISessionStateService sessionStateService)
        {
            _PhotosSearchService = photosSearchService;
            _NavigationService = navigationService;
            _CacheService = cacheService;
            _SessionStateService = sessionStateService;
            PhotosCollection = new IncrementalLoadingCollection<IPhotosSearchService, IPhoto>(_PhotosSearchService, 50, onError: OnLoadingError);
            SearchCommand = new DelegateCommand(OnSearch);
            PhotoClickedCommand = new DelegateCommand<ItemClickEventArgs>(OnPhotoClicked);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (e.NavigationMode != Windows.UI.Xaml.Navigation.NavigationMode.Back)
            {
                if (viewModelState.ContainsKey(Constants.SearchTagKey))
                {
                    SearchTag = (string)viewModelState[Constants.SearchTagKey];
                    IsSearchStateActive = true;
                    OnSearch();
                }
            }


        }
        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            viewModelState[Constants.SearchTagKey] = SearchTag;
        }
        private void OnSearch()
        {
            IsSearchStateActive = true;

            _PhotosSearchService.SearchTags = SearchTag;
            PhotosCollection.RefreshAsync();
        }
        private void OnPhotoClicked(ItemClickEventArgs arg)
        {
            _SessionStateService.SessionState[Constants.SelectedPhotoKey] = arg.ClickedItem as IPhoto;
            _NavigationService.Navigate("PhotoDetails", "");
        }

        private void OnLoadingError(Exception ex)
        {
            if (ex is ServiceException)
                ShowError(ex.Message);
        }

        private async void ShowError(string message)
        {
            MessageDialog dlg = new MessageDialog(message);
            await dlg.ShowAsync();
        }
    }
}
