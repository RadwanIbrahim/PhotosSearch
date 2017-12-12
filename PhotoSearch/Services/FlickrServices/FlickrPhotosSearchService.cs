using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSearch.Models;
using PhotoSearch.Common;
using Newtonsoft.Json;
using System.Threading;

namespace PhotoSearch.Services.FlickrServices
{
    public class FlickrPhotosSearchService : IPhotosSearchService
    {
        public string SearchTags { get; set; }

        public Task<IEnumerable<IPhoto>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(SearchTags) || string.IsNullOrWhiteSpace(SearchTags))
                return Task.FromResult<IEnumerable<IPhoto>>(new List<IPhoto>());

            return SearchAsync(SearchTags, pageIndex, pageSize, cancellationToken);
        }
        public async Task<IEnumerable<IPhoto>> SearchAsync(string Tags, int Page = 1, int NumberOfPhotosPerPage = 50, CancellationToken cancellationToken = default(CancellationToken))
        {
            var searchurl = GetSearchURl(Tags, Page, NumberOfPhotosPerPage);
            try
            {
                var json = await HttpHelper.GetStringAsync(searchurl);
                FlickrPhotosSearchResult flickrResult = JsonConvert.DeserializeObject<FlickrPhotosSearchResult>(json);
                if (flickrResult.IsValidState)
                {
                    var photos = new List<IPhoto>();
                    photos.AddRange(flickrResult.PhotosSource.Photos);
                    return photos;
                }
                else
                {
                    // also we can parse the json as flickerService Error
                    var error = JsonConvert.DeserializeObject<FlickerServiceError>(json);
                    throw new ServiceException(error.Message);
                }
            }
            catch (JsonException ex)
            {
                throw new ServiceException(ex.Message);
            }

        }

        private string GetSearchURl(string Tags, int Page = 1, int NumberOfPhotosPerPage = 50)
        {
            return Constants.FLickrEndPoint +
                $"?method=flickr.photos.search" +
                $"&api_key={Constants.FlickrAPIKey}" +
                $"&FLickrApi_sig={Constants.FLickrApi_sig}" +
                $"&nojsoncallback=1" +
                $"&format=json" +
                $"&tags={Tags}" +
                $"&page={Page}" +
                $"&per_page={NumberOfPhotosPerPage}" +
                $"&content_type=7" +
                $"&extras=owner_name,date_upload";
        }


    }
}
