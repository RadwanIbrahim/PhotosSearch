using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Models
{
    public interface IPhoto
    {
        string PhotoId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string OwnerName { get; set; }
        string PostedDate { get; set; }
        string ThumbnailURL { get; set; }
        string OrginalSizeUrL { get; set; }
        string GetPhotoURL(PhotoSize photoSize);
    }
}
