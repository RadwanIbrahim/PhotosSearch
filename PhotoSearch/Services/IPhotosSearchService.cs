using Microsoft.Toolkit.Collections;
using PhotoSearch.Models;

namespace PhotoSearch.Services
{
    public interface IPhotosSearchService : IIncrementalSource<IPhoto>
    {
        string SearchTags { get; set; }
    }
}
