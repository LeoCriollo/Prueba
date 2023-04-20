using DoleEcIntranet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Data.AccessAdam
{
    public class AccessAdam
    {
        //get conecction
        string connstr = ConfigurationManager.ConnectionStrings["SqlAdam"].ConnectionString;
       


        /// <summary>
        /// 
        /// </summary>
        /// <param name="codEmpleado"></param>
        /// <returns></returns>
        public List<VacacionesModel> GetInfoVacaciones(string employeeId)
        {

            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<VacacionesModel> dto = null;

            //Define a query string that has a parameter for orderID.

            string sql = "SELECT A.COMPANIA,A.TRABAJADOR,REPLACE(D.NOMBRE,'/',' ') NOMBRE,A.PUESTO,C.DESCRIPCION," +
                 "E.CICLO_LABORAL,E.VAC_POR_CICLO,E.VAC_DISFRUTADAS,E.VAC_PROGRAMADAS " +
                 "FROM PLAZAS A, TRABAJADORES_GRALES B, PUESTOS C, TRABAJADORES D, SALDOS_VACACIONES E " +
                 "WHERE(A.SILLA_SUPERIOR IN(SELECT SILLA FROM PLAZAS WHERE ltrim(rtrim(TRABAJADOR)) = @employeeId) " +
                 "OR A.SILLA IN(SELECT SILLA FROM PLAZAS WHERE ltrim(rtrim(TRABAJADOR)) = @employeeId)) " +
                 "AND A.COMPANIA = B.COMPANIA AND A.TRABAJADOR = B.TRABAJADOR AND B.SIT_TRABAJADOR = 1 " +
                 "AND A.PUESTO = C.PUESTO AND A.TRABAJADOR = D.TRABAJADOR AND A.COMPANIA = E.COMPANIA " +
                 "AND A.TRABAJADOR = E.TRABAJADOR AND(VAC_POR_CICLO - VAC_DISFRUTADAS) > 0";
            SqlCommand cmdOrderID = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            cmdOrderID.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            cmdOrderID.Parameters["@employeeId"].Value = employeeId;

            try
            {
                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(cmdOrderID);
                datAd.Fill(dt);

                if(dt.Tables[0] != null)
                {
                    if(dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        dto = result.Select(s => new VacacionesModel()
                        {
                            subordinado = s.Field<string>("NOMBRE"),
                            puesto = s.Field<string>("PUESTO"),
                            cicloLaboral = s.Field<string>("CICLO_LABORAL"),
                            vacXCiclo = s.Field<decimal>("VAC_POR_CICLO"),
                            vacDisfrutadas = s.Field<decimal>("VAC_DISFRUTADAS"),
                           vacProgramadas = s.Field<Int16>("VAC_PROGRAMADAS")                           

                        }).ToList();

                    }
                }
            }
            catch(Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }
            return dto;




        }


        public CargasFamiliaresModel GetInfoCargasFamiliares(string employeeId)
        {
            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            CargasFamiliaresModel dto = null;


            //Define a query string that has a parameter for orderID.

            string sql = "SELECT DISTINCT A.COMPANIA,L.NOMBRE_CIA,A.TRABAJADOR,FECHA_ANTIGUEDAD,B.PUESTO,E.DESCRIPCION CARGO," +
                            "G.DESCRIPCION AREA, REPLACE(M.NOMBRE, '/', ' ') NOMBRE_TRAB,ISNULL(I.dato_10, ' ') SEG_MED,ISNULL(I.dato_04, ' ') ACTIVO " +
                                 "FROM TRABAJADORES_GRALES A LEFT OUTER JOIN INF_SOC_TRABAJADOR I ON(A.TRABAJADOR= I.trabajador AND I.indice_inf_soc= '9_BENEFIC' " +
                            "AND I.trabajador_persona= 1 AND I.dato_09 LIKE 'MEDI%'), PLAZAS B, PUESTOS E, REL_TRAB_AGR F, DATOS_AGR_TRAB G, COMPANIAS L, TRABAJADORES M " +
                            "WHERE A.SIT_TRABAJADOR = 1 AND A.TRABAJADOR = B.TRABAJADOR AND B.PUESTO = E.PUESTO " +
                            "AND(A.COMPANIA = F.COMPANIA AND A.TRABAJADOR = F.TRABAJADOR AND F.AGRUPACION = 'EC_DEPTO' AND F.DATO = G.DATO AND F.AGRUPACION = G.AGRUPACION) " +
                            "AND A.COMPANIA = L.COMPANIA AND A.TRABAJADOR = M.TRABAJADOR AND ltrim(rtrim(A.TRABAJADOR)) = @employeeId";
            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            commanCab.Parameters["@employeeId"].Value = employeeId;

            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        dto = result.Select(s => new CargasFamiliaresModel()
                        {
                            nomCia = s.Field<string>("NOMBRE_CIA"),
                            nomEmpleado = s.Field<string>("NOMBRE_TRAB"),
                            area = s.Field<string>("AREA"),
                            segMedico = s.Field<string>("SEG_MED"),
                            activo = s.Field<string>("ACTIVO"),
                            detalle = new List<CargasFamiliaresDet>()

                        }).FirstOrDefault();

                    }
                }

            }
            catch(Exception ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }

            if(dto != null)
            {
                //cargar detalle
                dto.detalle = this.GetInfoCargasFamiliaresDet(employeeId);
            }

            return dto;


        }


        private List<CargasFamiliaresDet> GetInfoCargasFamiliaresDet(string employeeId)
        {
            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<CargasFamiliaresDet> dto = null;


            //Define a query string that has a parameter for orderID.

            string sql = "Select a.trabajador,a.dato_01 Parentezco,a.persona_relacionada Codigo,c.nombre Nombre_Familiar," +
                        "Sexo =case when c.sexo = 1 then 'Masculino' else 'Femenino' End," +
                        "c.fecha_nacimiento Fecha_Nacimiento, cast((dbo.fn_edad(c.fecha_nacimiento, getdate())) as numeric(03)) as Edad " +
                        "From inf_soc_trabajador A, personas_relacionada c, trabajadores_grales b, trabajadores D " +
                        "Where indice_inf_soc = '1_FAMILIA' and dato_01 in('Hijastro', 'Hijo(a)', 'Esposa(o)') " +
                        "and a.trabajador = c.trabajador and a.persona_relacionada = c.persona_relacionada " +
                        "and a.trabajador = b.trabajador and b.sit_trabajador = 1 " +
                        "and a.trabajador = d.trabajador and b.clase_nomina in('EA', 'EF') " +
                        "and ltrim(rtrim(A.TRABAJADOR)) = @employeeId";
            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            commanCab.Parameters["@employeeId"].Value = employeeId;

            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        dto = result.Select(s => new CargasFamiliaresDet()
                        {
                            relacion = s.Field<string>("Parentezco"),
                            codigo = s.Field<string>("Codigo"),
                            nombreCarga = s.Field<string>("Nombre_Familiar"),
                            sexo = s.Field<string>("Sexo"),
                            fechaNacimiento = s.Field<DateTime?>("Fecha_Nacimiento"),
                            edad = s.IsNull("Edad") ? "" : s.Field<decimal>("Edad").ToString() + " Años" 
                        }).ToList();

                    }
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }
           

            return dto;


        }





        public GastosPersonalesResponse1 GastosPersonalesResponse1()
        {
            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            GastosPersonalesResponse1 dto = null;


            //Define a query string that has a parameter for orderID.

            string sql = "select A.real FRACCION_BASICA, B.real TOPE_GP from parametros_rh A, parametros_rh B" +
                " where A.parametro_rh = 'var_nom_29' and A.compania = '3475'" +
                " and B.parametro_rh = 'var_nom_48' and B.compania = '3475'";

            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            //commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            //commanCab.Parameters["@employeeId"].Value = employeeId;

            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        dto = result.Select(s => new GastosPersonalesResponse1()
                        {
                            fraccionBasica = s.Field<decimal>("FRACCION_BASICA"),
                            topeGp = s.Field<decimal>("TOPE_GP")


                        }).FirstOrDefault();

                    }
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }


            return dto;


        }



        public GastosPersonalesResponse2 GastosPersonalesResponse2(string employeeId)
        {
            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            GastosPersonalesResponse2 dto = null;


            //Define a query string that has a parameter for orderID.

            string sql = "select tg.compania as codCia, c.registro_fiscal RUC, c.nombre_cia," +
                        "tg.fecha_ingreso, replace(t.nombre,'/',' ') nombre, t.clave_unica cedula," +
                        "dbo.fn_recupera_tope_ingreso_anual(@employeeId) tope_ing_anual " +
                        "from trabajadores_grales tg, trabajadores t, companias c " +
                        "where tg.sit_trabajador = 1 and ltrim(rtrim(tg.trabajador))  = @employeeId " +
                        "and tg.compania = c.compania and tg.trabajador = t.trabajador";
                        
            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            commanCab.Parameters["@employeeId"].Value = employeeId;

            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        dto = result.Select(s => new GastosPersonalesResponse2()
                        {
                            codCia = s.Field<string>("codCia"),
                            ruc = s.Field<string>("RUC"),
                            nomCia = s.Field<string>("nombre_cia"),
                            nombre = s.Field<string>("nombre"),
                            cedula = s.Field<string>("cedula"),                            
                            topAnual = s.Field<decimal>("tope_ing_anual"),
                            fechaIngreso = s.Field<DateTime>("fecha_ingreso")

                        }).FirstOrDefault();

                    }
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }


            return dto;


        }
               
       

        public List<ResponseBusquedaEmpleado> BusquedaEmpleado1(string employeeId)
        {

            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<ResponseBusquedaEmpleado> datos = null;

            string sql = "select replace(t.nombre,'/',' ') nombre from trabajadores t " +
                "where ltrim(rtrim(t.trabajador)) = @employeeId";

            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            commanCab.Parameters["@employeeId"].Value = employeeId;


            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        datos = result.Select(s => new ResponseBusquedaEmpleado()
                        {
                            nombreEmpleado = s.Field<string>("nombre"),
                            codigoEmpleado = int.Parse(employeeId)


                        }).ToList();

                    }
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }


            return datos;


        }


        public List<ResponseBusquedaEmpleado> BusquedaEmpleado2(string nombre)
        {

            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<ResponseBusquedaEmpleado> datos = null;

            if (nombre.Trim().Length > 0)
            {

                string sql = "select ltrim(rtrim(trabajador)) trabajador, replace(t.nombre,'/',' ') nombre from trabajadores t " +
                    "where replace(t.nombre, '/', ' ') like @nombre";

                SqlCommand commanCab = new SqlCommand(sql, conn);

                //Define the parameter and its value.  
                commanCab.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                commanCab.Parameters["@nombre"].Value = '%' + nombre.TrimEnd().TrimStart().Replace(" ", "%") + '%';


                try
                {

                    //Open the connection.  
                    conn.Open();

                    DataSet dt = new DataSet();

                    //Run the command by using SqlDataReader.              

                    SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                    datAd.Fill(dt);

                    if (dt.Tables[0] != null)
                    {
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            var result = dt.Tables[0].AsEnumerable();

                            datos = result.Select(s => new ResponseBusquedaEmpleado()
                            {
                                nombreEmpleado = s.Field<string>("nombre"),
                                codigoEmpleado = int.Parse(s.Field<string>("trabajador"))


                            }).ToList();

                        }
                    }

                }
                catch (SqlException ex)
                {
                    conn.Close();
                }
                finally
                {
                    conn.Close();

                }

            }
            return datos;


        }


        public List<ResponseBusquedaEmpleado> BusquedaEmpleado3(string employeeId)
        {

            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<ResponseBusquedaEmpleado> datos = null;

            string sql = "select replace(t.nombre,'/',' ') nombre from trabajadores t , trabajadores_grales tg, companias c " +
                "where tg.sit_trabajador = 1 and tg.compania = c.compania and tg.trabajador = t.trabajador and " +
                " c.compania = '3463' and ltrim(rtrim(t.trabajador)) = @employeeId";

            SqlCommand commanCab = new SqlCommand(sql, conn);

            //Define the parameter and its value.  
            commanCab.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.NVarChar));
            commanCab.Parameters["@employeeId"].Value = employeeId;


            try
            {

                //Open the connection.  
                conn.Open();

                DataSet dt = new DataSet();

                //Run the command by using SqlDataReader.              

                SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                datAd.Fill(dt);

                if (dt.Tables[0] != null)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        var result = dt.Tables[0].AsEnumerable();

                        datos = result.Select(s => new ResponseBusquedaEmpleado()
                        {
                            nombreEmpleado = s.Field<string>("nombre"),
                            codigoEmpleado = int.Parse(employeeId)


                        }).ToList();

                    }
                }

            }
            catch (SqlException ex)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();

            }


            return datos;


        }


        public List<ResponseBusquedaEmpleado> BusquedaEmpleado4(string nombre)
        {

            //Creamos la coneccion
            SqlConnection conn = new SqlConnection(connstr);
            List<ResponseBusquedaEmpleado> datos = null;

            if (nombre.Trim().Length > 0)
            {

                string sql = "select ltrim(rtrim(t.trabajador)) trabajador, replace(t.nombre,'/',' ') nombre from trabajadores t , trabajadores_grales tg, companias c " +
                "where tg.sit_trabajador = 1 and tg.compania = c.compania and tg.trabajador = t.trabajador and " +
                "c.compania = '3463' and replace(t.nombre, '/', ' ') like @nombre";               

                SqlCommand commanCab = new SqlCommand(sql, conn);

                //Define the parameter and its value.  
                commanCab.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                commanCab.Parameters["@nombre"].Value = '%' + nombre.TrimEnd().TrimStart().Replace(" ", "%") + '%';


                try
                {

                    //Open the connection.  
                    conn.Open();

                    DataSet dt = new DataSet();

                    //Run the command by using SqlDataReader.              

                    SqlDataAdapter datAd = new SqlDataAdapter(commanCab);
                    datAd.Fill(dt);

                    if (dt.Tables[0] != null)
                    {
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            var result = dt.Tables[0].AsEnumerable();

                            datos = result.Select(s => new ResponseBusquedaEmpleado()
                            {
                                nombreEmpleado = s.Field<string>("nombre"),
                                codigoEmpleado = int.Parse(s.Field<string>("trabajador"))


                            }).ToList();

                        }
                    }

                }
                catch (SqlException ex)
                {
                    conn.Close();
                }
                finally
                {
                    conn.Close();

                }

            }
            return datos;


        }


    }
}