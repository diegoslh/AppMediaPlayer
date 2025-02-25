using System;
using System.Collections.Generic;

namespace Models;

public partial class TblContenido
{
    public int CtoIdContenidoPk { get; set; }

    public string CtoTitulo { get; set; } = null!;

    public int CtoTipoContenidoFk { get; set; }

    public bool CtoEstado { get; set; }

    public virtual TblDicTipoContenido CtoTipoContenidoFkNavigation { get; set; } = null!;

    public virtual ICollection<TblBanner> TblBanners { get; set; } = new List<TblBanner>();

    public virtual ICollection<TblProgramacionContenido> TblProgramacionContenidos { get; set; } = new List<TblProgramacionContenido>();

    public virtual ICollection<TblVideo> TblVideos { get; set; } = new List<TblVideo>();
}
