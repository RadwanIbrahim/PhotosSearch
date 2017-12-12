using PhotoSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Services.FlickrServices
{
    public static class FlickrPhotoURLFormat
    {
        private const string PhotoUrlFormat = "https://farm{0}.staticflickr.com/{1}/{2}_{3}{4}.{5}";

        internal static string UrlFormat(FlickrPhoto p, PhotoSize size, string extension)
        {
            return UrlFormat(p.Farm, p.Server, p.PhotoId, p.Secret, size, extension);
        }

        internal static string UrlFormat(string farm, string server, string photoid, string secret, PhotoSize size, string extension)
        {
            string sizeAbbreviation;
            switch (size)
            {

                case PhotoSize.Small:
                    sizeAbbreviation = "_m";
                    break;
                case PhotoSize.Large:
                    sizeAbbreviation = "_b";
                    break;
                case PhotoSize.Original:
                    sizeAbbreviation = "_o";
                    break;
                case PhotoSize.Medium:
                    sizeAbbreviation = string.Empty;
                    break;
                default:
                    sizeAbbreviation = "_o";
                    break;
            }

            return UrlFormat(PhotoUrlFormat, farm, server, photoid, secret, sizeAbbreviation, extension);
        }

        private static string UrlFormat(string format, params object[] parameters)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, format, parameters);
        }
    }
}
