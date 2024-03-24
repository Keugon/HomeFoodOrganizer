using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// Service to Handle interactions with the XMLPersistence
    /// </summary>
    public static class FilesSystemManager
    {
        private static XMLPersistence _xmlPersistence = null!;

        /// <summary>
        /// Creates the Object
        /// </summary>
        public static void InitializeXMLFileSystem()
        {
            _xmlPersistence = new XMLPersistence();
        }
        /// <summary>
        /// Returns the Initialized Object
        /// </summary>
        /// <returns></returns>
        public static XMLPersistence GetXMLPersistance()
        {
            return _xmlPersistence;
        }
    }
}
