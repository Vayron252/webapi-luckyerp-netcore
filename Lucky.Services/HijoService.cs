using Lucky.Entitys;
using Lucky.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public class HijoService : IHijoService
    {
        private readonly IHijoRepository _hijorepository;

        public HijoService(IHijoRepository hijorepository)
        {
            this._hijorepository = hijorepository;
        }

        public async Task<Respuesta> listarHijosPorPersonal(int idpers)
        {
            Respuesta respuesta = await _hijorepository.listarHijosActivosPorPersonal(idpers);
            if (!respuesta.status)
            {
                respuesta.message = "Ocurrió un error al listar los hijos.";
            }
            return respuesta;
        }

        public async Task<Respuesta> obtenerHijoPorId(int id)
        {
            Hijo hijo = await _hijorepository.obtenerHijoPorId(id);
            Respuesta respuesta = new Respuesta();
            respuesta.status = true;
            respuesta.data = hijo;
            if (hijo == null)
            {
                respuesta.status = false;
                respuesta.message = "Ocurrió un error al obtener el hijo.";
            }
            return respuesta;
        }

        public async Task<Respuesta> guardarHijo(Hijo hijo)
        {
            Respuesta respuesta = new Respuesta();

            if (hijo.idhijo.Equals(0))
            {
                respuesta = await _hijorepository.registrarHijo(hijo);
                if (!respuesta.status)
                {
                    respuesta.message = "Ocurrió un error al registrar el hijo.";
                }
                else
                {
                    int idgenerado = (int)respuesta.data;
                    respuesta.data = await _hijorepository.obtenerHijoPorId(idgenerado);
                }
            }
            else
            {
                DateTime dateTime = DateTime.Now;
                hijo.fecmodifica = dateTime.ToString("dd/MM/yyyy hh:mm:ss");
                respuesta = await _hijorepository.editarHijo(hijo);
                if (!respuesta.status)
                {
                    respuesta.message = "Ocurrió un error al editar el hijo.";
                }
                else
                {
                    respuesta.data = await _hijorepository.obtenerHijoPorId(hijo.idhijo);
                }
            }

            return respuesta;
        }

        public async Task<Respuesta> eliminarHijo(Hijo hijo)
        {
            DateTime dateTime = DateTime.Now;
            hijo.fecmodifica = dateTime.ToString("dd/MM/yyyy hh:mm:ss");
            Respuesta respuesta = await _hijorepository.eliminarHijo(hijo);
            if (!respuesta.status)
            {
                respuesta.message = "Ocurrió un error al eliminar el hijo";
            }
            return respuesta;
        }
    }
}
