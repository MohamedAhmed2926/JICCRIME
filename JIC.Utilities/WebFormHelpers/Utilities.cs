using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JIC.Crime.View.WebFormHelpers
{
    public class Utilities
    {
        /// <summary>
        /// Adds a stylesheet to the page head tag.
        /// </summary>
        /// <param name="Header">The page head.</param>
        /// <param name="href">The url of the css file you want to add to the page head.</param>
        /// <remarks>Automatically replaces the token {langType} in href with ltr/rtl depending on the current language type.</remarks>
        /// <remarks>Automatically replaces the token {langCode} in href with code of the target language depending on the current language.</remarks>
        public static void AddCssToPageHeader(HtmlHead Header, string href)
        {
            string expandedHref = href;

            var styleTag = string.Format("<link href='{0}' rel='stylesheet'/>", expandedHref);
            Literal CustomLinks = (Literal)Header.FindControl("CssLinks");
            if (!CustomLinks.Text.Contains(styleTag))
                CustomLinks.Text += styleTag;
        }

        /// <summary>
        /// Adds a script reference to the page head tag.
        /// </summary>
        /// <param name="Header">The page head.</param>
        /// <param name="src">The url of the js file you want to add to the page head.</param>
        /// <remarks>Automatically replaces the token {langType} in src with ltr/rtl depending on the current language type.</remarks>
        /// <remarks>Automatically replaces the token {langCode} in src with code of the target language depending on the current language.</remarks>
        public static void AddJsToPageHeader(HtmlHead Header, string src)
        {
            string expandedSrc = src;



            var scriptTag = string.Format("<script src='{0}'></script>", expandedSrc);
            Literal CustomLinks = (Literal)Header.FindControl("JsLinks");
            if (!CustomLinks.Text.Contains(scriptTag))
                CustomLinks.Text += scriptTag;
        }

    }
}