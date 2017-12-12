using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Common
{
    /// <summary>
    /// App Constant 
    /// </summary>
    internal static class Constants
    {
        internal const string FLickrEndPoint = "https://api.flickr.com/services/rest";
        internal const string FlickrAPIKey = "ef1f9d4f8ca80dada31c684364355282";
        internal const string FLickrApi_sig = "d7f57fa9e01a6a2d6ccd8597b8d2f86b";

        #region SessionKeys
        internal const string SelectedPhotoKey = "SelectedPhotoKey";
        internal const string SearchTagKey = "SearchTagKey";
        #endregion
    }
}
