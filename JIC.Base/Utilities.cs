using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace JIC.Base
{
    public class Utilities
    {
        public static Dictionary<T, string> ConvertEnumToDictionary<T>(Type EnumType)
        {
            Dictionary<T, string> Dic = new Dictionary<T, string>();
            foreach (string name in Enum.GetNames(EnumType))
            {
                Dic.Add((T)Enum.Parse(EnumType, name), Resources.Resources.ResourceManager.GetString(name));
            }
            return Dic;
        }

        /// <summary>
        /// clear text from special characters and arabic special characters 
        /// </summary>
        /// <param _fullName="input"></param>
        /// <returns>clearText as strinf</returns>
        public static string RemoveSpecialCharacters(string input)
        {


            //remove special characters
            Regex r = new Regex("(?:[^ءءa-zأ-ي0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            var replace = input.Replace('أ', 'ا')
                .Replace('ئ', 'ء')
                .Replace('ؤ', 'و')
                .Replace('أ', 'ا')
                .Replace('إ', 'ا')
                .Replace('ى', 'ي')
                .Replace('آ', 'ا')
                .Replace("لا", "لا")
                .Replace("لآ", "لا")
                .Replace("لأ", "لا")
                .Replace("لإ", "لا")
                .Replace("ة", "ه");

            //replase arabic traks with defult characters
            replace = r.Replace(replace, string.Empty);


            //merge all spaces in one space
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            var clearText = regex.Replace(replace, " ");
            return clearText.ToLower();

        }
        public static string RemoveSpaces(String s)
        {
            s = Regex.Replace(s, @"\s+", "");
            return s;
        }


    }
}