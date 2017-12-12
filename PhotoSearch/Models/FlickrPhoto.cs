using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Models
{
    public class FlickrPhoto : IPhoto
    {
        [JsonProperty("id")]
        public string PhotoId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonProperty("ownername")]
        public string OwnerName { get; set; }
        [JsonProperty("dateupload")]
        public string PostedDate { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("farm")]
        public string Farm { get; set; }
        [JsonProperty("ispublic")]
        public int IsPublic { get; set; }
        [JsonProperty("isfriend")]
        public int IsFriend { get; set; }
        [JsonProperty("isfamily")]
        public int IsFamily { get; set; }

        public string ThumbnailURL
        {
            get { return GetPhotoURL(PhotoSize.Small); }
            set { }
        }
        public string OrginalSizeUrL
        {
            get { return GetPhotoURL(PhotoSize.Large); }
            set { }
        }
        public string GetPhotoURL(PhotoSize photoSize)
        {
            return Services.FlickrServices.FlickrPhotoURLFormat.UrlFormat(this, photoSize, "jpg");
        }
    }

}
