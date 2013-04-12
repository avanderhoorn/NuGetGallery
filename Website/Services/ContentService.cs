using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NuGetGallery.Services
{
    public class ContentService : IContentService
    {
        public static readonly string ContentFolderName = "content";

        public IFileStorageService FileStorage { get; protected set; }

        protected ContentService() { }
        public ContentService(IFileStorageService fileStorage)
        {
            if (fileStorage == null)
            {
                throw new ArgumentNullException("fileStorage");
            }

            FileStorage = fileStorage;
        }

        public HtmlString GetContentItem(string name, bool bypassCache)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException(String.Format(Strings.ParameterCannotBeNullOrEmpty, "name"), "name");
            }

            throw new NotImplementedException();
        }
    }
}