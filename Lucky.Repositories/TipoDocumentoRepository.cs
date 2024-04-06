using Lucky.Entitys;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Repositories
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly Conexion _conexion;

        public TipoDocumentoRepository(IOptions<Conexion> conexion)
        {
            this._conexion = conexion.Value;
        }

        public async Task<Respuesta> listarTiposDocumento()
        {
            Respuesta respuesta = new Respuesta();
            List<TipoDocumento> listaTiposDocs = new List<TipoDocumento>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    string query = @"select td.tipodoc_id 'codtdoc', td.tdoc_abreviatura 'abrevtdoc'
                                    from TipoDocumento td where td.tdoc_flgactivo = 1";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            listaTiposDocs.Add(
                                new TipoDocumento()
                                {
                                    idtipodoc = Convert.ToInt32(dr["codtdoc"]),
                                    abrevtipodoc = dr["abrevtdoc"].ToString()
                                }
                            );
                        }
                    }

                    respuesta.status = true;
                    respuesta.data = listaTiposDocs;
                }
            }
            catch (Exception)
            {
                respuesta.status = false;
                respuesta.data = null;
            }

            return respuesta;
        }
    }
}
