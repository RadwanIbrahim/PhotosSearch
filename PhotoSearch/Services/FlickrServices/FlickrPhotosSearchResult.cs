using Newtonsoft.Json;

namespace PhotoSearch.Services.FlickrServices
{
    public class FlickrPhotosSearchResult
    {
        [JsonProperty("photos")]
        public FlickrPhotosSource PhotosSource { get; set; }
        [JsonProperty("stat")]
        public string State { get; set; }
        public bool IsValidState
        {
            get { return State == "ok"; }
        }
    }
}
