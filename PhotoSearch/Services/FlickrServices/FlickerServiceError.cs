using Newtonsoft.Json;

namespace PhotoSearch.Services.FlickrServices
{
    public class FlickerServiceError
    {
        [JsonProperty("stat")]
        public string State { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
