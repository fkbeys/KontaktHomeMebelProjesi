using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.Helper
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowFileSize : ValidationAttribute
    {
        public int FileSize { get; set; } = 1 * 1024 * 1024 * 1024;
        public override bool IsValid(object value)
        {
            HttpPostedFileBase[] file = value as HttpPostedFileBase[];
            bool isValid = true;
            int allowedFileSize = this.FileSize;
            foreach (var item in file)
            {
                if (item != null)
                {
                    var fileSize = item.ContentLength;
                    isValid = fileSize <= allowedFileSize;
                    return isValid;
                }
            }
            return isValid;
        }
    }
}