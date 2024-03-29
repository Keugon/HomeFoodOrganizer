using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich;
/// <summary>
/// Stellt das Infrastruktur-Objekt bereit.
/// </summary>
/// <remarks>Eine Schnittstelle, Interface, ist vergleichbar
/// mit einer Wunschliste an Objekte. Alle Interfaces beginnen
/// im Namen mit einem großen I.</remarks>
public interface IAppObjekt
{
    //Bei Definitionen sind Wunschlisten "öffentlich",
    //d.h. innerhalb eines Interfaces gibt's keine Zugriffsmodifizierer

    /// <summary>
    /// Ruft das Infrastruktur-Objekt ab oder legt dieses fest.
    /// </summary>
     Essensausgleich.Infra.Infrastruktur Kontext { get; set; }
}
