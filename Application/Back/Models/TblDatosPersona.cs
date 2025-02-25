using System;
using System.Collections.Generic;

namespace Models;

public partial class TblDatosPersona
{
    public int DpIdDatosPersonaPk { get; set; }

    public string DpNombre1 { get; set; } = null!;

    public string? DpNombre2 { get; set; }

    public string DpApellido1 { get; set; } = null!;

    public string? DpApellido2 { get; set; }

    public bool DpEstado { get; set; }

    public virtual ICollection<TblUsuario> TblUsuarios { get; set; } = new List<TblUsuario>();
}
