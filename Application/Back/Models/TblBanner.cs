using System;
using System.Collections.Generic;

namespace Models;

public partial class TblBanner
{
    public int BnrIdBannerPk { get; set; }

    public int BnrIdContenidoFk { get; set; }

    public string? BnrRutaAcceso { get; set; }

    public string? BnrTexto { get; set; }

    public bool BnrEstado { get; set; }

    public int BnrDuracion { get; set; }

    public virtual TblContenido BnrIdContenidoFkNavigation { get; set; } = null!;
}
