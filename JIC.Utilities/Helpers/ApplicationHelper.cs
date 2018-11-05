using System;
using System.Collections.Generic;
using JIC.Utilities.Utilities;
using System.Web;
using JIC.Base.Views;

namespace JIC.Utilities.Helpers
{
    public class ApplicationHelper
    {
        #region Keys

        public enum Key
        {
            SystemActions,
            CurrentUsers
        }

        #endregion

        #region Properties

        public static List<vw_SystemActions> SystemActions
        {
            get
            {
                //if (Get<List<vw_SystemActions>>(Key.SystemActions) == default(List<vw_SystemActions>))
                //    Set(Key.SystemActions, new SecurityService().GetSystemActions());

                return Get<List<vw_SystemActions>>(Key.SystemActions);
            }
            set
            {
                Set(Key.SystemActions, value);
            }
        }

        public static List<SessionUser> CurrentUsers
        {
            get
            {
                if (Get<List<SessionUser>>(Key.CurrentUsers) == default(List<SessionUser>))
                    Set(Key.CurrentUsers, new List<SessionUser>());

                return Get<List<SessionUser>>(Key.CurrentUsers);
            }
            set
            {
                Set(Key.CurrentUsers, value);
            }
        }

        #endregion

        #region Methods

        #region Private methods

        private static T Get<T>(Key key)
        {
            try
            {
                object dataValue = HttpContext.Current.Application[KeyToString(key)];
                if (dataValue != null && dataValue is T)
                    return (T)dataValue;
                else
                    return default(T);
            }
            catch (Exception )
            { 

                throw;
            }
        }

        private static void Set(Key key, object value)
        {
            HttpContext.Current.Application[KeyToString(key)] = value;
        }

        private static string KeyToString(Key key)
        {
            return Enum.GetName(typeof(Key), key);
        }

        #endregion

        #region Generic setters/getters

        public static void Set(Key key, string extraKey, object value)
        {
            HttpContext.Current.Application[KeyToString(key) + extraKey] = value;
        }

        public static T Get<T>(Key key, HttpApplicationState application)
        {
            try
            {
                object dataValue = application[KeyToString(key)];
                if (dataValue != null && dataValue is T)
                    return (T)dataValue;
                else
                    return default(T);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static T Get<T>(Key key, string extraKey)
        {
            return Get<T>(key, KeyToString(key) + extraKey);
        }

        #endregion

        #endregion
    }
}
