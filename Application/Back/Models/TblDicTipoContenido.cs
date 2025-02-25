using System;
using System.Collections.Generic;

namespace Models;

public partial class TblDicTipoContenido
{
    public int TctoIdTipoContenidoPk { get; set; }

    public string TctoTipoContenido { get; set; } = null!;

    public string TctoDescripcion { get; set; } = null!;

    public bool TctoEstado { get; set; }

    public virtual ICollection<TblContenido> TblContenidos { get; set; } = new List<TblContenido>();
}
