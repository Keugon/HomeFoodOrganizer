using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.ExtensionMethods
{
    internal static class ILoggerExtensionMethods
    {
        public static void LogDebugInfo(this ILogger logger, string message)
        => logger.LogDebug(message);
    }
}
