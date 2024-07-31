using Essensausgleich.Data;

namespace Essensausgleich.Controller
{
    /// <summary>
    /// interface for multiplay variants of Persistence eg XML, SQL, no persistance
    /// </summary>
    internal interface Ipersistence
    {
        void Save(Inhabitant inhabitant1, Inhabitant inhabitant2);
        void Load(Inhabitant inhabitant1, Inhabitant inhabitant2);
        void Reset(Inhabitant inhabitant1, Inhabitant inhabitant2);
    }
}
