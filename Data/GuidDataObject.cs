using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Data;
/// <summary>
/// Serve as the base for DataObjects with a 
/// GUID and be observable properties
/// </summary>
public abstract class GuidDataObject : ObservableObject
{
    private Guid? _Guid;
    /// <summary>
    /// Gets or sets a guid if guid is NULL it generates a new
    /// </summary>
    public Guid? Guid
    {
        get
        {
            if(this._Guid == null)
            {
                this._Guid = System.Guid.NewGuid();
            }
            return this._Guid;
        }
        set => this._Guid = value;
    }
}
