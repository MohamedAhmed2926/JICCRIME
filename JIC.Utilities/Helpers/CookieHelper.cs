using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Utilities.Helpers
{
    public class CookieHelper
    {
        #region Keys

        public enum Key
        {
            LastLanguageID,
        }

        #endregion

        #region Methods

        #region Private methods

        private static string KeyToString(Key key)
        {
            return Enum.GetName(typeof(Key), key);
        }

        #endregion

        public static void AddCookie(Key key, string value, string domain = null, DateTime? expiryDate = null, bool persistent = true, bool httpOnly = false)
        {
            var cookie = HttpContext.Current.Response.Cookies[KeyToString(key)];
            cookie.Value = value;
            if (domain != null)
                cookie.Domain = domain;
            cookie.Expires = persistent ? (expiryDate ?? DateTime.Now.AddMonths(1)) : DateTime.MinValue;
            cookie.HttpOnly = httpOnly;
        }

        public static void DeleteCookie(Key key)
        {
            var cookie = HttpContext.Current.Response.Cookies[KeyToString(key)];
            if (cookie != null)
                cookie.Expires = DateTime.Now.AddDays(-1);
        }

        public static void DeleteCookies(string PartofName)
        {
            var cookies = HttpContext.Current.Request.Cookies.OfType<HttpCookie>().Where(c => c.Name.Contains(PartofName)).ToList();
            cookies.AddRange(HttpContext.Current.Response.Cookies.OfType<HttpCookie>().Where(c => c.Name.Contains(PartofName)).ToList());
            if (cookies.Count > 0)
            {
                cookies.ForEach(cookie =>
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                });
            }
        }

        public static string GetCookieValue(Key key)
        {
            string value = string.Empty;

            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(KeyToString(key)))
                value = HttpContext.Current.Request.Cookies.Get(KeyToString(key)).Value;

            return value;
        }

        #endregion
    }
}
