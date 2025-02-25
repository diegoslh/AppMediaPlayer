using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class ContentDto
    {
        [Required]
        public string CtoTitulo { get; set; } = null!;
        [Required]
        public int CtoTipoContenidoFk { get; set; }
        public IFormFile? CtoBanner { get; set; }
        public IFormFile? CtoVideo { get; set; }
        [MaxLength(255)]
        public string? CtoTextoBanner { get; set; }
        public int? CtoDurationBanner { get; set; }
        public bool CtoEstado { get; set; } = true;
    }
}
