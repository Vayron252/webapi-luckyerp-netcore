using Lucky.Entitys;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Lucky.Repositories
{
    public class HijoRepository : IHijoRepository
    {
        private readonly Conexion _conexion;

        public HijoRepository(IOptions<Conexion> conexion)
        {
            this._conexion = conexion.Value;
        }

        public async Task<Respuesta> listarHijosActivosPorPersonal(int idpers)
        {
            Respuesta respuesta = new Respuesta();
            List<Hijo> listaHijos = new List<Hijo>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    string query = @"sp_listarhijosporpersonal";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idpersonal", idpers);
                    oconexion.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            listaHijos.Add(
                                new Hijo()
                                {
                                    idhijo = Convert.ToInt32(dr["codhijo"]),
                                    oTipoDocumento = new TipoDocumento()
                                    {
                                        idtipodoc = Convert.ToInt32(dr["codtdoc"]),
                                        abrevtipodoc = dr["abrevtdoc"].ToString()
                                    },
                                    nrodocumento = dr["nrodochijo"].ToString(),
                                    nombrecompleto = dr["nomcomplhijo"].ToString(),
                                    fecnacimiento = dr["fnachijo"].ToString()
                                }
                            );
                        }
                    }

                    respuesta.status = true;
                    respuesta.data = listaHijos;
                }
            }
            catch (Exception)
            {
                respuesta.status = false;
                respuesta.data = null;
            }

            return respuesta;
        }

        public async Task<Hijo> obtenerHijoPorId(int id)
        {
            Hijo hijolbd = new Hijo();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    string query = @"sp_obtenerhijoxid";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idhijo", id);
                    oconexion.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            hijolbd = new Hijo()
                            {
                                idhijo = Convert.ToInt32(dr["codhijo"]),
                                oTipoDocumento = new TipoDocumento()
                                {
                                    idtipodoc = Convert.ToInt32(dr["codtdoc"]),
                                    abrevtipodoc = dr["abrevtdoc"].ToString()
                                },
                                nrodocumento = dr["nrodochijo"].ToString(),
                                apepaterno = dr["apaterno"].ToString(),
                                apematerno = dr["amaterno"].ToString(),
                                nombre1 = dr["nombre1"].ToString(),
                                nombre2 = dr["nombre2"].ToString(),
                                nombrecompleto = dr["nomcomplhijo"].ToString(),
                                fecnacimiento = dr["fnachijo"].ToString(),
                                fecregistro = dr["freghijo"].ToString(),
                                usuregistra = dr["ureghijo"].ToString(),
                                fecmodifica = dr["fmodhijo"].ToString(),
                                usumodifica = dr["umodhijo"].ToString(),
                                flgactivo = Convert.ToBoolean(dr["activo"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                hijolbd = null;
            }

            return hijolbd;
        }

        public async Task<Respuesta> registrarHijo(Hijo objHijo)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarhijo", oconexion);
                    cmd.Parameters.AddWithValue("@idpersonal", objHijo.oPersonal.idpersonal);
                    cmd.Parameters.AddWithValue("@idtipodoc", objHijo.oTipoDocumento.idtipodoc);
                    cmd.Parameters.AddWithValue("@nrodocumento", objHijo.nrodocumento);
                    cmd.Parameters.AddWithValue("@apepaterno", objHijo.apepaterno);
                    cmd.Parameters.AddWithValue("@apematerno", objHijo.apematerno);
                    cmd.Parameters.AddWithValue("@nombre1", objHijo.nombre1);
                    cmd.Parameters.AddWithValue("@nombre2", (objHijo.nombre2 == "" || objHijo.nombre2 == null) ? DBNull.Value : objHijo.nombre2);
                    cmd.Parameters.AddWithValue("@fecnacimiento", objHijo.fecnacimiento);
                    cmd.Parameters.AddWithValue("@usuregistra", objHijo.usuregistra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta.status = true;
                    respuesta.data = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception)
            {
                respuesta.status = false;
                respuesta.data = -1;
            }

            return respuesta;
        }

        public async Task<Respuesta> editarHijo(Hijo objHijo)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarhijo", oconexion);
                    cmd.Parameters.AddWithValue("@idhijo", objHijo.idhijo);
                    cmd.Parameters.AddWithValue("@idtipodoc", objHijo.oTipoDocumento.idtipodoc);
                    cmd.Parameters.AddWithValue("@nrodocumento", objHijo.nrodocumento);
                    cmd.Parameters.AddWithValue("@apepaterno", objHijo.apepaterno);
                    cmd.Parameters.AddWithValue("@apematerno", objHijo.apematerno);
                    cmd.Parameters.AddWithValue("@nombre1", objHijo.nombre1);
                    cmd.Parameters.AddWithValue("@nombre2", (objHijo.nombre2 == "" || objHijo.nombre2 == null) ? DBNull.Value : objHijo.nombre2);
                    cmd.Parameters.AddWithValue("@fecnacimiento", objHijo.fecnacimiento);
                    cmd.Parameters.AddWithValue("@fecmodifica", objHijo.fecmodifica);
                    cmd.Parameters.AddWithValue("@usumodifica", objHijo.usumodifica);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta.status = true;
                    respuesta.data = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception)
            {
                respuesta.status = true;
                respuesta.data = false;
            }

            return respuesta;
        }

        public async Task<Respuesta> eliminarHijo(Hijo objHijo)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarhijo", oconexion);
                    cmd.Parameters.AddWithValue("@idhijo", objHijo.idhijo);
                    cmd.Parameters.AddWithValue("@fecmodifica", objHijo.fecmodifica);
                    cmd.Parameters.AddWithValue("@usumodifica", objHijo.usumodifica);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    await cmd.ExecuteNonQueryAsync();

                    respuesta.status = true;
                    respuesta.data = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception)
            {
                respuesta.status = false;
                respuesta.data = false;
            }

            return respuesta;
        }
    }
}
