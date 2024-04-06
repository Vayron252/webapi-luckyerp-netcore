using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Entitys
{
    public class Hijo
    {
        public int idhijo { get; set; }
        public Personal oPersonal { get; set; }
        public TipoDocumento oTipoDocumento { get; set; }
        public string? nrodocumento { get; set; }
        public string? apepaterno { get; set; }
        public string? apematerno { get; set; }
        public string? nombre1 { get; set; }
        public string? nombre2 { get; set; }
        public string? nombrecompleto { get; set; }
        public string? fecnacimiento { get; set; }
        public string? fecregistro { get; set; }
        public string? usuregistra { get; set; }
        public string? fecmodifica { get; set; }
        public string? usumodifica { get; set; }
        public bool? flgactivo { get; set; }
    }
}
