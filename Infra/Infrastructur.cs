using Essensausgleich.Controller;
using Essensausgleich.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Infra;

/// <summary>
/// Stellt den Anwendungskontext
/// für eine WIFI Anwendung bereit
/// </summary>
/// <remarks>In der Infrastructur
/// befinden sich Informationen,
/// die überall bekannt sein müssen.
/// Bei einer neuen WIFI Anwendung
/// mit einem Objekt dieser Klasse beginnen
/// und alle anderen Objekte der Anwendung
/// mit Fabricate&lt;T&gt; erstellen,
/// damit diese Infrastructur überall
/// bekannt ist.</remarks>
public class Infrastructur : System.Object
{

    #region InvoiceManager

    private InvoiceManager _InvoiceManager = null!;
    public InvoiceManager InvoiceManager
    {
        get
        {
            if(this._InhabitantsManager == null)
            {
                this._InvoiceManager = new InvoiceManager();
            }
            return this._InvoiceManager;
        }
    }

    #endregion

    #region InhabitantsManager
    private InhabitantsManager _InhabitantsManager = null!;
     /// <summary>
     /// Gets the Service for Controlling the Inhabitants
     /// </summary>
    public InhabitantsManager InhabitantsManager
    {
        get
        {
            if(_InhabitantsManager == null)
            {
                this._InhabitantsManager = this.Fabricate<InhabitantsManager>();
            }
            return this._InhabitantsManager;
        }
    }
    #endregion


    #region Objektfabrik

    /// <summary>
    /// Gibt ein initialisiertes Anwendungsobjekt zurück
    /// </summary>
    /// <typeparam name="T">Eine Klasse, die AppObjekt 
    /// erweitert und einen öffentlichen Konstruktor
    /// ohne Parameter besitzt</typeparam>
    /// <returns>Ein Objekt mit eingestellter
    /// Infrastructur</returns>
    public T Fabricate<T>() where T : AppObjekt, new()
    {

        T newObject = new T();

        // Die Infrastructur an 
        // das neue Objekt übergeben
        newObject.Context = this;

        //Nur für die Entwickler
        //einen Protokolleintrag für VisualStudio
        //dass ein Objekt prouziert wurde und einen Fehlerbehandler
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"Es wurde das Objekt:" +
            $"{newObject} produziert");
#endif
        // TODO - hier weitere Produktionsschritte ergänzen

        newObject.FehlerAufgetreten += (sender, e) =>
        System.Diagnostics.Debug.WriteLine(
            $"FEHLER! {newObject}hat" +
            $" eine Ausnahme \"{e.Ursache.Message}\" ausgelöst!");

        return newObject;
    }

    #endregion Objektfabrik

}
