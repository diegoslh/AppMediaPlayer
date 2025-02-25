using System;
using System.Collections.Generic;

namespace Models;

public partial class TblVideo
{
    public int VdoIdVideoPk { get; set; }

    public int VdoIdContenidoFk { get; set; }

    public string VdoRutaAcceso { get; set; } = null!;

    public bool VdoEstado { get; set; }

    public virtual TblContenido VdoIdContenidoFkNavigation { get; set; } = null!;
}
