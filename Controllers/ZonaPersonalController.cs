using DoleEcIntranet.Data;
using DoleEcIntranet.Data.AccessAdam;
using DoleEcIntranet.Data.DataAdam;
using DoleEcIntranet.Models;
using DoleEcIntranet.Tools;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Controllers
{
    [DoleEcIntranetAuthorize]
    public class ZonaPersonalController : Controller
    {
        private EntityIntranet db = new EntityIntranet();
        private EntityAdam dba = new EntityAdam();
        private AccessAdam dbaa = new AccessAdam();
        // GET: ZonaPersonal
        public ActionResult Index()
        {
            return View();
        }


        #region Ficha_Personal
        //Ficha Personal
        /// <summary>
        /// Obtener Informacion de ficha personal
        /// </summary>
        /// <returns></returns>
        public ActionResult FichaPersonal()
        {
            FichaPersonalViewModel model = new FichaPersonalViewModel();

            String codigoEmpleado = GetCodigoEmpleado();

            if (!string.IsNullOrEmpty(codigoEmpleado))
            {
                //Consulta a ADAM
                var perfilUser = dba.Rol_usuarios_ADAM.FirstOrDefault(f => f.num_personal.Equals(codigoEmpleado));
                if (perfilUser != null)
                {
                    model.IdPersonal = perfilUser.num_personal;
                    model.Nombre = perfilUser.nombre;
                    model.Empresa = perfilUser.nom_cia;
                    model.ZonaGeo = perfilUser.des_zonageo;
                    model.Area = perfilUser.des_departamento;
                    model.Cargo = perfilUser.nom_cargo;
                    model.JefeInmediato = perfilUser.nombre_jefe;
                    model.FechaDeIngreso = perfilUser.fh_priming;
                    model.FechaDeAntiguedad = perfilUser.fh_priming;
                    model.GrupoDeNomina = perfilUser.des_grpnomina;
                    model.CentroDeCosto = perfilUser.des_ccosto;
                }

            }


            return View("FichaPersonal", model);

        }
        #endregion

        #region Slip_Pago

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SlipPagoIndex()
        {
            String codEmpleado = GetCodigoEmpleado();
            List<GenericDto> listaO = new List<GenericDto>();

            var periodos = dba.Rol_slip_cab_ADAM.Where(w => w.trabajador.Trim().Equals(codEmpleado)).ToList();

            if (periodos.Any())
            {
                foreach (var item in periodos)
                {
                    string meses = "";

                    if (item.sec_anio == 13)
                        meses = "BONO";
                    else
                        meses = item.sec_anio.ToString().PadLeft(3, '0');

                    string yearPer = item.anio + "-" + meses;

                    GenericDto dato = new GenericDto
                    {
                        Id = yearPer,
                        Name = yearPer
                    };
                    listaO.Add(dato);
                }
            }

            ViewBag.YearPer = new SelectList(listaO.OrderByDescending(o => o.Id), "Id", "Name");


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult SlipPago(RequestConsultaSlipPago request)
        {
            SlipPago model = new SlipPago();

            if (ModelState.IsValid)
            {
                string codEmpleado = GetCodigoEmpleado();
                //Consultar
                var infoN = dba.Rol_slip_det_ADAM
                    .Where(w => w.trabajador.Trim().Equals(codEmpleado)
                                && w.anio == request.year && w.sec_anio == request.periodo)
                          .ToList();

                if (infoN.Any())
                {
                    var cab = dba.Rol_slip_cab_ADAM.First(f=> f.trabajador.Trim().Equals(codEmpleado) 
                    && f.anio == request.year && f.sec_anio == request.periodo);

                    var infoPer = dba.Rol_usuarios_ADAM.FirstOrDefault(f => f.num_personal.Trim().Equals(codEmpleado));
                    string cuenta = "";
                    if (infoPer != null)
                    {
                        cuenta = infoPer.cuenta_banco + " - " + infoPer.desc_banco;
                    }

                    //Mapear Cabecera
                    model.cabecera = new SliPagoCab()
                    {
                        compania = cab.nombre_cia,
                        nombre = cab.nombre,
                        cedula = cab.cedula,
                        mes = request.periodo==13 ? "BONO" : cab.sec_anio + "    " + cab.fecha_inicio_periodo.Value.ToShortDateString() + "  AL  " + cab.fecha_fin_periodo.Value.ToShortDateString(),
                        centroCosto = cab.centro_costo + "  " + cab.desc_ccosto,
                        fechaPago = cab.fecha_fin_periodo.Value.Date,
                        codEmpleado = cab.trabajador,
                        cuenta = cuenta,
                        // mesP = cab.sec_anio.Value.ToString().PadLeft(3, '0'),
                        cargo = cab.desc_carg_trabajador
                    };

                    // Add detalles
                    model.detalles = new List<SlipPagoDet>();

                    foreach (var item in infoN)
                    {
                        //if(item.concepto == 5380)
                        //{
                        //    int a = 89;
                        //}
                        //Total haberes y Descuentos
                        if (item.tipo_transaccion == (int)Tools.Enum.Transacciones.Debitos_1 || item.tipo_transaccion == (int)Tools.Enum.Transacciones.Debitos_2)
                            model.totalHaberes += item.importe.Value;


                        else if (item.tipo_transaccion == (int)Tools.Enum.Transacciones.Creditos_1)
                            model.totalDescuentos += item.importe.Value;

                        //Se agrupa en una sola linea los movimientos que tienen el mismo concepto
                        if (model.detalles.Any(f => f.idConcepto == item.concepto && f.tipoMov == item.tipo_transaccion.Value))
                        {
                            var line1 = model.detalles.First(f => f.idConcepto == item.concepto && f.tipoMov == item.tipo_transaccion.Value);
                            line1.tiempo += item.tiempo;
                            line1.importe += item.importe.Value;
                        }
                        else
                        {


                            SlipPagoDet line = new SlipPagoDet()
                            {
                                tipoMov = item.tipo_transaccion.Value,
                                idConcepto = item.concepto,
                                descConcepto = item.desc_concepto,
                                tiempo = item.tiempo,
                                importe = item.importe.Value
                            };

                            model.detalles.Add(line);
                        }
                    }

                    //Neto a Pagar
                    model.netoAPagar = model.totalHaberes - model.totalDescuentos;

                }


            }
            return PartialView("_SlipPago", model);
        }

        #endregion

        #region Impto_Renta

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImpuestoRentaIndex()
        {
            String codEmpleado = GetCodigoEmpleado();
            List<GenericDto> listaO = new List<GenericDto>();

            var periodos = dba.Rol_slip_cab_ADAM.Where(w => w.trabajador.Trim().Equals(codEmpleado)).ToList();

            if (periodos.Any())
            {
                foreach (var item in periodos)
                {
                    string yearPer = item.anio + "-" + item.sec_anio.ToString().PadLeft(3, '0');
                    GenericDto dato = new GenericDto
                    {
                        Id = yearPer,
                        Name = yearPer
                    };
                    listaO.Add(dato);
                }
            }

            ViewBag.YearPer = new SelectList(listaO.OrderByDescending(o => o.Id), "Id", "Name");


            return View();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult ImptoRta(RequestConsultaImptoRta request)
        {
            ImptoRta model = new ImptoRta();
            if (ModelState.IsValid)
            {
                string codEmpleado = GetCodigoEmpleado();

                //Consultar
                var infoN = dba.Rol_ImpRenta_ADAM
                    .Where(w => w.trabajador.Trim().Equals(codEmpleado)
                                && w.anio == request.year && w.sec_anio == request.periodo)
                                .ToList();

                if (infoN.Any())
                {
                    var cab = infoN.First();

                    //Mapear Cabecera
                    model.cab = new ImptoRtaCab
                    {
                        compania = cab.nombre_cia,
                        nombre = cab.nombre,
                        cedula = cab.cedula,
                        mes = cab.sec_anio + "    " + cab.fecha_inicio_periodo.Value.ToShortDateString() + "  AL  " + cab.fecha_fin_periodo.Value.ToShortDateString(),
                        centroCosto = cab.centro_costo + "  " + cab.desc_ccosto,
                        fechaPago = cab.fecha_fin_periodo.Value.Date,
                        codEmpleado = cab.trabajador,
                        //cuenta = infoPer.cuenta_banco + " - " + infoPer.desc_banco,
                        // mesP = cab.sec_anio.Value.ToString().PadLeft(3, '0'),
                        cargo = cab.desc_carg_trabajador
                    };

                    model.det = new List<ImptoRtaDet>();

                    List<int> listaGastos = new List<int>();
                    listaGastos.Add(9800);
                    listaGastos.Add(9810);
                    listaGastos.Add(9820);
                    listaGastos.Add(9830);
                    listaGastos.Add(9840);
                    List<int> grupo1 = new List<int>();
                    grupo1.Add(16520);
                    grupo1.Add(16500);
                    List<int> grupo2 = new List<int>();
                    grupo2.Add(6694);
                    grupo2.Add(6695);
                    grupo2.Add(16510);
                    grupo2.Add(16610);
                    List<int> grupo3 = new List<int>();
                    grupo3.Add(16570);
                    List<int> grupo4 = new List<int>();
                    grupo4.Add(16560);
                    grupo4.Add(16580);
                    List<int> grupo5 = new List<int>();
                    grupo5.Add(10000);


                    //Primero agregamos los valores por proyecciones de gastos que se declaran en el primer mes del year
                    var pGastos = dba.Rol_ImpRenta_ADAM
                    .Where(w => w.trabajador.Trim().Equals(codEmpleado)
                                && w.anio == request.year && listaGastos.Contains(w.concepto))
                                .ToList();
                    if (pGastos.Any())
                    {
                        foreach (var pg in pGastos)
                        {
                            var line = model.det.FirstOrDefault(f => f.idConcepto == pg.concepto);
                            if (line != null)
                            {
                                line.valor = line.valor + (pg.importe.HasValue ? (pg.importe.Value * -1) : 0);
                            }
                            else
                            {
                                ImptoRtaDet det = new ImptoRtaDet()
                                {
                                    idConcepto = pg.concepto,
                                    descConcepto = pg.desc_concepto,
                                    valor = pg.importe.HasValue ? (pg.importe.Value * -1) : 0,
                                    tipoGrupo = 3

                                };
                                model.det.Add(det);
                            }

                        }
                    }

                    foreach (var item in infoN)
                    {
                        ImptoRtaDet det = new ImptoRtaDet();
                        det.idConcepto = item.concepto;
                        det.descConcepto = item.desc_concepto;


                        if (grupo1.Contains(item.concepto))
                            det.tipoGrupo = 1;
                        else if (grupo2.Contains(item.concepto))
                            det.tipoGrupo = 2;
                        else if (grupo3.Contains(item.concepto))
                            det.tipoGrupo = 3;
                        else if (grupo4.Contains(item.concepto))
                            det.tipoGrupo = 4;
                        else if (grupo5.Contains(item.concepto))
                            det.tipoGrupo = 5;

                        if (det.tipoGrupo == 3)
                            det.valor = item.importe.HasValue ? (item.importe.Value) * -1 : 0;
                        else
                            det.valor = item.importe.HasValue ? item.importe.Value : 0;

                        if (det.tipoGrupo > 0)
                            model.det.Add(det);

                    }

                    decimal dif_16560 = 0;
                    if (model.det.Any(f => f.idConcepto == 16560))
                    {
                        dif_16560 = model.det.First(f => f.idConcepto == 16560).valor = model.det.Where(f => f.idConcepto == 10000 || f.idConcepto == 16560 || f.idConcepto == 16580).Sum(s => s.valor);

                    }

                    model.totalSueldos = model.det.Where(w => w.tipoGrupo == 1).Sum(f => f.valor);
                    model.totalIngresosAnuales = model.det.Where(w => w.tipoGrupo == 1 || w.tipoGrupo == 2).Sum(f => f.valor);
                    decimal valorI = decimal.Parse("0.0945");
                    model.aporteIess = (model.totalSueldos * valorI) * -1;
                    model.baseImponible = model.totalIngresosAnuales - Math.Abs(model.det.Where(w => w.tipoGrupo == 3).Sum(f => f.valor)) - Math.Abs(model.aporteIess);
                    decimal dif_16580 = model.det.Any(f => f.idConcepto == 16580) ? model.det.First(f => f.idConcepto == 16580).valor : 0;
                    model.diferencia = dif_16560 - dif_16580;
                    model.totalRetFecha = model.det.Where(f => f.idConcepto == 10000 || f.idConcepto == 16580).Sum(s => s.valor);

                }


                int per = request.periodo + 1;
                int perM = request.periodo - 1;
                ViewBag.desc1 = " hasta fin de Año (Meses " + per + "-12)";
                ViewBag.desc2 = "Corriente (Mes " + request.periodo + ")";

                ViewBag.g41 = "Proyectado a Pagar Año " + request.year;
                if (request.periodo == 1)
                    ViewBag.g42 = "Impuesto Retenido (Meses " + 1 + ")";
                else if (request.periodo == 12)
                    ViewBag.g42 = "Impuesto Retenido (Meses " + 1 + "-" + 12 + ")";
                else
                    ViewBag.g42 = "Impuesto Retenido (Meses " + 1 + "-" + perM + ")";



            }





            return PartialView("_ImptoRta", model);
        }

        #endregion

        #region Vacaciones
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VacacionesIndex()
        {
            string codEmpleado = this.GetCodigoEmpleado();

            List<VacacionesModel> model = dbaa.GetInfoVacaciones(codEmpleado);

            Rol_usuarios_ADAM perfilUser = dba.Rol_usuarios_ADAM.FirstOrDefault(f => f.num_personal.Equals(codEmpleado));

            if (perfilUser != null)
            {

                ViewBag.JefeArea = perfilUser.nombre_jefe;
                ViewBag.Nombre = perfilUser.nombre;
                ViewBag.Area = perfilUser.des_departamento;
            }
            else
            {
                ViewBag.JefeArea = "";
                ViewBag.Nombre = "";
                ViewBag.Area = "";
            }

            return View(model);

        }

        #endregion

        public ActionResult RedirectToUrl1()
        {
            string url = "http://172.28.90.108/pcsistel/comprueba.asp?usuario_intradole=";
            url += GetCodigoEmpleado() + "J";
            return Redirect(url);

        }

        public ActionResult RedirectToUrl2()
        {
            string url = "http://172.28.90.108/pcsistel/comprueba.asp?usuario_intradole=";
            url += GetCodigoEmpleado();
            return Redirect(url);

        }


        #region Cargas Familiares

        public ActionResult CargasFamiliaresIndex()
        {

            CargasFamiliaresModel model = dbaa.GetInfoCargasFamiliares(this.GetCodigoEmpleado());



            return View(model);
        }
        #endregion


        #region Consulta_Slip_Pago

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminSlipPago + "," + Tools.Enum.RolAdminSlipPagoPuerto)]
        public ActionResult ConsultaSlipPagoIndex()
        {

            List<DtoGeneric> listaTipoBusqueda = new List<DtoGeneric>();
            listaTipoBusqueda.Add(new DtoGeneric() { Id = 1, Name = "Código" });
            listaTipoBusqueda.Add(new DtoGeneric() { Id = 2, Name = "Nombre" });


            ViewBag.tipoBusqueda = new SelectList(listaTipoBusqueda.OrderBy(o => o.Id), "Id", "Name");


            return View();
        }


        [HttpPost]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminSlipPago + "," + Tools.Enum.RolAdminSlipPagoPuerto)]
        public PartialViewResult ConsultaSlipPagoIndex(FilterSlipPago filter)
        {
            List<ResponseBusquedaEmpleado> model = new List<ResponseBusquedaEmpleado>();
            if (ModelState.IsValid)
            {

                if (User.IsInRole(Tools.Enum.RolAdminSlipPago))
                {
                    // por codigo de empleado
                    if (filter.tipoBusqueda == 1)
                    {
                        model = dbaa.BusquedaEmpleado1(filter.busqueda);

                    }
                    else if (filter.tipoBusqueda == 2) // por nombre
                    {
                        model = dbaa.BusquedaEmpleado2(filter.busqueda);
                    }
                }else if (User.IsInRole(Tools.Enum.RolAdminSlipPagoPuerto))
                {
                    // por codigo de empleado
                    if (filter.tipoBusqueda == 1)
                    {
                        model = dbaa.BusquedaEmpleado3(filter.busqueda);

                    }
                    else if (filter.tipoBusqueda == 2) // por nombre
                    {
                        model = dbaa.BusquedaEmpleado4(filter.busqueda);
                    }
                }

            }

            return PartialView("_ConsultaSlipPagoIndex", model);

        }


        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminSlipPago + "," + Tools.Enum.RolAdminSlipPagoPuerto)]

        public ActionResult ConsultaSlip1(int id)
        {
            String codEmpleado = id.ToString();
            List<GenericDto> listaO = new List<GenericDto>();

            List<Rol_slip_cab_ADAM> periodos = new List<Rol_slip_cab_ADAM>();

            if (User.IsInRole(Tools.Enum.RolAdminSlipPago))
            {
                periodos = dba.Rol_slip_cab_ADAM.Where(w => w.trabajador.Trim().Equals(codEmpleado)).ToList();
                
            }
            else if (User.IsInRole(Tools.Enum.RolAdminSlipPagoPuerto))
            {
                periodos = dba.Rol_slip_cab_ADAM.Where(w => w.trabajador.Trim().Equals(codEmpleado) && w.compania.Trim().Equals("3463")).ToList();

            }

            if (periodos.Any())
            {
                foreach (var item in periodos)
                {
                    string meses = "";

                    if (item.sec_anio == 13)
                        meses = "BONO";
                    else
                        meses = item.sec_anio.ToString().PadLeft(3, '0');

                    string yearPer = item.anio + "-" + meses;

                    //string yearPer = item.anio + "-" + item.sec_anio.ToString().PadLeft(3, '0');
                    GenericDto dato = new GenericDto
                    {
                        Id = yearPer,
                        Name = yearPer
                    };
                    listaO.Add(dato);
                }
            }

            ViewBag.YearPer = new SelectList(listaO.OrderByDescending(o => o.Id), "Id", "Name");
            ViewBag.CodigoEmpleado = id;

            return View();

        }


        [HttpPost]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminSlipPago + "," + Tools.Enum.RolAdminSlipPagoPuerto)]
        public PartialViewResult ConsultaSlip2(RequestConsultaSlipPago2 request)
        {
            SlipPago model = new SlipPago();

            if (ModelState.IsValid)
            {
                string codEmpleado = request.codEmpleado.ToString();
                //Consultar

                List<Rol_slip_det_ADAM> infoN = new List<Rol_slip_det_ADAM>();


                if (User.IsInRole(Tools.Enum.RolAdminSlipPago))
                {
                    infoN = dba.Rol_slip_det_ADAM
                                       .Where(w => w.trabajador.Trim().Equals(codEmpleado)
                                                   && w.anio == request.year && w.sec_anio == request.periodo)
                                             .ToList();
                }
                else if (User.IsInRole(Tools.Enum.RolAdminSlipPagoPuerto))
                {
                    infoN = dba.Rol_slip_det_ADAM
                                       .Where(w => w.trabajador.Trim().Equals(codEmpleado)
                                                   && w.anio == request.year && w.sec_anio == request.periodo && w.compania.Trim().Equals("3463"))
                                             .ToList();
                }               

                if (infoN.Any())
                {
                    //var cab = infoN.First();
                    var cab = dba.Rol_slip_cab_ADAM.First(f => f.trabajador.Trim().Equals(codEmpleado)
                    && f.anio == request.year && f.sec_anio == request.periodo);
                    var infoPer = dba.Rol_usuarios_ADAM.FirstOrDefault(f => f.num_personal.Trim().Equals(codEmpleado));
                    string cuenta = "";
                    if (infoPer != null)
                    {
                        cuenta = infoPer.cuenta_banco + " - " + infoPer.desc_banco;
                    }

                    //Mapear Cabecera
                    model.cabecera = new SliPagoCab()
                    {
                        compania = cab.nombre_cia,
                        nombre = cab.nombre,
                        cedula = cab.cedula,
                        mes = request.periodo == 13 ? "BONO" : cab.sec_anio + "    " + cab.fecha_inicio_periodo.Value.ToShortDateString() + "  AL  " + cab.fecha_fin_periodo.Value.ToShortDateString(),
                        centroCosto = cab.centro_costo + "  " + cab.desc_ccosto,
                        fechaPago = cab.fecha_fin_periodo.Value.Date,
                        codEmpleado = cab.trabajador,
                        cuenta = cuenta,
                        // mesP = cab.sec_anio.Value.ToString().PadLeft(3, '0'),
                        cargo = cab.desc_carg_trabajador
                    };

                    // Add detalles
                    model.detalles = new List<SlipPagoDet>();

                    foreach (var item in infoN)
                    {
                        //if(item.concepto == 5380)
                        //{
                        //    int a = 89;
                        //}
                        //Total haberes y Descuentos
                        if (item.tipo_transaccion == (int)Tools.Enum.Transacciones.Debitos_1 || item.tipo_transaccion == (int)Tools.Enum.Transacciones.Debitos_2)
                            model.totalHaberes += item.importe.Value;


                        else if (item.tipo_transaccion == (int)Tools.Enum.Transacciones.Creditos_1)
                            model.totalDescuentos += item.importe.Value;

                        //Se agrupa en una sola linea los movimientos que tienen el mismo concepto
                        if (model.detalles.Any(f => f.idConcepto == item.concepto && f.tipoMov == item.tipo_transaccion.Value))
                        {
                            var line1 = model.detalles.First(f => f.idConcepto == item.concepto && f.tipoMov == item.tipo_transaccion.Value);
                            line1.tiempo += item.tiempo;
                            line1.importe += item.importe.Value;
                        }
                        else
                        {


                            SlipPagoDet line = new SlipPagoDet()
                            {
                                tipoMov = item.tipo_transaccion.Value,
                                idConcepto = item.concepto,
                                descConcepto = item.desc_concepto,
                                tiempo = item.tiempo,
                                importe = item.importe.Value
                            };

                            model.detalles.Add(line);
                        }
                    }

                    //Neto a Pagar
                    model.netoAPagar = model.totalHaberes - model.totalDescuentos;

                }


            }
            return PartialView("_SlipPago", model);
        }


        #endregion


        #region Declarion_Gastos_Personales
        #region periodo1
        //  [AllowAnonymous]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ///
        public ActionResult GastosPersonalesIndex()
        {

            int codEmpleado = int.Parse(this.GetCodigoEmpleado());
            int year = DateTime.Now.Year;


            //1.- Validar periodo segun corresponda
            int valPeriodo = 1; // asignar segun corresponda
            bool notEnabled = true; // asignar segun corresponda
            string endPeriod = "final"; // Asignar segun corresponda


            //2.- Consultar data si existe
            var line = db.GastosPersonales.FirstOrDefault(a => a.CodigoEmpleado == codEmpleado && a.Year == year && a.Secuencia == valPeriodo);
            if (line != null)
            {
                // a.- traer informacion del periodo actual
                GastosPersonalesModel model = new GastosPersonalesModel()
                {
                    RucEmpleador = line.RucEmpleador,
                    RazonSocial = line.RazonSocial,
                    CodigoEmpleado = line.CodigoEmpleado,
                    Cedula = line.Cedula,
                    Year = line.Year,
                    FechaEntrega = line.FechaEntrega,
                    NombresApellidos = line.NombresApellidos,
                    C_103 = line.C_103,
                    C_104 = line.C_104,
                    C_105 = line.C_105,
                    C_106 = line.C_106,
                    C_107 = line.C_107,
                    C_108 = line.C_108,
                    C_109 = line.C_109,
                    C_110 = line.C_110,
                    C_111 = line.C_111,
                    C_112 = line.C_112,
                    C_113 = line.C_113.HasValue ? line.C_113.Value : 0.00M,
                    classReadOnly = notEnabled ? "" : "@readonly=true",
                    Estado = line.Estado,
                    endPeriod = "final1"

                };

                return View(model);


            }
            else
            {

                //mensaje de error

                //Get info
                var data = dbaa.GastosPersonalesResponse2(this.GetCodigoEmpleado());
                var val1 = dbaa.GastosPersonalesResponse1();

                //cargar plantilla 
                GastosPersonalesModel model = new GastosPersonalesModel()
                {
                    RucEmpleador = data.ruc,
                    RazonSocial = data.nomCia,
                    CodigoEmpleado = codEmpleado,
                    Cedula = data.cedula,
                    Year = year,
                    FechaEntrega = DateTime.Now.Date,
                    NombresApellidos = data.nombre,
                    classReadOnly = notEnabled ? "" : "@readonly=true",
                    Estado = "1",
                    endPeriod = "inicial",
                    topeG = val1.topeGp.ToString()

                };

                return View(model);



            }





            // return View();

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GastosPersonalesIndex(GastosPersonalesModel request)
        {
            //GastosPersonalesResponse1 val1 = new GastosPersonalesResponse1()
            //{
            //    fraccionBasica = 11310.00M,
            //    topeGp = 3675.75M
            //};  // Cargar desde base

            request.FechaEntrega = DateTime.Now.Date;

            var val1 = dbaa.GastosPersonalesResponse1();

            //Obtener info para validacion 2
            var data = dbaa.GastosPersonalesResponse2(this.GetCodigoEmpleado());


            //Validaciones Iniciales
            //decimal validateR = val1.fraccionBasica * 1.3M;
            decimal validateR = val1.topeGp;

            if (request.C_111 > val1.topeGp)
                ModelState.AddModelError("C_111", "No puede ser mayor a " + val1.topeGp.ToString());
            if (request.C_110 > val1.topeGp)
                ModelState.AddModelError("C_110", "No puede ser mayor a " + val1.topeGp.ToString());
            if (request.C_109 > val1.topeGp)
                ModelState.AddModelError("C_109", "No puede ser mayor a " + val1.topeGp.ToString());
            if (request.C_107 > val1.topeGp)
                ModelState.AddModelError("C_107", "No puede ser mayor a " + val1.topeGp.ToString());
            if (request.C_106 > val1.topeGp)
                ModelState.AddModelError("C_106", "No puede ser mayor a " + val1.topeGp.ToString());


            decimal sumR = request.C_106 + request.C_107 + request.C_108 + request.C_109 + request.C_110 + request.C_111;

            if (sumR > validateR)
                ModelState.AddModelError("C_112", "No puede ser mayor a " + validateR.ToString());

            if (sumR > data.topAnual)
                ModelState.AddModelError("C_112", "No puede ser mayor a " + data.topAnual.ToString());




            if (ModelState.IsValid)
            {

                int codEmpleado = int.Parse(this.GetCodigoEmpleado());
                int getPeriodo = 1; // asignar segun corresponda
                bool isEnabled = true; // asignar segun corresponda
                int year = DateTime.Now.Year;
                request.FechaEntrega = DateTime.Now.Date;

                var line = db.GastosPersonales.FirstOrDefault(a => a.CodigoEmpleado == codEmpleado && a.Year == year && a.Secuencia == getPeriodo);
                if (line != null)
                {

                    line.C_103 = request.C_103;
                    line.C_104 = request.C_104;
                    line.C_105 = request.C_105;
                    line.C_106 = request.C_106;
                    line.C_107 = request.C_107;
                    line.C_108 = request.C_108;
                    line.C_109 = request.C_109;
                    line.C_110 = request.C_110;
                    line.C_111 = request.C_111;
                    line.C_112 = request.C_112;
                    line.C_113 = request.C_113;
                    line.FechaActualizacion = DateTime.Now;
                    line.UsuarioActualizacion = this.GetIdUser();// revisar traer valor adecuado
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //si no existe lo grabamos
                    GastosPersonales gastos = new GastosPersonales()
                    {
                        IdUser = this.GetIdUser(), // revisar traer valor adecuado
                        Cedula = request.Cedula,
                        CodigoEmpleado = codEmpleado,
                        Secuencia = 1,
                        RazonSocial = request.RazonSocial,
                        Estado = "1",
                        Year = year,
                        FechaCreacion = DateTime.Now,
                        FechaEntrega = DateTime.Now.Date,
                        NombresApellidos = request.NombresApellidos,
                        RucEmpleador = request.RucEmpleador,
                        UsuarioCreacion = this.GetIdUser(),  //revisar traer el valor adecuado
                        C_103 = request.C_103,
                        C_104 = request.C_104,
                        C_105 = request.C_105,
                        C_106 = request.C_106,
                        C_107 = request.C_107,
                        C_108 = request.C_108,
                        C_109 = request.C_109,
                        C_110 = request.C_110,
                        C_111 = request.C_111,
                        C_112 = request.C_112,
                        C_113 = request.C_113

                    };
                    db.GastosPersonales.Add(gastos);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");


                }

            }


            return View(request);
        }

        #endregion

        //#region periodo2
        //public ActionResult GastosPersonalesIndex()
        //{

        //    int codEmpleado = int.Parse(this.GetCodigoEmpleado());
        //    int year = DateTime.Now.Year;


        //    //1.- Validar periodo segun corresponda
        //    int valPeriodo = 2; // asignar segun corresponda
        //    bool notEnabled = true; // asignar segun corresponda
        //    string endPeriod = "final"; // Asignar segun corresponda


        //    //2.- Consultar data si existe
        //    var line = db.GastosPersonales.FirstOrDefault(a => a.CodigoEmpleado == codEmpleado && a.Year == year && a.Secuencia == valPeriodo);
        //    if (line != null)
        //    {
        //        // a.- traer informacion del periodo actual
        //        GastosPersonalesModel model = new GastosPersonalesModel()
        //        {
        //            RucEmpleador = line.RucEmpleador,
        //            RazonSocial = line.RazonSocial,
        //            CodigoEmpleado = line.CodigoEmpleado,
        //            Cedula = line.Cedula,
        //            Year = line.Year,
        //            FechaEntrega = line.FechaEntrega,
        //            NombresApellidos = line.NombresApellidos,
        //            C_103 = line.C_103,
        //            C_104 = line.C_104,
        //            C_105 = line.C_105,
        //            C_106 = line.C_106,
        //            C_107 = line.C_107,
        //            C_108 = line.C_108,
        //            C_109 = line.C_109,
        //            C_110 = line.C_110,
        //            C_111 = line.C_111,
        //            C_112 = line.C_112,
        //            classReadOnly = notEnabled ? "" : "@readonly=true",
        //            Estado = line.Estado,
        //            endPeriod = "final"

        //        };

        //        return View(model);


        //    }
        //    else
        //    {
        //        DateTime fechaIngresoVal = new DateTime(2021, 1, 30);
        //        var data = dbaa.GastosPersonalesResponse2(this.GetCodigoEmpleado());
        //        var line1 = db.GastosPersonales.FirstOrDefault(a => a.CodigoEmpleado == codEmpleado && a.Year == year && a.Secuencia == 1);
        //        if (line1 != null)
        //        {

        //            //Get info

        //            GastosPersonalesModel model = new GastosPersonalesModel()
        //            {
        //                RucEmpleador = line1.RucEmpleador,
        //                RazonSocial = line1.RazonSocial,
        //                CodigoEmpleado = line1.CodigoEmpleado,
        //                Cedula = line1.Cedula,
        //                Year = line1.Year,
        //                FechaEntrega = line1.FechaEntrega,
        //                NombresApellidos = line1.NombresApellidos,
        //                C_103 = line1.C_103,
        //                C_104 = line1.C_104,
        //                C_105 = line1.C_105,
        //                C_106 = line1.C_106,
        //                C_107 = line1.C_107,
        //                C_108 = line1.C_108,
        //                C_109 = line1.C_109,
        //                C_110 = line1.C_110,
        //                C_111 = line1.C_111,
        //                C_112 = line1.C_112,
        //                classReadOnly = notEnabled ? "" : "@readonly=true",
        //                Estado = "1",
        //                endPeriod = "final"

        //            };


        //            return View(model);
        //        }
        //        else if (data.fechaIngreso > fechaIngresoVal)
        //        {
        //            //cargar plantilla 
        //            GastosPersonalesModel model = new GastosPersonalesModel()
        //            {
        //                RucEmpleador = data.ruc,
        //                RazonSocial = data.nomCia,
        //                CodigoEmpleado = codEmpleado,
        //                Cedula = data.cedula,
        //                Year = year,
        //                FechaEntrega = DateTime.Now.Date,
        //                NombresApellidos = data.nombre,
        //                classReadOnly = notEnabled ? "" : "@readonly=true",
        //                Estado = "1",
        //                endPeriod = "final"

        //            };

        //            return View(model);



        //        }
        //        else
        //        {
        //            return RedirectToAction("ErrorGastosPersonales", "Error");
        //        }

        //    }

        //}


        //// [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult GastosPersonalesIndex(GastosPersonalesModel request)
        //{
        //    //GastosPersonalesResponse1 val1 = new GastosPersonalesResponse1()
        //    //{
        //    //    fraccionBasica = 11310.00M,
        //    //    topeGp = 3675.75M
        //    //};  // Cargar desde base

        //    request.FechaEntrega = DateTime.Now.Date;

        //    var val1 = dbaa.GastosPersonalesResponse1();

        //    //Obtener info para validacion 2
        //    var data = dbaa.GastosPersonalesResponse2(this.GetCodigoEmpleado());


        //    //Validaciones Iniciales
        //    decimal validateR = val1.fraccionBasica * 1.3M;

        //    if (request.C_111 > val1.topeGp)
        //        ModelState.AddModelError("C_111", "No puede ser mayor a " + val1.topeGp.ToString());
        //    if (request.C_110 > val1.topeGp)
        //        ModelState.AddModelError("C_110", "No puede ser mayor a " + val1.topeGp.ToString());
        //    if (request.C_109 > val1.topeGp)
        //        ModelState.AddModelError("C_109", "No puede ser mayor a " + val1.topeGp.ToString());
        //    if (request.C_107 > val1.topeGp)
        //        ModelState.AddModelError("C_107", "No puede ser mayor a " + val1.topeGp.ToString());
        //    if (request.C_106 > val1.topeGp)
        //        ModelState.AddModelError("C_106", "No puede ser mayor a " + val1.topeGp.ToString());


        //    decimal sumR = request.C_106 + request.C_107 + request.C_108 + request.C_109 + request.C_110 + request.C_111;

        //    if (sumR > validateR)
        //        ModelState.AddModelError("C_112", "No puede ser mayor a " + validateR.ToString());

        //    if (sumR > data.topAnual)
        //        ModelState.AddModelError("C_112", "No puede ser mayor a " + data.topAnual.ToString());




        //    if (ModelState.IsValid)
        //    {

        //        int codEmpleado = int.Parse(this.GetCodigoEmpleado());
        //        int getPeriodo = 1; // asignar segun corresponda
        //        bool isEnabled = true; // asignar segun corresponda
        //        int year = DateTime.Now.Year;
        //        request.FechaEntrega = DateTime.Now.Date;


        //        //si no existe lo grabamos
        //        GastosPersonales gastos = new GastosPersonales()
        //        {
        //            IdUser = this.GetIdUser(), // revisar traer valor adecuado
        //            Cedula = request.Cedula,
        //            CodigoEmpleado = codEmpleado,
        //            Secuencia = 2,
        //            RazonSocial = request.RazonSocial,
        //            Estado = "1",
        //            Year = year,
        //            FechaCreacion = DateTime.Now,
        //            FechaEntrega = DateTime.Now.Date,
        //            NombresApellidos = request.NombresApellidos,
        //            RucEmpleador = request.RucEmpleador,
        //            UsuarioCreacion = this.GetIdUser(),  //revisar traer el valor adecuado
        //            C_103 = request.C_103,
        //            C_104 = request.C_104,
        //            C_105 = request.C_105,
        //            C_106 = request.C_106,
        //            C_107 = request.C_107,
        //            C_108 = request.C_108,
        //            C_109 = request.C_109,
        //            C_110 = request.C_110,
        //            C_111 = request.C_111,
        //            C_112 = request.C_112

        //        };
        //        db.GastosPersonales.Add(gastos);
        //        db.SaveChanges();
        //        return RedirectToAction("Index", "Home");




        //    }


        //    return View(request);
        //}

        //#endregion

        public ActionResult DescargarDeclaracionGastos()
        {

            int codEmpleado = int.Parse(this.GetCodigoEmpleado());
            int year = DateTime.Now.Year;


            //1.- Validar periodo segun corresponda
            int valPeriodo = 1; // asignar segun corresponda

            var line = db.GastosPersonales.FirstOrDefault(a => a.CodigoEmpleado == codEmpleado && a.Year == year && a.Secuencia == valPeriodo);
            if (line != null)
            {
                
                string directory = AppDomain.CurrentDomain.BaseDirectory;
                FileInfo file = new FileInfo(directory + "Content\\files\\declaraciongastos\\declaraciongastos2021.xlsx");

                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {

                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                 


                    excelWorksheet.Cells[8, 23].Value = "Guayaquil";
                    excelWorksheet.Cells[8, 27].Value = year.ToString().Substring(0,1);
                    excelWorksheet.Cells[8, 28].Value = year.ToString().Substring(1, 1);
                    excelWorksheet.Cells[8, 29].Value = year.ToString().Substring(2, 1);
                    excelWorksheet.Cells[8, 30].Value = year.ToString().Substring(3, 1);
                

                    if(line.FechaEntrega.Month > 9)
                    {

                        //mes
                        excelWorksheet.Cells[8, 31].Value = line.FechaEntrega.Month.ToString().Substring(0,1); 
                        excelWorksheet.Cells[8, 32].Value = line.FechaEntrega.Month.ToString().Substring(1, 1);

                       
                    }
                    else
                    {
                        //mes
                        excelWorksheet.Cells[8, 31].Value = "0";
                        excelWorksheet.Cells[8, 32].Value = line.FechaEntrega.Month.ToString();

                        
                    }

                    if (line.FechaEntrega.Day > 9)
                    {                       

                        //dia
                        excelWorksheet.Cells[8, 33].Value = line.FechaEntrega.Day.ToString().Substring(0, 1);
                        excelWorksheet.Cells[8, 34].Value = line.FechaEntrega.Day.ToString().Substring(1, 1);
                    }
                    else
                    {                     

                        //dia
                        excelWorksheet.Cells[8, 33].Value = "0";
                        excelWorksheet.Cells[8, 34].Value = line.FechaEntrega.Day.ToString();
                    }

                    
                    excelWorksheet.Cells[12, 3].Value = line.Cedula;
                    excelWorksheet.Cells[12, 17].Value = line.NombresApellidos;


                    excelWorksheet.Cells[14, 25].Value = line.C_103;
                    excelWorksheet.Cells[15, 25].Value = line.C_104;
                    excelWorksheet.Cells[16, 25].Value = line.C_105;

                    excelWorksheet.Cells[18, 25].Value = line.C_106;
                    excelWorksheet.Cells[19, 25].Value = line.C_107;
                    excelWorksheet.Cells[20, 25].Value = line.C_108;
                    excelWorksheet.Cells[21, 25].Value = line.C_109;
                    excelWorksheet.Cells[22, 25].Value = line.C_110;
                    excelWorksheet.Cells[23, 25].Value = line.C_111;
                    excelWorksheet.Cells[24, 25].Value = line.C_112;
                    excelWorksheet.Cells[25, 25].Value = line.C_113;


                    excelWorksheet.Cells[38, 3].Value = line.RucEmpleador.Substring(0, 1);
                    excelWorksheet.Cells[38, 4].Value = line.RucEmpleador.Substring(1, 1);
                    excelWorksheet.Cells[38, 5].Value = line.RucEmpleador.Substring(2, 1);
                    excelWorksheet.Cells[38, 6].Value = line.RucEmpleador.Substring(3, 1);
                    excelWorksheet.Cells[38, 7].Value = line.RucEmpleador.Substring(4, 1);
                    excelWorksheet.Cells[38, 8].Value = line.RucEmpleador.Substring(5, 1);
                    excelWorksheet.Cells[38, 9].Value = line.RucEmpleador.Substring(6, 1);
                    excelWorksheet.Cells[38, 10].Value = line.RucEmpleador.Substring(7, 1);
                    excelWorksheet.Cells[38, 11].Value = line.RucEmpleador.Substring(8, 1);
                    excelWorksheet.Cells[38, 12].Value = line.RucEmpleador.Substring(9, 1);
                    excelWorksheet.Cells[38, 13].Value = line.RucEmpleador.Substring(10, 1);
                    excelWorksheet.Cells[38, 14].Value = line.RucEmpleador.Substring(11, 1);
                    excelWorksheet.Cells[38, 15].Value = line.RucEmpleador.Substring(12, 1);

                    excelWorksheet.Cells[38, 17].Value = line.RazonSocial;
                    excelWorksheet.Protection.IsProtected = true;
                    
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(excelPackage.GetAsByteArray(), contentType, "Declaracion_Gastos" + ".xlsx"); //Name File


                }



            }




            return View();
        }


        #endregion

        #region Facturacion Peru
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolFactPeru)]
        public ActionResult DocumentoDeBaja()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolFactPeru)]
        public async Task<ActionResult> DocumentoDeBaja(DocumentoBajaModel model)
        {

            if (ModelState.IsValid)
            {

                if (model.documentFile.ContentType != "text/plain")
                {
                    ModelState.AddModelError("documentFile", "Tipo de archivo incorrecto");
                    return View(model);
                }


                string nameFileBaja = "";

                try
                {

                    // Read bytes from http input stream
                    BinaryReader b = new BinaryReader(model.documentFile.InputStream);
                    byte[] binData = b.ReadBytes(model.documentFile.ContentLength);
                    string resulttxt = System.Text.Encoding.UTF8.GetString(binData);

                    //tipoDOc
                    string[] IdeN = model.documentFile.FileName.Split('-');
                    string tipoDoc = IdeN[1].TrimEnd();
                    string tipoDocAnu = "";
                    //Asignar el tipo de anulacion que correspone
                    tipoDocAnu = tipoDoc.Equals("01") || tipoDoc.Equals("07") ? "RA" : tipoDoc.Equals("20") ? "RR" : "";

                    if (tipoDocAnu == "")
                    {
                        ModelState.AddModelError("", "Tipo de documento para anular incorrecto.");
                        return View(model);
                    }


                    //Info TXT               
                    string[] infoAnuDoc = resulttxt.Split(new string[] { "\r\n" }, StringSplitOptions.None);


                    //Line EDI              
                    string[] infoAnuDocEDI = infoAnuDoc[0].Split('|');
                    int secuencia = 0;
                    string feff = DateTime.Now.ToString("yyyyMMdd");
                    var secSave = db.General_Data.First(f => f.Id == 1);
                    if (secSave.Name.TrimEnd().Equals(feff))
                    {
                        secuencia = int.Parse(secSave.Value) + 1;
                        secSave.Value = secuencia.ToString();
                    }
                    else
                    {
                        secuencia = 1;
                        secSave.Name = feff;
                        secSave.Value = secuencia.ToString();
                    }


                    string IDE_numeracion = tipoDocAnu + "-" + feff + "-" + secuencia.ToString();
                    string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    String IDE = string.Format("IDE|{0}|{1}", IDE_numeracion, fecha);


                    //Line EMI     
                    string[] infoAnuDocEMI = infoAnuDoc[1].Split('|');
                    string tipoDocId = infoAnuDocEMI[1];
                    string numeroDocId = infoAnuDocEMI[2];
                    string razonSocial = infoAnuDocEMI[3];
                    string direccion = infoAnuDocEMI[6];
                    String EMI = string.Format("EMI|{0}|{1}||{2}||{3}|||||||", tipoDocId, numeroDocId, razonSocial, direccion);

                    //line CBR
                    string fechaReferencia = infoAnuDocEDI[2].TrimEnd();
                    String CBR = string.Format("CBR|{0}", fechaReferencia);

                    //Line DBR              
                    string[] infoSerieNum = infoAnuDocEDI[1].Split('-');
                    string serieItem = infoSerieNum[0].TrimEnd();
                    string correlativoItem = infoSerieNum[1].TrimEnd();
                    String DBR = string.Format("DBR|1|{0}|{1}|{2}|{3}", tipoDoc, serieItem, correlativoItem, model.motivo);


                    //Write lines in file
                    nameFileBaja = string.Format("{0}-{1}", numeroDocId, IDE_numeracion) + ".txt";

                    string rutaFile = ConfigurationManager.AppSettings.Get("FactPeruBaja") + nameFileBaja;
                    StreamWriter sw = new StreamWriter(rutaFile);
                    using (sw)
                    {
                        sw.WriteLine(IDE);
                        sw.WriteLine(EMI);
                        sw.WriteLine(CBR);
                        sw.WriteLine(DBR);
                    }

                }
                catch (Exception ex)
                {
                    //error
                    ModelState.AddModelError("documentFile", "Error en la estructura del archivo cargado");
                    return View(model);
                }



                //leer archivo generado para enviarlo
                string file = Directory.GetFiles(ConfigurationManager.AppSettings["FactPeruBaja"], nameFileBaja, SearchOption.TopDirectoryOnly).First();

                HttpClient client = new HttpClient() { BaseAddress = new Uri(ConfigurationManager.AppSettings["urlWs20"]) };

                RequestWS request = new RequestWS();
                request.customer = new Customer()
                {
                    username = ConfigurationManager.AppSettings["user"],
                    password = ConfigurationManager.AppSettings["password"]

                };
                request.fileName = Path.GetFileName(file);
                request.fileContent = Convert.ToBase64String(System.IO.File.ReadAllBytes(file));


                try
                {

                    client.DefaultRequestHeaders.Clear();
                    string urlv = ConfigurationManager.AppSettings["urlWs20"];
                    string myContent = JsonConvert.SerializeObject(request);
                    var result = await client.PostAsync(urlv, Tools.Tools.GetContentRequest(myContent));


                    if (result.IsSuccessStatusCode)
                    {

                        Response response1 = result.Content.ReadAsAsync<Response>().Result;

                        //Validar si proceso es el correcto
                        if (response1.responseCode.Equals("98"))
                        {

                            db.SaveChanges(); //Actualizamos la secuencia de archivo

                            RequestWS2 request2 = new RequestWS2();
                            request2.user = new User()
                            {
                                username = ConfigurationManager.AppSettings["user"],
                                password = ConfigurationManager.AppSettings["password"]

                            };
                            request2.ticket = response1.ticket.TrimEnd();

                            //Esperamos 10 segundos 
                            System.Threading.Thread.Sleep(10000);

                            client.DefaultRequestHeaders.Clear();
                            string urlv2 = ConfigurationManager.AppSettings["urlWsStatus"];
                            string myContent2 = JsonConvert.SerializeObject(request2);
                            var result2 = await client.PostAsync(urlv2, Tools.Tools.GetContentRequest(myContent2));

                            if (result2.IsSuccessStatusCode)
                            {
                                Response2 response2 = result2.Content.ReadAsAsync<Response2>().Result;

                                if (response2.codigo == 0 && response2.mensaje.Equals("OK") && response2.responseCode.Equals("0"))
                                {
                                    //Movemos el archivo ref a ANULADOS
                                    string filesave = ConfigurationManager.AppSettings["FactPeruRef"] + model.documentFile.FileName;
                                    model.documentFile.SaveAs(filesave);
                                    ViewBag.Message = response2.responseMessage;
                                    return View("DocumentoDeBajaOk");

                                }
                                else
                                {
                                    // ref pasa a error
                                    System.IO.File.Move(file, ConfigurationManager.AppSettings["FactPeruBajaError"] + Path.GetFileName(file));
                                    ModelState.AddModelError("", "2");
                                    ModelState.AddModelError("", "Response Codigo:" + response2.responseCode);
                                    ModelState.AddModelError("", "Status Code:" + response2.statusCode);
                                    ModelState.AddModelError("", "Message:" + response2.responseMessage);
                                    return View(model);


                                }


                            }
                            else
                            {
                                // ref pasa a error
                                System.IO.File.Move(file, ConfigurationManager.AppSettings["FactPeruBajaError"] + Path.GetFileName(file));
                                ModelState.AddModelError("", "1.- WS Devolvio Error");
                                return View(model);
                            }

                        }
                        else
                        {
                            //eliminamos el archivo
                            System.IO.File.Delete(file);
                            ModelState.AddModelError("", "1");
                            ModelState.AddModelError("", "Response Codigo:" + response1.responseCode);
                            ModelState.AddModelError("", "nMessage:" + response1.responseContent);
                            return View(model);
                            //error

                        }

                    }
                    else
                    {
                        //eliminamos el Archivo
                        // ref pasa a error
                        System.IO.File.Delete(file);
                        ModelState.AddModelError("", "2.- WS Devolvio Error");
                        return View(model);
                        //error de comunicacion con el ws


                    }


                }
                catch (Exception ex)
                {
                    //error
                    System.IO.File.Delete(file);
                    ModelState.AddModelError("", ex.Message);
                    return View(model);

                }

            }


            return View(model);
        }

        #endregion

        #region Formulario 107
        public ActionResult Formulario107()
        {
            try
            {
                string codigoEmpleado = this.GetCodigoEmpleado();
                string year = (DateTime.Now.Year - 1).ToString();
                var data = dbaa.GastosPersonalesResponse2(this.GetCodigoEmpleado());
                string cedula = data.cedula;
                string codCia = data.codCia;
                string rutaPdf = ConfigurationManager.AppSettings.Get("Form107_Pdf");
                string rutaFirmaCia = ConfigurationManager.AppSettings.Get("Form107_FirmaCia");
                string rutaFirmaRrhh = ConfigurationManager.AppSettings.Get("Form107_FirmaRrhh");
                string rutaFinalPdf = ConfigurationManager.AppSettings.Get("Form107_Output");

                rutaPdf = rutaPdf + year + "\\" + "001-C-" + cedula;
                rutaFinalPdf = rutaFinalPdf + "\\" + "001-C-" + cedula;
                rutaFirmaCia = rutaFirmaCia + codCia;



                using (Stream inputPdfStream = new FileStream(rutaPdf + ".pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream inputImageStream = new FileStream(rutaFirmaCia + ".jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream inputImageStream2 = new FileStream(rutaFirmaRrhh, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(rutaFinalPdf + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var reader = new PdfReader(inputPdfStream);
                    var stamper = new PdfStamper(reader, outputPdfStream);
                    var pdfContentByte = stamper.GetOverContent(1);

                    Image image = Image.GetInstance(inputImageStream);
                    image.SetAbsolutePosition(380, 122);
                    pdfContentByte.AddImage(image);

                    Image image2 = Image.GetInstance(inputImageStream2);
                    image2.SetAbsolutePosition(35, 107);
                    pdfContentByte.AddImage(image2);

                    stamper.Close();

                }

                string content_type = "application/pdf";
                byte[] fileContents = System.IO.File.ReadAllBytes(rutaFinalPdf + ".pdf");

                System.IO.File.Delete(rutaFinalPdf + ".pdf");

                return new FileContentResult(fileContents, content_type);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorFormulario107", "Error");
            }

        }

        #endregion

        /// <summary>
        /// Obtener Código de empleado del usuario actual autenticado
        /// </summary>
        /// <returns></returns>
        private string GetCodigoEmpleado()
        {
            //Obtener ID Usuario y Codigo de Empleado
            int IdUser = db.AspNetUsers.First(f => f.UserName.Equals(User.Identity.Name)).Id;
            return db.AspNetUserClaims.FirstOrDefault(f => f.UserId == IdUser && f.ClaimType.Equals(Tools.Enum.ClaimCodigoEmpleado)).ClaimValue;
            //return "250656";
        }

        private int GetIdUser()
        {
            return db.AspNetUsers.First(f => f.UserName.Equals(User.Identity.Name)).Id;
        }

    }
}