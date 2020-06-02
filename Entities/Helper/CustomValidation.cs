using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Entities.Helper
{
    public class CustomValidation
    {
        public class RequiredIfAttribute : RequiredAttribute
        {
            private String PropertyName { get; set; }
            private String DesiredValue { get; set; }

            public RequiredIfAttribute(String propertyName, String desiredvalue)
            {
                PropertyName = propertyName;
                DesiredValue = desiredvalue;
            }

            protected override ValidationResult IsValid(object value, ValidationContext context)
            {
                Object instance = context.ObjectInstance;
                Type type = instance.GetType();
                Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
                if (proprtyvalue.ToString() == DesiredValue.ToString())
                {
                    ValidationResult result = base.IsValid(value, context);
                    return result;
                }
                return ValidationResult.Success;
            }
        }
    }
}