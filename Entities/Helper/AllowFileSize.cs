using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.Helper
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowFileSize: ValidationAttribute
    {       
        public int FileSize { get; set; } = 1 * 1024 * 1024 * 1024;        
        public override bool IsValid(object value)
        {
            // Initialization  
            HttpPostedFileBase[] file = value as HttpPostedFileBase[];
            bool isValid = true;
           
            // Settings.  
            int allowedFileSize = this.FileSize;

            foreach (var item in file)
            {
                // Verification.  
                if (item != null)
                {
                    // Initialization.  
                    var fileSize = item.ContentLength;

                    // Settings.  
                    isValid = fileSize <= allowedFileSize;
                    return isValid;
                }
            }
           

            // Info  
            return isValid;
        }
    }
}