using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using PhotoSearch.Models;
using PhotoSearch.Services.Cache;
using PhotoSearch.Common;
using Prism.Windows.AppModel;

namespace PhotoSearch.ViewModels
{
    public class PhotoDetailsPageViewModel : ViewModelBase
    {
        private IPhoto _photo;
        public IPhoto Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }
        private readonly ISessionStateService _SessionStateService;
        public PhotoDetailsPageViewModel(ISessionStateService sessionStateService)
        {
            _SessionStateService = sessionStateService;
            if (_SessionStateService.SessionState.ContainsKey(Constants.SelectedPhotoKey))
            {
                Photo = (IPhoto)_SessionStateService.SessionState[Constants.SelectedPhotoKey];
            }
        }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

        }
        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
           
        }
    }
}
