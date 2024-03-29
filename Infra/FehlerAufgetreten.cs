using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Infra;

//Erster Schritt für eigene Ereignisse
//-> Die Ereignisdaten-Klasse

/// <summary>
/// Stellt die Daten für das FehlerAufgetreten Ereignis bereit
/// </summary>
/// <remarks>Solche classen müssen System.EventArgs erweitern</remarks>
public class FehlerAufgetretenEventArgs : System.EventArgs
{
    /// <summary>
    /// Initialisiert ein FehlerAufgetretenEventArgs Objekt
    /// </summary>
    /// <param name="ursache">Die Ausnahme 
    /// die den Fehler beschreibt</param>
    public FehlerAufgetretenEventArgs(System.Exception ursache)
    {
        this._Ursache = ursache;
    }

    private System.Exception _Ursache = null!;
    /// <summary>
    /// Ruft das Ausnahme-Objekt ab mit 
    /// dem der Fehler beschrieben ist
    /// </summary>
    public System.Exception Ursache => this._Ursache;
}  

    //Zweiter Schritt für eigene Ereignisse
    //-> Die Signatur der Methode die als
    //Ergeigniss Behandler erlaubt ist


    /// <summary>
    /// Stellt die Methode dar die das FehlerAufgetreten Ereignis behandelt
    /// </summary>
    /// <param name="sender">Immer der erste Parameter.
    /// Der Verweis auf das Objekt von dem diese Methode aufgerufen wird</param>
    /// <param name="e">Immer der zweite Parameter.
    /// Der Verweis auf das Objekt mit dem Daten für das Ereignis. Falls keine 
    /// Daten geliefert werden, System.EventArgs alleine</param>
public delegate void FehlerAufgetretenEventHandler(
    object sender,
    FehlerAufgetretenEventArgs e);


