﻿using System;
using System.Web.Mvc;

namespace NuGetGallery
{
    public static class UrlExtensions
    {
        // Shorthand for current url
        public static string Current(this UrlHelper url)
        {
            return url.RequestContext.HttpContext.Request.RawUrl;
        }

        public static string Absolute(this UrlHelper url, string path)
        {
            UriBuilder builder = GetCanonicalUrl(url);
            builder.Path = path;
            return builder.Uri.AbsoluteUri;
        }

        public static string Home(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.Home);
        }

        public static string Statistics(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.StatisticsHome);
        }

        public static string StatisticsAllPackageDownloads(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.StatisticsPackages);
        }

        public static string StatisticsAllPackageVersionDownloads(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.StatisticsPackageVersions);
        }

        public static string StatisticsPackageDownloadByVersion(this UrlHelper url, string id)
        {
            string result = url.RouteUrl(RouteName.StatisticsPackageDownloadsByVersion, new { id });

            // Ensure trailing slashes for versionless package URLs, as a fix for package filenames that look like known file extensions
            return EnsureTrailingSlash(result);
        }

        public static string StatisticsPackageDownloadsDetail(this UrlHelper url, string id, string version)
        {
            return url.RouteUrl(RouteName.StatisticsPackageDownloadsDetail, new { id, version });
        }

        public static string PackageList(this UrlHelper url, int page, string sortOrder, string searchTerm, bool prerelease)
        {
            return url.Action(MVC.Packages.ListPackages(searchTerm, sortOrder, page, prerelease));
        }

        public static string PackageList(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.ListPackages);
        }

        public static string Package(this UrlHelper url, string id)
        {
            return url.Package(id, null, scheme: null);
        }

        public static string Package(this UrlHelper url, string id, string version, string scheme = null)
        {
            string result = url.RouteUrl(RouteName.DisplayPackage, new { id, version }, protocol: scheme);

            // Ensure trailing slashes for versionless package URLs, as a fix for package filenames that look like known file extensions
            return version == null ? EnsureTrailingSlash(result) : result;
        }

        public static string Package(this UrlHelper url, Package package)
        {
            return url.Package(package.PackageRegistration.Id, package.Version);
        }

        public static string Package(this UrlHelper url, IPackageVersionModel package)
        {
            return url.Package(package.Id, package.Version);
        }

        public static string Package(this UrlHelper url, PackageRegistration package)
        {
            return url.Package(package.Id);
        }

        public static string PackageGallery(this UrlHelper url, string id, string version)
        {
            string protocol = url.RequestContext.HttpContext.Request.IsSecureConnection ? "https" : "http";
            string result = url.RouteUrl(RouteName.DisplayPackage, new { Id = id, Version = version }, protocol: protocol);

            // Ensure trailing slashes for versionless package URLs, as a fix for package filenames that look like known file extensions
            return version == null ? EnsureTrailingSlash(result) : result;
        }

        public static string PackageDownload(this UrlHelper url, int feedVersion, string id, string version)
        {
            string routeName = "v" + feedVersion + RouteName.DownloadPackage;
            string protocol = url.RequestContext.HttpContext.Request.IsSecureConnection ? "https" : "http";
            string result = url.RouteUrl(routeName, new { Id = id, Version = version }, protocol: protocol);
            
            // Ensure trailing slashes for versionless package URLs, as a fix for package filenames that look like known file extensions
            return version == null ? EnsureTrailingSlash(result) : result;
        }

        public static string LogOn(this UrlHelper url)
        {
            return url.RouteUrl(RouteName.Authentication, new { action = "LogOn" });
        }

        public static string LogOff(this UrlHelper url)
        {
            return url.Action(MVC.Authentication.LogOff(url.Current()));
        }

        public static string Search(this UrlHelper url, string searchTerm)
        {
            return url.RouteUrl(RouteName.ListPackages, new { q = searchTerm });
        }

        public static string UploadPackage(this UrlHelper url)
        {
            return url.Action(actionName: "UploadPackage", controllerName: MVC.Packages.Name);
        }

        public static string User(this UrlHelper url, User user, string scheme = null)
        {
            string result = url.Action(MVC.Users.Profiles(user.Username), protocol: scheme);
            return EnsureTrailingSlash(result);
        }

        public static string EditPackage(this UrlHelper url, IPackageVersionModel package)
        {
            return url.Action(MVC.Packages.Edit(package.Id, package.Version));
        }

        public static string DeletePackage(this UrlHelper url, IPackageVersionModel package)
        {
            return url.Action(MVC.Packages.Delete(package.Id, package.Version));
        }

        public static string ManagePackageOwners(this UrlHelper url, IPackageVersionModel package)
        {
            return url.Action(MVC.Packages.ManagePackageOwners(package.Id, package.Version));
        }

        public static string ConfirmationUrl(this UrlHelper url, ActionResult actionResult, string username, string token, string protocol)
        {
            return url.Action(actionResult.AddRouteValue("username", username).AddRouteValue("token", token), protocol: protocol);
        }

        public static string VerifyPackage(this UrlHelper url)
        {
            return url.Action(actionName: "VerifyPackage", controllerName:  MVC.Packages.Name);
        }

        public static string CancelUpload(this UrlHelper url)
        {
            return url.Action(actionName: "CancelUpload", controllerName: MVC.Packages.Name);
        }

        private static UriBuilder GetCanonicalUrl(UrlHelper url)
        {
            UriBuilder builder = new UriBuilder(url.RequestContext.HttpContext.Request.Url);
            builder.Query = String.Empty;
            if (builder.Host.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                builder.Host = builder.Host.Substring(4);
            }
            return builder;
        }

        internal static string EnsureTrailingSlash(string url)
        {
            if (url != null && !url.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                return url + '/';
            }

            return url;
        }
    }
}
