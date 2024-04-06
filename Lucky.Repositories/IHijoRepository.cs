using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Repositories
{
    public interface IHijoRepository
    {
        Task<Hijo> obtenerHijoPorId(int id);
        Task<Respuesta> listarHijosActivosPorPersonal(int idpers);
        Task<Respuesta> registrarHijo(Hijo objHijo);
        Task<Respuesta> editarHijo(Hijo objHijo);
        Task<Respuesta> eliminarHijo(Hijo objHijo);
    }
}
