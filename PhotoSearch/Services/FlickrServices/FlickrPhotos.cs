using Newtonsoft.Json;
using PhotoSearch.Models;
using System.Collections.Generic;

namespace PhotoSearch.Services.FlickrServices
{
    public class FlickrPhotosSource
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pages")]
        public int NumberOfPages { get; set; }
        [JsonProperty("perpage")]
        public int NumberPerPage { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("photo")]
        public List<FlickrPhoto> Photos { get; set; }
    }
}
