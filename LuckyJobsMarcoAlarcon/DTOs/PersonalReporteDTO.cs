using Lucky.Entitys;

namespace LuckyJobsMarcoAlarcon.DTOs
{
    public class PersonalReporteDTO
    {
        public int idpersonal { get; set; }
        public TipoDocumento oTipoDocumento { get; set; }
        public string nrodocumento { get; set; }
        public string nombrecompleto { get; set; }
        public string fecnacimiento { get; set; }
        public string fecingreso { get; set; }
    }
}
