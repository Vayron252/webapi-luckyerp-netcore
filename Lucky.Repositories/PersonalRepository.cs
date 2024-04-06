using Lucky.Entitys;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly Conexion _conexion;

        public PersonalRepository(IOptions<Conexion> conexion)
        {
            this._conexion = conexion.Value;
        }

        public async Task<Personal> obtenerPersonalPorId(int id)
        {
            Personal personalbd = new Personal();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    string query = @"sp_obtenerpersonalxid";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idpersonal", id);
                    oconexion.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            personalbd = new Personal()
                            {
                                idpersonal = Convert.ToInt32(dr["codpers"]),
                                oTipoDocumento = new TipoDocumento()
                                {
                                    idtipodoc = Convert.ToInt32(dr["codtdoc"]),
                                    abrevtipodoc = dr["abrevtdoc"].ToString()
                                },
                                nrodocumento = dr["nrodocpers"].ToString(),
                                apepaterno = dr["apaterno"].ToString(),
                                apematerno = dr["amaterno"].ToString(),
                                nombre1 = dr["nombre1"].ToString(),
                                nombre2 = dr["nombre2"].ToString(),
                                nombrecompleto = dr["nomcomplpers"].ToString(),
                                fecnacimiento = dr["fnacpers"].ToString(),
                                fecingreso = dr["fingpers"].ToString(),
                                fecregistro = dr["fregpers"].ToString(),
                                usuregistra = dr["uregpers"].ToString(),
                                fecmodifica = dr["fmodpers"].ToString(),
                                usumodifica = dr["umodpers"].ToString(),
                                flgactivo = Convert.ToBoolean(dr["activo"])
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                personalbd = null;
            }

            return personalbd;
        }

        public async Task<Respuesta> listarPersonal()
        {
            Respuesta respuesta = new Respuesta();
            List<Personal> listaPersonal = new List<Personal>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    string query = @"sp_listarpersonal";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            listaPersonal.Add(
                                new Personal()
                                {
                                    idpersonal = Convert.ToInt32(dr["codpers"]),
                                    oTipoDocumento = new TipoDocumento()
                                    {
                                        idtipodoc = Convert.ToInt32(dr["codtdoc"]),
                                        abrevtipodoc = dr["abrevtdoc"].ToString()
                                    },
                                    nrodocumento = dr["nrodocpers"].ToString(),
                                    nombrecompleto = dr["nomcomplpers"].ToString(),
                                    fecnacimiento = dr["fnacpers"].ToString(),
                                    fecingreso = dr["fingpers"].ToString()
                                }
                            );
                        }
                    }

                    respuesta.status = true;
                    respuesta.data = listaPersonal;
                }
            }
            catch (Exception)
            {
                respuesta.status = false;
                respuesta.data = null;
            }

            return respuesta;
        }

        public async Task<Respuesta> registrarPersonal(Personal objPersonal)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarpersonal", oconexion);
                    cmd.Parameters.AddWithValue("@idtipodoc", objPersonal.oTipoDocumento.idtipodoc);
                    cmd.Parameters.AddWithValue("@nrodocumento", objPersonal.nrodocumento);
                    cmd.Parameters.AddWithValue("@apepaterno", objPersonal.apepaterno);
                    cmd.Parameters.AddWithValue("@apematerno", objPersonal.apematerno);
                    cmd.Parameters.AddWithValue("@nombre1", objPersonal.nombre1);
                    cmd.Parameters.AddWithValue("@nombre2", (objPersonal.nombre2 == "" || objPersonal.nombre2 == null) ? DBNull.Value : objPersonal.nombre2);
                    cmd.Parameters.AddWithValue("@fecnacimiento", objPersonal.fecnacimiento);
                    cmd.Parameters.AddWithValue("@usuregistra", objPersonal.usuregistra);
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

        public async Task<Respuesta> editarPersonal(Personal objPersonal)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_editarpersonal", oconexion);
                    cmd.Parameters.AddWithValue("@idpersonal", objPersonal.idpersonal);
                    cmd.Parameters.AddWithValue("@idtipodoc", objPersonal.oTipoDocumento.idtipodoc);
                    cmd.Parameters.AddWithValue("@nrodocumento", objPersonal.nrodocumento);
                    cmd.Parameters.AddWithValue("@apepaterno", objPersonal.apepaterno);
                    cmd.Parameters.AddWithValue("@apematerno", objPersonal.apematerno);
                    cmd.Parameters.AddWithValue("@nombre1", objPersonal.nombre1);
                    cmd.Parameters.AddWithValue("@nombre2", (objPersonal.nombre2 == "" || objPersonal.nombre2 == null) ? DBNull.Value : objPersonal.nombre2);
                    cmd.Parameters.AddWithValue("@fecnacimiento", objPersonal.fecnacimiento);
                    cmd.Parameters.AddWithValue("@fecmodifica", objPersonal.fecmodifica);
                    cmd.Parameters.AddWithValue("@usumodifica", objPersonal.usumodifica);
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

        public async Task<Respuesta> eliminarPersonal(Personal objPersonal)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminarpersonal", oconexion);
                    cmd.Parameters.AddWithValue("@idpersonal", objPersonal.idpersonal);
                    cmd.Parameters.AddWithValue("@fecmodifica", objPersonal.fecmodifica);
                    cmd.Parameters.AddWithValue("@usumodifica", objPersonal.usumodifica);
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
