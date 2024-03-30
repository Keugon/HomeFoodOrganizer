using Essensausgleich.Controller;
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
/// <remarks>In der Infrastruktur
/// befinden sich Informationen,
/// die überall bekannt sein müssen.
/// Bei einer neuen WIFI Anwendung
/// mit einem Objekt dieser Klasse beginnen
/// und alle anderen Objekte der Anwendung
/// mit Produziere&lt;T&gt; erstellen,
/// damit diese Infrastruktur überall
/// bekannt ist.</remarks>
public class Infrastruktur : System.Object
{

    #region Bewohner

    private Bewohner _Bewohner1 = null!;
    /// <summary>
    /// Gets the object Bewohner1
    /// </summary>
    public Bewohner bewohner1
    {
        get
        {
            if (this._Bewohner1 == null)
            {
                this._Bewohner1 = this.Produziere<Bewohner>();
            }
            return this._Bewohner1;
        }
    }

    private Bewohner _Bewohner2 = null!;
    /// <summary>
    /// Gets the object Bewohner2
    /// </summary>
    public Bewohner bewohner2
    {
        get
        {
            if(this._Bewohner2 == null)
            {
                this._Bewohner2 = this.Produziere<Bewohner>();
            }
            return this._Bewohner2;
        }
    }
    #endregion


    #region FilesystemManagaer    
    /// <summary>
    /// Ruft den Dienst zum Verwalten des XML Dienstes ab
    /// </summary>
    public FilesSystemManager FilesSystemManagerService
    {
        get
        {
            if (this._FilesSystemManagerService == null)
            {
                this._FilesSystemManagerService = this.Produziere<FilesSystemManager>();
            }
            return this._FilesSystemManagerService;
        }
    }

    private FilesSystemManager _FilesSystemManagerService = null!;
    #endregion

    #region Objektfabrik

    /// <summary>
    /// Gibt ein initialisiertes Anwendungsobjekt zurück
    /// </summary>
    /// <typeparam name="T">Eine Klasse, die AppObjekt 
    /// erweitert und einen öffentlichen Konstruktor
    /// ohne Parameter besitzt</typeparam>
    /// <returns>Ein Objekt mit eingestellter
    /// Infrastruktur</returns>
    public T Produziere<T>() where T : AppObjekt, new()
    {

        T NeuesObjekt = new T();

        // Die Infrastruktur an 
        // das neue Objekt übergeben
        NeuesObjekt.Kontext = this;

        //Nur für die Entwickler
        //einen Protokolleintrag für VisualStudio
        //dass ein Objekt prouziert wurde und einen Fehlerbehandler
#if DEBUG
        System.Diagnostics.Debug.WriteLine($"Es wurde das Objekt:" +
            $"{NeuesObjekt} produziert");
#endif
        // TODO - hier weitere Produktionsschritte ergänzen

        NeuesObjekt.FehlerAufgetreten += (sender, e) =>
        System.Diagnostics.Debug.WriteLine(
            $"FEHLER! {NeuesObjekt}hat" +
            $" eine Ausnahme \"{e.Ursache.Message}\" ausgelöst!");

        return NeuesObjekt;
    }

    #endregion Objektfabrik

}
