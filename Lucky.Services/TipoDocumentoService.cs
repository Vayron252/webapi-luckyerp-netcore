using Lucky.Entitys;
using Lucky.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository)
        {
            this._tipoDocumentoRepository = tipoDocumentoRepository;
        }

        public async Task<Respuesta> listarTiposDocumentos()
        {
            Respuesta respuesta = await _tipoDocumentoRepository.listarTiposDocumento();
            if (!respuesta.status)
            {
                respuesta.message = "Ocurrió un error al listar los tipos de documento.";
            }
            return respuesta;
        }
    }
}
