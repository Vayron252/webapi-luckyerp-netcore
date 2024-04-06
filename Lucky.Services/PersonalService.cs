using Lucky.Entitys;
using Lucky.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalRepository _personalrepository;

        public PersonalService(IPersonalRepository personalRepository)
        {
            this._personalrepository = personalRepository;
        }

        public async Task<Respuesta> listarPersonal()
        {
            Respuesta respuesta = await _personalrepository.listarPersonal();
            if (!respuesta.status)
            {
                respuesta.message = "Ocurrió un error al listar el personal.";
            }
            return respuesta;
        }

        public async Task<Respuesta> obtenerPersonalPorId(int id)
        {
            Personal personal = await _personalrepository.obtenerPersonalPorId(id);
            Respuesta respuesta = new Respuesta();
            respuesta.status = true;
            respuesta.data = personal;
            if (personal == null)
            {
                respuesta.status = false;
                respuesta.message = "Ocurrió un error al obtener el personal.";
            }
            return respuesta;
        }

        public async Task<Respuesta> guardarPersonal(Personal personal)
        {
            Respuesta respuesta = new Respuesta();

            if (personal.idpersonal.Equals(0))
            {
                respuesta = await _personalrepository.registrarPersonal(personal);
                if (!respuesta.status)
                {
                    respuesta.message = "Ocurrió un error al registrar el personal.";
                }
                else
                {
                    int idgenerado = (int)respuesta.data;
                    respuesta.data = await _personalrepository.obtenerPersonalPorId(idgenerado);
                }
            } 
            else
            {
                DateTime dateTime = DateTime.Now;
                personal.fecmodifica = dateTime.ToString("dd/MM/yyyy hh:mm:ss");
                respuesta = await _personalrepository.editarPersonal(personal);
                if (!respuesta.status)
                {
                    respuesta.message = "Ocurrió un error al editar el personal.";
                }
                else
                {
                    respuesta.data = await _personalrepository.obtenerPersonalPorId(personal.idpersonal);
                }
            }

            return respuesta;
        }

        public async Task<Respuesta> eliminarPersonal(Personal personal)
        {
            DateTime dateTime = DateTime.Now;
            personal.fecmodifica = dateTime.ToString("dd/MM/yyyy hh:mm:ss");
            Respuesta respuesta = await _personalrepository.eliminarPersonal(personal);
            if (!respuesta.status)
            {
                respuesta.message = "Ocurrió un error al eliminar el personal";
            }
            return respuesta;
        }
    }
}
