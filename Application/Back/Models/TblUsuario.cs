using System;
using System.Collections.Generic;

namespace Models;

public partial class TblUsuario
{
    public int UsIdUsuarioPk { get; set; }

    public int UsDatosPersonaFk { get; set; }

    public int UsRolSistemaFk { get; set; }

    public string UsAlias { get; set; } = null!;

    public string UsContrasena { get; set; } = null!;

    public bool UsEstado { get; set; }

    public virtual TblDatosPersona UsDatosPersonaFkNavigation { get; set; } = null!;

    public virtual TblDicRolesSistema UsRolSistemaFkNavigation { get; set; } = null!;
}
