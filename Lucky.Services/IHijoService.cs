using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Services
{
    public interface IHijoService
    {
        Task<Respuesta> obtenerHijoPorId(int id);
        Task<Respuesta> listarHijosPorPersonal(int idpers);
        Task<Respuesta> guardarHijo(Hijo hijo);
        Task<Respuesta> eliminarHijo(Hijo hijo);
    }
}
