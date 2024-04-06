using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Entitys
{
    public class Respuesta
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
