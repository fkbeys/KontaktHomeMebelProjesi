using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    public static class RemoveCharacters
    {
        public static string TelephoneClean(string data)
        {
            return new StringBuilder(data).Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "").ToString();
        }
    }
}
