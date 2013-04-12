using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Helpers;

namespace NuGetGallery.Services
{
    public class TestFileReference : IFileReference
    {
        private byte[] _content;
        public string FullName { get; private set; }

        public string Name
        {
            get { return Path.GetFileName(FullName); }
        }

        public DateTime LastModifiedUtc
        {
            get;
            private set;
        }

        public string ContentId
        {
            get;
            private set;
        }

        public TestFileReference(byte[] content, string fullName, DateTime lastModifiedUtc, string contentId)
        {
            _content = content;
            FullName = fullName;
            LastModifiedUtc = lastModifiedUtc;
            ContentId = contentId;
        }

        public static TestFileReference Create(string content, string fullName, DateTime lastModifiedUtc)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            string hash = Crypto.Hash(contentBytes);
            return new TestFileReference(contentBytes, fullName, lastModifiedUtc, hash);
        }

        public Stream Open()
        {
            return new MemoryStream(_content);
        }
    }
}
