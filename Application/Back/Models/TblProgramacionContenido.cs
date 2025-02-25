using System;
using System.Collections.Generic;

namespace Models;

public partial class TblProgramacionContenido
{
    public int PcoIdProgramacionPk { get; set; }

    public int PcoIdContenidoFk { get; set; }

    public TimeOnly PcoHoraProgramada { get; set; }

    public bool PcoEstado { get; set; }

    public virtual TblContenido PcoIdContenidoFkNavigation { get; set; } = null!;
}
