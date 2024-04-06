using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public interface IPersonalService
    {
        Task<Respuesta> obtenerPersonalPorId(int id);
        Task<Respuesta> listarPersonal();
        Task<Respuesta> guardarPersonal(Personal personal);
        Task<Respuesta> eliminarPersonal(Personal personal);
    }
}
