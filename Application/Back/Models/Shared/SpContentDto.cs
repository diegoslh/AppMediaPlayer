using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Shared
{
    public class SpContentDto
    {
        public int? CtoIdContenidoPk { get; set; }
        public string CtoTitulo { get; set; } = null!;
        public int CtoTipoContenidoFk { get; set; }
        public string? CtoTipoContenido { get; set; }
        public string? CtoBanner { get; set; }
        public string? CtoVideo { get; set; }
        public string? CtoTextoBanner { get; set; }
        public int? CtoDurationBanner { get; set; }
        public bool CtoEstado { get; set; } = true;
    }
}
