using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Web;
using Windows.Web.Http;

namespace PhotoSearch.Services
{
    public static class HttpHelper
    {
        public static async Task<string> GetStringAsync(string url)
        {
            try
            {
                using (HttpClient httpclient = new HttpClient())
                {
                    var httpResponse = await httpclient.GetAsync(new Uri(url));
                    if (!httpResponse.IsSuccessStatusCode)
                        throw new ServiceException("Faild to fetch the Photos From the server");

                    return await httpResponse.Content.ReadAsStringAsync();

                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new ServiceException("Unauthorized Access app doesn't has permession to access internet");
            }
            catch (OperationCanceledException)
            {
                throw new ServiceException("Call Time out");
            }
            catch (COMException ex)
            {
                var error = WebError.GetStatus(ex.HResult);
                string msg = "network error " + Enum.GetName(typeof(WebErrorStatus), error);
                throw new ServiceException(msg);
            }
            catch (Exception)
            {
                throw new ServiceException("Operation error please try again");
            }
        }
    }
}
