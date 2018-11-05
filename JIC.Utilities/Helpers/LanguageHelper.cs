using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Utilities.Helpers
{
    public class LanguageHelper
    {
        //#region Variables

        public const int ArabicLanguageID = 1025;
        //public const int EnglishLanguageID = 1033;

        //#endregion

        //#region Properties

        /////// <summary>
        /////// Returns current language type, "rtl" for the arabic language and "ltr" for the other languages.
        /////// </summary>
        ////public static string CurrentLanguageType { get { return GetLanguageType(SessionHelper.CurrentLanguageID); } }

        /////// <summary>
        /////// Returns a value which indecates if the current language is rtl language or not.
        /////// </summary>
        /////// <returns></returns>
        ////public static bool CurrentLanguageIsRTL { get { return LanguageIsRTL(SessionHelper.CurrentLanguageID); } }

        ///// <summary>
        ///// Gets the two letter code for the current language.
        ///// </summary>
        ///// <returns></returns>
        //public static string CurrentLanguageCode { get { return GetLanguageCode(SessionHelper.CurrentLanguageID); } }

        //#endregion

        //#region Methods

        ///// <summary>
        ///// Returns "rtl" for the arabic language and "ltr" for the other languages.
        ///// </summary>
        ///// <param name="LCID">The id of the culture which will be used to get its' type.</param>
        ///// <returns></returns>
        //public static string GetLanguageType(int LCID)
        //{
        //    return LCID == ArabicLanguageID ? "rtl" : "ltr";
        //}

        ///// <summary>
        ///// Returns a value which indecates if the supported language is rtl language or not.
        ///// </summary>
        ///// <param name="LCID">The id of the culture which will be used to get its' type.</param>
        ///// <returns></returns>
        //public static bool LanguageIsRTL(int LCID)
        //{
        //    return GetLanguageType(LCID) == "rtl";
        //}

        /// <summary>
        /// Updates the current thread culture based on the assigned CurrentLanguageID value in the current session.
        /// </summary>
        public static void SetCurrentThreadLanguage()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(ArabicLanguageID);
        }

        ///// <summary>
        ///// Gets the two letter code for a specific language.
        ///// </summary>
        ///// <param name="LCID">The id of the culture which will be used to get its' code.</param>
        ///// <returns></returns>
        //public static string GetLanguageCode(int LCID)
        //{
        //    switch (LCID)
        //    {
        //        case ArabicLanguageID: return "ar";
        //        case EnglishLanguageID: return "en";
        //        default: throw new ArgumentException(string.Format("Language {0} is not a supported language.", LCID.ToString()));
        //    }
        //}

        //#endregion
    }
}
