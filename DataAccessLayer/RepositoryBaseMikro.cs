using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RepositoryBaseMikro
    {
        protected static DatabaseContextMikro contextmikro;
        protected static object _lockSync = new object();

        protected RepositoryBaseMikro()
        {
            contextmikro = new DatabaseContextMikro();
            //CreateContextMikro();
        }

        private static void CreateContextMikro()
        {
            if (contextmikro == null)
            {
                lock (_lockSync)
                {
                    if (contextmikro == null)
                    {
                        contextmikro = new DatabaseContextMikro();
                    }
                }

            }
        }
    }
}
