using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NuGetGallery
{
    public interface IFileReference
    {
        string FullName { get; }
        string Name { get; }
        DateTime LastModifiedUtc { get; }
        string ContentId { get; }

        Stream Open();
    }
}
