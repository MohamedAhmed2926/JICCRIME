using JIC.Base.Views;
using System;
using System.Web;
using System.Web.Routing;

namespace JIC.Utilities.Helpers
{
    public class SessionHelper
    {
        #region Keys

        public enum Key
        {
            CurrentUser,
            CurrentUsername,
            RedirectRoute,
            RedirectRouteValues,
            IsLoadPredcyion,
        }

        #endregion

        #region Properties

        public static vw_UserSecurity CurrentUser
        {
            get { return Get<vw_UserSecurity>(Key.CurrentUser); }
            set { Set(Key.CurrentUser, value); }
        }

        //this property is accessed out of the class in repositories project.
        public static string CurrentUsername
        {
            get { return Get<string>(Key.CurrentUsername); }
            set { Set(Key.CurrentUsername, value); }
        }

        public static string RedirectRoute
        {
            get { return Get<string>(Key.RedirectRoute); }
            set { Set(Key.RedirectRoute, value); }
        }

        public static RouteValueDictionary RedirectRouteValues
        {
            get { return Get<RouteValueDictionary>(Key.RedirectRouteValues); }
            set { Set(Key.RedirectRouteValues, value); }
        }
        public static bool IsLoadPredcyion
        {
            get { return Get<bool>(Key.IsLoadPredcyion); }
            set { Set(Key.IsLoadPredcyion, value); }
        }
        #endregion

        #region Methods

        #region Private methods

        private static T Get<T>(Key key)
        {
            object dataValue = HttpContext.Current.Session[KeyToString(key)];
            if (dataValue != null && dataValue is T)
                return (T)dataValue;
            else
                return default(T);
        }

        private static void Set(Key key, object value)
        {
            HttpContext.Current.Session[KeyToString(key)] = value;
        }

        private static string KeyToString(Key key)
        {
            return Enum.GetName(typeof(Key), key);
        }

        #endregion

        #region Generic setters/getters

        public static void Set(Key key, string extraKey, object value)
        {
            HttpContext.Current.Session[KeyToString(key) + extraKey] = value;
        }

        //public static T Get<T>(Key key, string extraKey)
        //{
        //    return Get<T>(key, KeyToString(key) + extraKey);
        //}

        public static T Get<T>(Key key, string extraKey)
        {
            object dataValue = HttpContext.Current.Session[KeyToString(key) + extraKey];
            if (dataValue != null && dataValue is T)
                return (T)dataValue;
            else
                return default(T);
           
        }
        #endregion

        #endregion
    }
}
