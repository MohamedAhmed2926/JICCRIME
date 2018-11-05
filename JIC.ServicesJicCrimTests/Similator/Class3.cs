#region Disclaimer/Info
///////////////////////////////////////////////////////////////////////////////////////////////////
// Subtext WebLog
// 
// Subtext is an open source weblog system that is a fork of the .TEXT
// weblog system.
//
// For updated news and information please visit http://subtextproject.com/
// Subtext is hosted at SourceForge at http://sourceforge.net/projects/subtext
// The development mailing list is at subtext-devs@lists.sourceforge.net 
//
// This project is licensed under the BSD license.  See the License.txt file for more information.
///////////////////////////////////////////////////////////////////////////////////////////////////
#endregion


using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Hosting;


namespace JIC.ServicesJicCrimTests.Similator
{
    /// <summary>
    /// Used to simulate an HttpRequest.
    /// </summary>
    public class SimulatedHttpRequest : SimpleWorkerRequest
    {
        readonly string host;
        readonly string verb;
        readonly int port;
        readonly string physicalFilePath;
        private readonly NameValueCollection headers = new NameValueCollection();
        private readonly NameValueCollection formVariables = new NameValueCollection();

        Uri referrer;

        public SimulatedHttpRequest(string applicationPath, string physicalAppPath, string physicalFilePath, string page, string query, TextWriter output, string host, int port, string verb)
            : base(applicationPath, physicalAppPath, page, query, output)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host", "Host cannot be null.");
            }

            if (host.Length == 0)
            {
                throw new ArgumentException("Host cannot be empty.", "host");
            }

            if (applicationPath == null)
            {
                throw new ArgumentNullException("applicationPath", "Can't create a request with a null application path. Try empty string.");
            }

            this.host = host;
            this.verb = verb;
            this.port = port;
            this.physicalFilePath = physicalFilePath;
        }

        internal void SetReferrer(Uri referer)
        {
            this.referrer = referer;
        }

        /// <summary>
        /// Returns the specified member of the request header.
        /// </summary>
        /// <returns>
        /// The HTTP verb returned in the request
        /// header.
        /// </returns>
        public override string GetHttpVerbName()
        {
            return verb;
        }

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <returns></returns>
        public override string GetServerName()
        {
            return host;
        }

        public override int GetLocalPort()
        {
            return this.port;
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public NameValueCollection Headers
        {
            get
            {
                return this.headers;
            }
        }


        /// <summary>
        /// Gets the format exception.
        /// </summary>
        /// <value>The format exception.</value>
        public NameValueCollection Form
        {
            get
            {
                return formVariables;
            }
        }

        /// <summary>
        /// Get all nonstandard HTTP header name-value pairs.
        /// </summary>
        /// <returns>An array of header name-value pairs.</returns>
        public override string[][] GetUnknownRequestHeaders()
        {
            if (this.headers == null || this.headers.Count == 0)
            {
                return null;
            }
            var headersArray = new string[this.headers.Count][];
            for (var i = 0; i < this.headers.Count; i++)
            {
                headersArray[i] = new string[2];
                headersArray[i][0] = this.headers.Keys[i];
                headersArray[i][1] = this.headers[i];
            }
            return headersArray;
        }

        public override string GetKnownRequestHeader(int index)
        {
            if (index == 0x24)
            {
                return referrer == null ? string.Empty : referrer.ToString();
            }

            if (index == 12 && this.verb == "POST")
            {
                return "application/x-www-form-urlencoded";
            }

            return base.GetKnownRequestHeader(index);
        }

        /// <summary>
        /// Returns the virtual path to the currently executing
        /// server application.
        /// </summary>
        /// <returns>
        /// The virtual path of the current application.
        /// </returns>
        public override string GetAppPath()
        {
            var appPath = base.GetAppPath();
            return appPath;
        }

        public override string GetAppPathTranslated()
        {
            var path = base.GetAppPathTranslated();
            return path;
        }

        public override string GetUriPath()
        {
            var uriPath = base.GetUriPath();
            return uriPath;
        }

        public override string GetFilePathTranslated()
        {
            return physicalFilePath;
        }

        /// <summary>
        /// Reads request data from the client (when not preloaded).
        /// </summary>
        /// <returns>The number of bytes read.</returns>
        public override byte[] GetPreloadedEntityBody()
        {
            var formText = string.Empty;

            foreach (string key in this.formVariables.Keys)
            {
                formText += string.Format("{0}={1}&", key, this.formVariables[key]);
            }

            return Encoding.UTF8.GetBytes(formText);
        }

        /// <summary>
        /// Returns a value indicating whether all request data
        /// is available and no further reads from the client are required.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if all request data is available; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public override bool IsEntireEntityBodyIsPreloaded()
        {
            return true;
        }
    }
}