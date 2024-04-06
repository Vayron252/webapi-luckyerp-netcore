using AutoMapper;
using Lucky.Entitys;
using Lucky.Services;
using LuckyJobsMarcoAlarcon.DTOs;
using LuckyJobsMarcoAlarcon.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuckyJobsMarcoAlarcon.Controllers
{
    [ApiController]
    [Route("api/lucky")]
    public class LuckyController : Controller
    {
        private readonly IPersonalService _personalService;
        private readonly IHijoService _hijoService;
        private readonly ITipoDocumentoService _tipoDocService;
        private readonly IMapper _mapper;

        public LuckyController(IPersonalService personalService, IHijoService hijoService, ITipoDocumentoService tipoDocService, IMapper mapper)
        {
            this._personalService = personalService;
            this._hijoService = hijoService;
            this._tipoDocService = tipoDocService;
            this._mapper = mapper;
        }

        [HttpGet("listarTiposDocumentos")]
        public async Task<ResponseModel> listarTiposDocumentos()
        {
            ResponseModel respmodel = new ResponseModel();
            Respuesta respuesta = await _tipoDocService.listarTiposDocumentos();
            if (respuesta.status)
            {
                respmodel.data = respuesta.data;
            }
            respmodel.status = respuesta.status;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpGet("listarPersonal")]
        public async Task<ResponseModel> listarPersonal()
        {
            ResponseModel respmodel = new ResponseModel();
            Respuesta respuesta = await _personalService.listarPersonal();
            if (respuesta.status)
            {
                List<Personal> listaPersonal = (List<Personal>)respuesta.data;
                List<PersonalReporteDTO> listaPersonalReporteDTOs = _mapper.Map<List<PersonalReporteDTO>>(listaPersonal);
                respmodel.data = listaPersonalReporteDTOs;
            }
            respmodel.status = respuesta.status;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpGet("obtenerPersonalPorId")]
        public async Task<ResponseModel> obtenerPersonalPorId([FromQuery] int id)
        {
            ResponseModel respmodel = new ResponseModel();
            Respuesta respuesta = await _personalService.obtenerPersonalPorId(id);
            if (respuesta.status)
            {
                respmodel.data = respuesta.data;
            }
            respmodel.status = respuesta.status;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpPost("guardarPersonal")]
        public async Task<ResponseModel> guardarPersonal([FromBody] Personal personal)
        {
            Respuesta respuesta = await _personalService.guardarPersonal(personal);
            ResponseModel respmodel = new ResponseModel();
            respmodel.status = respuesta.status;
            respmodel.data = respuesta.data;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpPost("eliminarPersonal")]
        public async Task<ResponseModel> eliminarPersonal([FromBody] PersonalEliminacionDTO personalEliminacionDTO)
        {
            Personal personal = _mapper.Map<Personal>(personalEliminacionDTO);
            Respuesta respuesta = await _personalService.eliminarPersonal(personal);
            ResponseModel respmodel = new ResponseModel();
            respmodel.status = respuesta.status;
            respmodel.data = respuesta.data;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpGet("listarHijosPorPersonal")]
        public async Task<ResponseModel> listarHijosPorPersonal([FromQuery] int idpers)
        {
            ResponseModel respmodel = new ResponseModel();
            Respuesta respuesta = await _hijoService.listarHijosPorPersonal(idpers);
            if (respuesta.status)
            {
                List<Hijo> listaHijos = (List<Hijo>)respuesta.data;
                List<HijoReporteDTO> listaHijosReporteDTOs = _mapper.Map<List<HijoReporteDTO>>(listaHijos);
                respmodel.data = listaHijosReporteDTOs;
            }
            respmodel.status = respuesta.status;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpGet("obtenerHijoPorId")]
        public async Task<ResponseModel> obtenerHijoPorId([FromQuery] int id)
        {
            ResponseModel respmodel = new ResponseModel();
            Respuesta respuesta = await _hijoService.obtenerHijoPorId(id);
            if (respuesta.status)
            {
                respmodel.data = respuesta.data;
            }
            respmodel.status = respuesta.status;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpPost("guardarHijo")]
        public async Task<ResponseModel> guardarHijo([FromBody] Hijo hijo)
        {
            Respuesta respuesta = await _hijoService.guardarHijo(hijo);
            ResponseModel respmodel = new ResponseModel();
            respmodel.status = respuesta.status;
            respmodel.data = respuesta.data;
            respmodel.message = respuesta.message;
            return respmodel;
        }

        [HttpPost("eliminarHijo")]
        public async Task<ResponseModel> eliminarHijo([FromBody] HijoEliminacionDTO hijoEliminacionDTO)
        {
            Hijo hijo = _mapper.Map<Hijo>(hijoEliminacionDTO);
            Respuesta respuesta = await _hijoService.eliminarHijo(hijo);
            ResponseModel respmodel = new ResponseModel();
            respmodel.status = respuesta.status;
            respmodel.data = respuesta.data;
            respmodel.message = respuesta.message;
            return respmodel;
        }
    }
}
