using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NuGetGallery.Services
{
    public interface IContentService
    {
        HtmlString GetContentItem(string name, bool bypassCache);
    }

    public static class ContentServiceExtensions
    {
        public static HtmlString GetContentItem(this IContentService self, string name)
        {
            return self.GetContentItem(name, bypassCache: false);
        }
    }
}
