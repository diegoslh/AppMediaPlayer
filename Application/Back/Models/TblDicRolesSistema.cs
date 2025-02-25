using System;
using System.Collections.Generic;

namespace Models;

public partial class TblDicRolesSistema
{
    public int RsIdRolSistemaPk { get; set; }

    public string RsNombreRol { get; set; } = null!;

    public bool RsEstado { get; set; }

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
