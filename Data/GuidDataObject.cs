using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Data;

public abstract class GuidDataObject : ObservableObject
{
    private Guid? _Guid;

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
