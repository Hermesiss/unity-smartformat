using System.Collections.Generic;
using Trismegistus.SmartFormat.Core.Parsing;

namespace Trismegistus.SmartFormat.Core.Formatting
{
    /// <summary>
    /// Caches information about a format operation
    /// so that repeat calls can be optimized to run faster.
    /// </summary>
    public class FormatCache
    {
        private Dictionary<string, object> cachedObjects;

        public FormatCache(Format format)
        {
            Format = format;
        }

        /// <summary>
        /// Caches the parsed format.
        /// </summary>
        public Format Format { get; }

        /// <summary>
        /// Storage for any misc objects.
        /// This can be used by extensions that want to cache data,
        /// such as reflection information.
        /// </summary>
        public Dictionary<string, object> CachedObjects =>
            cachedObjects ?? (cachedObjects = new Dictionary<string, object>());
    }
}
