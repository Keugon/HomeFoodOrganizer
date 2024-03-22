using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    public static class FilesSystemManager
    {
        private static XMLPersistence _xmlPersistence = null!;

        public static void InitializeXMLFileSystem()
        {
            _xmlPersistence = new XMLPersistence();
        }
        public static XMLPersistence GetXMLPersistance()
        {
            return _xmlPersistence;
        }
    }
}
