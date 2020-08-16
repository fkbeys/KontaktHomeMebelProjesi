using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    public static class TextHelpers
    {
        public static string CapitalizeFirstLetter(string text)
        {
            string textUpper = text;
            if (text.Length == 0)
            {
                return textUpper;
            }
            else if (text.Length == 1)
            {
                char.ToUpper(text[0]);
                return text;
            }
            else
            {
                textUpper=char.ToUpper(text[0])+text.Substring(1).ToLower();
                return textUpper;
            }
        }
    }
}
