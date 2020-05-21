using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KontaktHome.Filters
{
    public static class CheckFileType
    {
        public static bool checkIfImage(string fileType)
        {
            List<string> allowedFileTypes = new List<string>() { "image/png", "image/jpeg", "image/jpg", "image/gif" };
            bool isAllowedType = allowedFileTypes.Contains(fileType);
            return isAllowedType;
        }
    }
}