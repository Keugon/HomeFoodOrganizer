using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Infra;

/// <summary>
/// Unterstützt sämtliche 
/// WIFI Anwendungsobjekte
/// mit der Infrastructur und 
/// weiteren Basisfunktionalitäten
/// </summary>
public abstract class AppObjekt : ObservableObject, IAppObjekt
{
    #region Infrastruktur

    /// <summary>
    /// Ruft das Objekt mit der Infrastructur
    /// einer WIFI Anwendung ab oder legt
    /// dieses fest
    /// </summary>
    public Infrastructur Context { get; set; } = null!;

    #endregion Infrastruktur

    #region FehlerAufgetreten-Ereignis

    //Dritter Schritt (oder erster bei System.EventHandler)
    //-> Das Ereignis deklarieren

    /// <summary>
    /// Wird ausgelöst, wenn im Objekt ein Problem eingetretten ist
    /// </summary>
    /// <remarks>Die Ursache befindet sich in den Ereignisdaten</remarks>
    public event FehlerAufgetretenEventHandler
        FehlerAufgetreten = null!;

    //Vierter Schritt (oder 2ter Schritt bei System.EventHandler)
    //->Die Ereignis auslöser Methode

    /// <summary>
    /// Löst das Ereignis FehlerAufgetreten aus
    /// </summary>
    /// <param name="e">Ereignisdaten mit der Ursache</param>
    protected virtual void OnFehlerAufgetreten(FehlerAufgetretenEventArgs e)
    {
        //Wegen des Multithreadings ...
        //Wir blockieren die Speicheradresse damit das Objekt
        //von der GarbageCollektion nicht entfernt wird

        var BehandlerKopie = this.FehlerAufgetreten;


        //Falls ein Ereignis-Behandler vorhanden ist 
        if (BehandlerKopie != null)
        {
            //aufrufen
            BehandlerKopie(this, e);
        }

    }
  
    #endregion
}
