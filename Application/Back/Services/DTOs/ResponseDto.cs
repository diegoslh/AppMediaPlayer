using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Jwt { get; set; }
    }
}
