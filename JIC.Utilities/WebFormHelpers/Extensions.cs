using JIC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIC.Utilities.WebFormHelpers
{
    internal static class Extensions
    {
        #region ViewState Methods

        internal static string Get(this StateBag ViewState, string key)
        {
            return Get(ViewState, key, string.Empty);
        }

        internal static T Get<T>(this StateBag ViewState, string key) where T : new()
        {
            var val = ViewState[key];
            if (val != null)
                return (T)val;

            Set<T>(ViewState, key, new T());
            return new T();
        }

        internal static T Get<T>(this StateBag ViewState, string key, T defaultValue)
        {
            var val = ViewState[key];
            if (val != null)
                return (T)val;
            Set<T>(ViewState, key, defaultValue);
            return defaultValue;
        }

        internal static void Set<T>(this StateBag ViewState, string key, T value)
        {
            if (value != null && !value.GetType().IsSerializable)
                throw new ArithmeticException(string.Format("Type {0} is not serializable.", value.GetType().FullName));
            ViewState[key] = value;
        }

        #endregion

        public static Route MapRouteWithName(this RouteCollection routes, string routeName, string routeUrl, string physicalFile)
        {
            Route route = routes.MapPageRoute(routeName, routeUrl, physicalFile, false);
            route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("RouteName", routeName);
            return route;
        }

        public static Route MapRouteWithName(this RouteCollection routes, string routeName, string routeUrl, string physicalFile, RouteValueDictionary defaults)
        {
            Route route = routes.MapPageRoute(routeName, routeUrl, physicalFile, false, defaults);
            route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("RouteName", routeName);
            return route;
        }

        public static Routings GetRoute(this RouteData RouteData)
        {
            Routings CurrentRoute;
            if (!Enum.TryParse<Routings>(RouteData.DataTokens["RouteName"].ToString(), out CurrentRoute))
                throw new Exception("Invalid Route");
            return CurrentRoute;
        }

        public static string GetRouteName(this RouteData RouteData)
        {
            return RouteData.DataTokens["RouteName"].ToString();
        }

        public static void RouteRedirect(this HttpResponse Response, Routings Routing)
        {
            Response.RedirectToRoute(Routing.ToString());
        }

        public static void RouteRedirect(this HttpResponse Response, Routings Routing, object RouteValues)
        {
            Response.RedirectToRoute(Routing.ToString(), RouteValues);
        }

        public static void RouteRedirect(this HttpResponse Response, string Routing, object RouteValues)
        {
            Response.RedirectToRoute(Routing, RouteValues);
        }

        public static void RouteRedirect(this HttpResponse Response, Routings Routing, RouteValueDictionary RouteValues)
        {
            Response.RedirectToRoute(Routing.ToString(), RouteValues);
        }

        #region ListItem Method
        public static List<ListItem> GetSelectedItems(this ListItemCollection items)
        {
            List<ListItem> selectedItems = new List<ListItem>();
            foreach (ListItem item in items)
            {
                if (item.Selected)
                    selectedItems.Add(item);

            }
            return selectedItems;

        }

        public static void ClearSelectedItems(this ListItemCollection items)
        {
            List<ListItem> selectedItems = new List<ListItem>();
            foreach (ListItem item in items)
            {
                if (item.Selected)
                    item.Selected = false;
            }

        }
        #endregion

        #region List
        public static T PopAt<T>(this List<T> list,int index)
        {
            if (list.Count > 1)
            {
                T r = list[index];
                list.RemoveAt(index);
                return r;
            }
            else
                return default(T);
        }
        #endregion
    }
}
