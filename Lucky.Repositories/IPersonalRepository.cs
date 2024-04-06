using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Repositories
{
    public interface IPersonalRepository
    {
        Task<Personal> obtenerPersonalPorId(int id);
        Task<Respuesta> listarPersonal();
        Task<Respuesta> registrarPersonal(Personal objPersonal);
        Task<Respuesta> editarPersonal(Personal objPersonal);
        Task<Respuesta> eliminarPersonal(Personal objPersonal);
    }
}
