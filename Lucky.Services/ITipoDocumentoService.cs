using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public interface ITipoDocumentoService
    {
        Task<Respuesta> listarTiposDocumentos();
    }
}
