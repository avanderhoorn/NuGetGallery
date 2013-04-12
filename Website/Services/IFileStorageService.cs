﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using NuGetGallery.Services;

namespace NuGetGallery
{
    public interface IFileStorageService
    {
        Task<ActionResult> CreateDownloadFileActionResultAsync(Uri requestUrl, string folderName, string fileName);

        Task DeleteFileAsync(string folderName, string fileName);

        Task<bool> FileExistsAsync(string folderName, string fileName);

        Task<Stream> GetFileAsync(string folderName, string fileName);

        /// <summary>
        /// Gets a reference to a file in the storage service, which can be used to open the full file data.
        /// </summary>
        /// <param name="folderName">The folder containing the file to open</param>
        /// <param name="fileName">The file within that folder to open</param>
        /// <returns>A <see cref="IFileReference"/> representing the file reference</returns>
        Task<IFileReference> GetFileReferenceAsync(string folderName, string fileName);

        Task SaveFileAsync(string folderName, string fileName, Stream packageFile);
    }
}
