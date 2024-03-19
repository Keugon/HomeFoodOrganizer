using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// interface for multiplay variants of Persistence eg XML, SQL, no persistance
    /// </summary>
    internal interface Persistence
    {
        void Save(Bewohner bewohner1, Bewohner bewohner2);
        void Load(Bewohner bewohner1, Bewohner bewohner2);
        void Reset(Bewohner bewohner1, Bewohner bewohner2);
    }
}
