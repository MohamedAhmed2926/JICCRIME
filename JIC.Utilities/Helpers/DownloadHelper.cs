using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JIC.Base;

namespace JIC.Utilities.Helpers
{
    public class DownloadHelper
    {
        /// <summary>
        /// Use this method to download an existing physical file.
        /// </summary>
        /// <param name="FilePath"></param>
        public static void DownLoadFile(string FilePath)
        {
            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo myfile = new FileInfo(FilePath);
            // Clear the content of the response
            HttpContext.Current.Response.ClearContent();
            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + myfile.Name + "\"");
            // Add the file size into the response header
            HttpContext.Current.Response.AddHeader("Content-Length", myfile.Length.ToString());
            // Set the ContentType
            HttpContext.Current.Response.ContentType = GetMIMEType(myfile.Extension.ToLower());
            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            HttpContext.Current.Response.TransmitFile(myfile.FullName);
            // End the response
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Use this method to download an existing physical file.
        /// </summary>
        /// <param name="FilePath"></param>
        public static void DownLoadFile(string FilePath, string FileName)
        {
            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo myfile = new FileInfo(FilePath);
            // Clear the content of the response
            HttpContext.Current.Response.ClearContent();
            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + FileName + "\"");
            // Add the file size into the response header
            HttpContext.Current.Response.AddHeader("Content-Length", myfile.Length.ToString());
            // Set the ContentType
            HttpContext.Current.Response.ContentType = GetMIMEType(Path.GetExtension(FilePath));
            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            HttpContext.Current.Response.TransmitFile(myfile.FullName);
            // End the response
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// Use this method to write binary data to response.
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="FileContent"></param>
        public static void DownloadFile(string FileName, byte[] FileContent)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = string.Empty;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (HttpContext.Current.Request.Browser.IsBrowser("IE"))
            {
                FileName = HttpContext.Current.Server.UrlEncode(FileName);
            }
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
            HttpContext.Current.Response.AppendHeader("Content-Length", FileContent.Length.ToString());
            HttpContext.Current.Response.BinaryWrite(FileContent);
            HttpContext.Current.Response.End();
        }

        public static string GetMIMEType(string FileExtension)
        {
            switch (FileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
