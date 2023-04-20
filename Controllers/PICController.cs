using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoleEcIntranet.Models ;
using DoleEcIntranet.Data.DataPIC;
using DoleEcIntranet.Data.DataPIC.DataMaestroAdamVW;
using DoleEcIntranet.Models.DBAdam;
using DoleEcIntranet.Models.PicModel;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Configuration;
using HtmlAgilityPack;
using System.IO;
using System.Text;
using DoleEcIntranet.Tools;
using HiQPdf;
using static DoleEcIntranet.Models.Util;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using static iTextSharp.text.pdf.AcroFields;
using System.Collections;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Data.Entity;
using System.Web.Services.Description;
//using iTextSharp.text;
//using iTextImage = iTextSharp.text.Image;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace DoleEcIntranet.Controllers
{
    public class PICController : Controller
    {
        DataPic db = new DataPic();
        DataAdamMaestro dbAdam = new DataAdamMaestro();
        // GET: PIC
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReporteGastos()
        {
            List<SelectListItem> motivoAnticipo = new List<SelectListItem>();

            motivoAnticipo.Add(new SelectListItem() { Value = "1", Text = "Capacitación" });
            motivoAnticipo.Add(new SelectListItem() { Value = "2", Text = "Eventos / Ferias" });
            motivoAnticipo.Add(new SelectListItem() { Value = "3", Text = "Otros" });
            motivoAnticipo.Add(new SelectListItem() { Value = "4", Text = "Reuniones de trabajo" });
            motivoAnticipo.Add(new SelectListItem() { Value = "5", Text = "Trabajos en otras cias.DOLE" });
            motivoAnticipo.Add(new SelectListItem() { Value = "6", Text = "Tramites Visa (trabajo)" });

            ViewBag.usuarioLog = User.Identity.Name;
            var user = User.Identity.Name;
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_Trans.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();



            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");

            var lstFaes = db.Cab_transFae.Where(s => s.UsuarioCreacion == user && s.Estado == "A").ToList();

            List<SelectListItem> lstFae = lstFaes.Select(s => new SelectListItem
            {
                Value = s.Secuencial.ToString(),
                Text = "Secuencial:[FAE-0000"+s.Secuencial.ToString()+"] -" + motivoAnticipo.FirstOrDefault(x => x.Value == s.motivoAnticipo).Text

            }).ToList();
     
            ViewBag.SlctLiquidacionFae = new SelectList(lstFae, "Value", "Text") ;

            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-"});
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-"});
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            //var lstaMonedas = db.Sharepoint_Monedas.ToList();
            //ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(    s => s.Moneda), "Id", "Codigo", 0);

            var lstaMonedas = new List<Sharepoint_Monedas>();
            lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Descripcion", 53);
            ReporteGastosModel rpg = new ReporteGastosModel();

            rpg.FechaSolicitud = DateTime.Now.ToString("dddd, dd MMMM yyyy. HH:mm ");
            rpg.Estatus = "Inicio";
            rpg.secuencial = (ultimoSecuencial + 1).ToString();
            return View(rpg);
        }
        public ActionResult AnticipoEfectivo()
        {
            ViewBag.usuarioLog = User.Identity.Name;
            var lstfae = db.Cab_transFae.Where(s => s.UsuarioCreacion == User.Identity.Name).ToList();
            var denegado = "";
            foreach(var a in lstfae)
            {
                if (a.Estado.Trim() == "P" || a.Estado.Trim() == "A" || a.Estado.Trim() == "R"  ) {
                    denegado = "1";
                }
            }
            ViewBag.Denegado = denegado;
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_transFae.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();
            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-" });
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            var lstPaises = db.Paises.ToList();
            lstPaises.Add(new Paises { Id = 0, Nombre = "-Seleccione-" });
            ViewBag.SlPais = new SelectList(lstPaises.OrderBy(o => o.Nombre), "Id", "Nombre", 0);
            //ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);
            AnticipoEfectivoModel fae = new AnticipoEfectivoModel();
            fae.FechaSolicitud = DateTime.Now.ToString("yyyy, MM ddd");
            fae.Estatus = "Inicio";
            fae.Secuencial = (ultimoSecuencial + 1).ToString();
            return View(fae);
        }

        [HttpGet]
        public JsonResult RefreshCiudades(int Id)
        {
            List<SelectListItem> lst = new List<SelectListItem>();

          
                lst = db.Ciudades.Where(s => s.IdPais == Id).Select(s => new SelectListItem
                {
                    Text = s.Nombre ,
                    Value = s.Id.ToString(),

                }).ToList();
                lst.Add(new SelectListItem { Text = "- Seleccione -", Value = "" });

            
            return Json(lst.OrderBy(s => s.Value), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidacionFecha(string fecha)
        {
            var mensaje = "";
            var FechaIngresada = DateTime.Parse(fecha);
            var FechaActual = DateTime.Now.AddMonths(-3);
            if (FechaIngresada > FechaActual)
            {
                mensaje = "Positivo";
            }
            else
            {
                mensaje = "Negativo";
            }

            return Json(mensaje, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public PartialViewResult _DetalleConsumo(ConsumoViewModel obj)
        {
            List<OpcionesModel> ListaMonedas = new List<OpcionesModel>();
            ListaMonedas.Add(new OpcionesModel() { Id = 1, Desc = "Dolar" });
            ListaMonedas.Add(new OpcionesModel() { Id = 2, Desc = "Euro" });
            ListaMonedas.Add(new OpcionesModel() { Id = 3, Desc = "Sol Peruano" });
            ListaMonedas.Add(new OpcionesModel() { Id = 4, Desc = "Peso Colombiano" });
            ListaMonedas.Add(new OpcionesModel() { Id = 5, Desc = "Otros" });

            var extension = "";
            ConsumoViewModel lstP = new ConsumoViewModel();
            if (obj.namefile != null)
            {
                 extension = obj.namefile.Split('.')[1];

            }
       

            //lstP.file = extension;
            lstP.descripcionGastos = obj.descripcionGastos == null ? "N/D" : obj.descripcionGastos;
            lstP.fechafactura = obj.fechafactura == null ? "N/D" : DateTime.Parse(obj.fechafactura).ToString("dd - MMMM - yyyy");
            lstP.invitados = obj.invitados == null ? "N/D" : obj.invitados;
            lstP.idmoneda = "1"/*ListaMonedas.FirstOrDefault(s => s.Id == int.Parse(obj.idmoneda)).Desc*/;
            lstP.monto = obj.monto;
            lstP.inputfile = obj.inputfile;
            lstP.numerofactura = obj.numerofactura == null ? "N/D" : obj.numerofactura;
            lstP.rucproveedor = obj.rucproveedor == null ? "N/D" : obj.rucproveedor;
            lstP.tipoconsumo = obj.tipoconsumo == null ? "N/D" : obj.tipoconsumo;
            lstP.tipofactura = obj.tipofactura == null ? "N/D" : obj.tipofactura;
            lstP.tipoproveedor = obj.idtipoproveedor == "1" ? "Nacional" : "Exterior";
            lstP.razonsocial = obj.razonsocial == null ? "N/D" : obj.razonsocial;
            lstP.tcambio = decimal.Parse(obj.tcambio).ToString("N2");
            lstP.valor = decimal.Parse(obj.valor).ToString("N2");
            lstP.AtencionNegocios = obj.AtencionNegocios == "1" ? "SI":"NO";
            //if (obj.idDetFile.HasValue)
            //{
            //    if (db.Sharepoint_File.Any(s => s.IdDet == obj.idDetFile))
            //    {
            //        var ae = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == obj.idDetFile).Archive;
            //        lstP.Archive = Convert.ToBase64String(ae.ToArray());
            //        lstP.idDetFile = obj.idDetFile;
            //    }
            //}
            
            return PartialView("_DetalleConsumo", lstP);
        }

        [HttpPost]
        public PartialViewResult _DetalleInvitados(List<InvitadosModel> obj)
        {

            List<InvitadosModel> invitados = new List<InvitadosModel>();

            var a = 1;
            if(obj != null)
            {

                foreach (var item in obj)
                {
                    InvitadosModel invitado = new InvitadosModel();
                    invitado.id = a;
                    invitado.nombre = item.nombre;
                    invitado.cargo = item.cargo;
                    invitado.empresa = item.empresa;
                    invitados.Add(invitado);
                    a = a + 1;

                }

            }



            return PartialView("_DetalleInvitados", invitados);
        }

        public JsonResult CargaEmpleado(string codigo)
        {

            EmpleadoModel ep = GetEmpleadoModel(codigo);

            return Json(ep, JsonRequestBehavior.DenyGet);
        }



        public JsonResult CargarRuc(string ruc)
        {
            ProveedorModel pro = new ProveedorModel();


            var proveedor = db.Proveedores.Where(s => s.Ruc.Equals(ruc));
            if (proveedor.Any())
            {

                pro.razonsocialModal = proveedor.FirstOrDefault().RazonSocial;
                pro.TipoProveedor1 = proveedor.FirstOrDefault().Tipo.ToString() ;
            }




            return Json(pro, JsonRequestBehavior.DenyGet);
        }


        public ViewResult ListGerentes()
        {
            var ListaMS = db.Maestro_SharePoint.ToList();

            List<AutoridadModel> lstGer = new List<AutoridadModel>();
            var ListCodGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            lstGer = ListaMS.Where(x => !ListCodGerentes.Select(c => c.CodTrabajador).Contains(x.trabajador)).Select(s => new AutoridadModel
            {

                AreaDesc = s.area_resp_des.Trim(),
                Cargo = s.nombre_puesto.Trim(),
                Centr_costo = s.centro_costo_des.Trim(),
                CodigoCia = s.compania.Trim(),
                CodTrabajador = s.trabajador.Trim(),
                NombreTrab = s.nombre_trab.Trim(),
                Depto_Desc = s.depto_des.Trim(),
                NombreCia = s.nombre_cia.Trim(),
                NombrePuesto = s.nombre_puesto.Trim()
            }).ToList();

            lstGer.Add(new AutoridadModel { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.Lista = new SelectList(lstGer.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", 0);
            List<AutoridadModel> LstGerente = new List<AutoridadModel>();


            LstGerente = ListCodGerentes.Select(s => new AutoridadModel
            {
                AreaDesc = s.AreaDesc,
                Cargo = s.Cargo,
                Centr_costo = s.Centr_costo,
                CodigoCia = s.CodigoCia,
                CodTrabajador = s.CodTrabajador,
                NombreTrab = s.NombreTrab,
                Depto_Desc = s.Depto_Desc,
                NombreCia = s.NombreCia,
                NombrePuesto = s.NombrePuesto

            }).ToList();
            return View(LstGerente);
        }

        public ViewResult ListAgenciaViajes()
        {
            List<string> lstAgen = new List<string>();
           lstAgen = db.AgenciaViajes.Select(s => s.Nombre).ToList();
            ViewBag.listaAgencia = lstAgen;
            return View();
        }

        public ActionResult CrearAgencia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearAgencia(List<string> values, string nameAgencia)
        {

            var age = new  AgenciaViajes();
            age.Nombre = nameAgencia;
            age.FechaCreacion = DateTime.Now;
            age.Usuario = "LcriollC";
            db.AgenciaViajes.Add(age);
            db.SaveChanges();
            List<AgenciaViajes_Det> lstAge = new List<AgenciaViajes_Det>();
            foreach(var correo in values)
            {
                AgenciaViajes_Det age1 = new AgenciaViajes_Det();
                age1.IdAgenciaViajes = age.Id;
                age1.Correo = correo;
                lstAge.Add(age1);

            }
            db.AgenciaViajes_Det.AddRange(lstAge);
            db.SaveChanges();
            return Json(new { success = true, message = "Datos recibidos" });
        }




            public ViewResult ListDirector()
        {

            var ListaMS = db.Maestro_SharePoint.ToList();
            List<AutoridadModel> lstDir = new List<AutoridadModel>();
            var LstCodDirectores = db.DirectorAreas.Where(s => s.Estado == "1").ToList();
            lstDir = ListaMS.Where(x => !LstCodDirectores.Select(d => d.CodTrabajador).Contains(x.trabajador)).Select(s => new AutoridadModel
            {

                AreaDesc = s.area_resp_des.Trim(),
                Cargo = s.nombre_puesto.Trim(),
                Centr_costo = s.centro_costo_des.Trim(),
                CodigoCia = s.compania.Trim(),
                CodTrabajador = s.trabajador.Trim(),
                NombreTrab = s.nombre_trab.Trim(),
                Depto_Desc = s.depto_des.Trim(),
                NombreCia = s.nombre_cia.Trim(),
                NombrePuesto = s.nombre_puesto.Trim()
            }).ToList();

            lstDir.Add(new AutoridadModel { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.Lista = new SelectList(lstDir.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", 0);
            List<AutoridadModel> LstDirector = new List<AutoridadModel>();


            LstDirector = LstCodDirectores.Where(s => s.Estado == "1").Select(s => new AutoridadModel
            {
                AreaDesc = s.AreaDesc,
                Cargo = s.Cargo,
                Centr_costo = s.Centr_costo,
                CodigoCia = s.CodigoCia,
                CodTrabajador = s.CodTrabajador,
                NombreTrab = s.NombreTrab,
                Depto_Desc = s.Depto_Desc,
                NombreCia = s.NombreCia,
                NombrePuesto = s.NombrePuesto

            }).ToList();
            return View(LstDirector);
        }

        [HttpPost]
        public JsonResult AddDirector(List<string> codigo)
        {
            var mensaje = "";
            var ListDirector = db.DirectorAreas.ToList();
            List<string> codd = new List<string>();

            codd = codigo;
            // quiero saber si algun seleccionado esta en Gerentes para habilitarlo y eliminar ese codigo
            for (var i = 0; i < codd.Count(); i++)
            {
                var Direct = ListDirector.Where(x => x.CodTrabajador == codd[i]).ToList();
                if (Direct.Any())
                {

                    var Director = ListDirector.FirstOrDefault(s => s.CodTrabajador == codd[i]);

                    Director.Estado = "1";
                    Director.FechaActualizacion = DateTime.Now;
                    db.SaveChanges();
                    codigo.Remove(codigo[i]);



                }



            }


            var listaAdd = db.Maestro_SharePoint.Where(s => codigo.Contains(s.trabajador)).ToList();

            var obj = listaAdd.Select(s => new DirectorAreas
            {
                AreaDesc = s.area_resp_des.Trim(),
                Cargo = s.nombre_puesto.Trim(),
                Centr_costo = s.centr_costo.Trim() + " - " + s.centro_costo_des.Trim(),
                CodigoCia = s.compania.Trim(),
                CodTrabajador = s.trabajador.Trim(),
                NombreTrab = s.nombre_trab.Trim(),
                Depto_Desc = s.depto_des.Trim(),
                NombreCia = s.nombre_cia.Trim(),
                NombrePuesto = s.nombre_puesto.Trim(),
                AreaResp = s.area_resp.Trim(),
                Estado = "1"


            }).ToList();

            db.DirectorAreas.AddRange(obj);
            db.SaveChanges();
            return Json(mensaje, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult EliminarDirector(string cod)
        {

            var objDirector = db.DirectorAreas.FirstOrDefault(s => s.NombreTrab == cod);
            objDirector.Estado = "0";
            objDirector.FechaActualizacion = DateTime.Now;
            objDirector.UsuarioModificacion = System.Web.HttpContext.Current.User.Identity.Name;
            db.SaveChanges();

            return Json(JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult AddGerente(List<string> codigo)
        {
            var mensaje = "";
            var ListGerentes = db.GerentesAreas.ToList();
            List<string> codd = new List<string>();

            codd = codigo;
            // quiero saber si algun seleccionado esta en Gerentes para habilitarlo y eliminar ese codigo
            for (var i = 0; i < codd.Count(); i++)
            {
                var Gerent = ListGerentes.Where(x => x.CodTrabajador == codd[i]).ToList();
                if (Gerent.Any())
                {

                    var Gerente = ListGerentes.FirstOrDefault(s => s.CodTrabajador == codd[i]);
                    Gerente.Estado = "1";
                    Gerente.FechaActualizacion = DateTime.Now;
                    db.SaveChanges();
                    codigo.Remove(codigo[i]);



                }



            }


            var listaAdd = db.Maestro_SharePoint.Where(s => codigo.Contains(s.trabajador)).ToList();
            var obj = listaAdd.Select(s => new GerentesAreas
            {
                AreaDesc = s.area_resp_des.Trim(),
                Cargo = s.nombre_puesto.Trim(),
                Centr_costo = s.centr_costo.Trim() + " - " + s.centro_costo_des.Trim(),
                CodigoCia = s.compania.Trim(),
                CodTrabajador = s.trabajador.Trim(),
                NombreTrab = s.nombre_trab.Trim(),
                Depto_Desc = s.depto_des.Trim(),
                NombreCia = s.nombre_cia.Trim(),
                NombrePuesto = s.nombre_puesto.Trim(),
                AreaResp = s.area_resp.Trim(),
                Estado = "1"


            }).ToList();

            db.GerentesAreas.AddRange(obj);
            db.SaveChanges();
            return Json(mensaje, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult EliminarGerente(string cod)
        {

            var objGerente = db.GerentesAreas.FirstOrDefault(s => s.NombreTrab == cod);
            objGerente.Estado = "0";
            objGerente.FechaActualizacion = DateTime.Now;
            objGerente.UsuarioModificacion = System.Web.HttpContext.Current.User.Identity.Name;
            db.SaveChanges();

            return Json(JsonRequestBehavior.DenyGet);
        }

        public JsonResult Refresh(int Id)
        {
            List<SelectListItem> lst = new List<SelectListItem>();


            lst = db.DirectorAreas.Select(s => new SelectListItem
            {
                Text = s.NombreTrab,
                Value = s.CodTrabajador.ToString(),



            }).ToList();
            lst.Add(new SelectListItem { Text = "- Seleccione -", Value = "" });


            return Json(lst.OrderBy(s => s.Value), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public PartialViewResult _CreateProveedor(string numero)
        {
            List<SelectListItem> TipoProveedor1 = new List<SelectListItem>();
            TipoProveedor1.Add(new SelectListItem() { Value = "0", Text = "Nacional" });
            TipoProveedor1.Add(new SelectListItem() { Value = "1", Text = "Exterior" });
            ViewBag.TipoProveedor1 = TipoProveedor1;
            ProveedorModel pro = new ProveedorModel();
            pro.rucModal = numero;

            return PartialView("_CreateProveedorGrabar", pro);
        }

        [HttpPost]
        public PartialViewResult _AddComentario()
        {


            return PartialView("_AddComentario");
        }

        [HttpPost]
        public JsonResult CreateProveedorGrabar(ProveedorModel obj1)
        {
            var a = "";
            try
            {
                Proveedores proveedor = new Proveedores();
                proveedor.RazonSocial = obj1.razonsocialModal;
                proveedor.Ruc = obj1.rucModal;
                proveedor.Tipo = int.Parse(obj1.TipoProveedor1);

                db.Proveedores.Add(proveedor);
                db.SaveChanges();
                a = "ok";
            }
            catch
            {
                a = "error";
            }







            return Json(a, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult _EnviarFlujo(Sharepoint_Flujos obj2)
        {
            var mensaje = "";




            return Json(mensaje, JsonRequestBehavior.DenyGet);
        }

        //[HttpPost]
        //public JsonResult _AlcanceDetalle(FaeViewModel obj)
        //{
        //    var mensaje = "";
        //    var usuarioLog = User.Identity.Name;
        //    using (var dbContextTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var idCab = 0;
        //            Cab_transFae cab = new Cab_transFae();
        //            //por el secuencial pregunta si existe... si existe elimina toda la transaccion para volverla a guardar


        //            var idCabFae = 0;
        //            cab.codEmpleado = int.Parse(obj.codigoEmpleado);
        //            cab.destino = int.Parse(obj.destino);
        //            cab.motivoAnticipo = obj.motivoAnticipo;
        //            cab.usaTc = int.Parse(obj.usaTc);
        //            cab.nivel1 = obj.nivel1;
        //            cab.Alcance = "1";
        //            cab.UsuarioCreacion = usuarioLog;
        //            cab.nivel2 = obj.nivel2;
        //            cab.nameFile = obj.namefile;
        //            cab.IdAutoridadFinanciera = int.Parse(obj.AutoFinanciera);
        //            cab.pais = int.Parse(obj.pais);
        //            cab.ciudad = int.Parse(obj.ciudad);
        //            cab.Secuencial = int.Parse(obj.secuencial);
        //            cab.nameFile = obj.namefile;
        //            cab.inputFile = obj.inputfile;
        //            cab.fechaCreacion = DateTime.Now;
        //            cab.fechadesde = DateTime.Parse(obj.fechadesde);
        //            cab.fechahasta = DateTime.Parse(obj.fechahasta);
        //            cab.FechaSolicitud = DateTime.Now;
        //            cab.Estado = obj.estado;
        //            cab.Comentarios = obj.Comentario;
        //            db.Cab_transFae.Add(cab);
        //            db.SaveChanges();
        //            idCabFae = cab.Id;




        //            foreach (var det in obj.DetallGasto)
        //            {
        //                if (det != null)
        //                {
        //                    var idDetFae = 0;
        //                    Det_TransFae dete = new Det_TransFae();
        //                    dete.IdCab_TransFae = idCabFae;
        //                    dete.Idtipoconsumo = int.Parse(det.idtipoconsumo);
        //                    dete.total = decimal.Parse(det.total);
        //                    dete.valor = decimal.Parse(det.valor);
        //                    dete.cantInvitados = int.Parse(det.cantInvitados);
        //                    dete.descripcionGastos = det.descripcionGastos;
        //                    db.Det_TransFae.Add(dete);
        //                    db.SaveChanges();
        //                    idDetFae = dete.Id;
        //                    List<Fae_Asistentes> Listasisten = new List<Fae_Asistentes>();
        //                    if (det.ArrayAsistente != null)
        //                    {
        //                        foreach (var asis in det.ArrayAsistente)
        //                        {
        //                            if (asis.nombre != null)
        //                            {
        //                                Fae_Asistentes asisten = new Fae_Asistentes();
        //                                asisten.Nombre = asis.nombre;
        //                                asisten.Id_Det_TransFae = idDetFae;
        //                                Listasisten.Add(asisten);

        //                            }

        //                        }
        //                        if (Listasisten != null)
        //                        {
        //                            db.Fae_Asistentes.AddRange(Listasisten);
        //                            db.SaveChanges();
        //                        }

        //                    }

        //                }



        //            }

        //            List<Flujo> listaflujos = new List<Flujo>();

        //            if (obj.estado.Equals("A") || obj.estado.Equals("R") || obj.estado.Equals("P"))
        //            {

        //                if (!db.Flujo.Any(s => s.IdCab == idCabFae && s.Formulario == "FAE"))
        //                {


        //                    Flujo flu = new Flujo();
        //                    flu.IdCab = idCabFae;
        //                    flu.Nivel = 1;
        //                    flu.Estado = "P";
        //                    flu.Formulario = "FAE";
        //                    flu.Correo_Revisor = obj.nivel1;
        //                    flu.Revisor = obj.AutoFinanciera;
        //                    flu.FechaCreacion = DateTime.Now;
        //                    listaflujos.Add(flu);


        //                    Flujo flu1 = new Flujo();
        //                    flu1.IdCab = idCabFae;
        //                    flu1.Nivel = 2;
        //                    flu1.Estado = "P";
        //                    flu1.Formulario = "FAE";
        //                    flu1.Correo_Revisor = obj.nivel2;
        //                    flu1.Revisor = "233340";
        //                    flu1.FechaCreacion = DateTime.Now;
        //                    listaflujos.Add(flu1);



        //                    db.Flujo.AddRange(listaflujos);
        //                    db.SaveChanges();



        //                }
        //                else
        //                {
        //                    db.Flujo.RemoveRange(db.Flujo.Where(s => s.IdCab == idCabFae && s.Formulario == "FAE").ToList());
        //                    db.SaveChanges();


        //                    Flujo flu = new Flujo();
        //                    flu.IdCab = idCabFae;
        //                    flu.Nivel = 1;
        //                    flu.Formulario = "FAE";
        //                    flu.Estado = "P";
        //                    flu.Correo_Revisor = obj.nivel1;
        //                    flu.Revisor = obj.AutoFinanciera;
        //                    flu.FechaCreacion = DateTime.Now;
        //                    listaflujos.Add(flu);


        //                    Flujo flu1 = new Flujo();
        //                    flu1.IdCab = idCabFae;
        //                    flu1.Nivel = 2;
        //                    flu1.Formulario = "FAE";
        //                    flu1.Estado = "P";
        //                    flu1.Correo_Revisor = "Alegria.Molina@dole.com";
        //                    flu1.Revisor = "233340";
        //                    flu1.FechaCreacion = DateTime.Now;
        //                    listaflujos.Add(flu1);

        //                    db.Flujo.AddRange(listaflujos);
        //                    db.SaveChanges();


        //                }


        //                listaflujos.OrderBy(s => s.Nivel).ToList();
        //                Sharepoint_Flujos flujo = new Sharepoint_Flujos();
        //                flujo.Secuencial = int.Parse(obj.secuencial);
        //                flujo.Estado = obj.estado;
        //                flujo.Formulario = "FAE";
        //                flujo.Nivel = listaflujos[0].Nivel;
        //                flujo.Aprobador = listaflujos[0].Revisor;
        //                flujo.FechaCreacion = DateTime.Now;
        //                flujo.Comentario = obj.nivel1;
        //                db.Sharepoint_Flujos.Add(flujo);



        //                //var cab1 = db.Cab_transFae.FirstOrDefault(s => s.Secuencial == flujo.Secuencial);
        //                //cab1.Estado = obj.estado;
        //                //cab1.nivel1 = obj.nivel1;
        //                //cab1.nivel2 = obj.nivel2;

        //                db.SaveChanges();
        //                mensaje = "ok";
        //                Session["IdCabecera"] = idCabFae;
        //                Session["Formulario"] = "FAE";
        //                dbContextTransaction.Commit();

        //                return Json(mensaje, JsonRequestBehavior.DenyGet);

        //            }

        //            Session["IdCabecera"] = idCabFae;
        //            Session["Formulario"] = "FAE";
        //            dbContextTransaction.Commit();
        //            mensaje = "";

        //            return Json(mensaje, JsonRequestBehavior.DenyGet);
        //        }
        //        catch (Exception ex)
        //        {

        //            dbContextTransaction.Rollback();
        //            mensaje = "Error";

        //            return Json(mensaje, JsonRequestBehavior.DenyGet);

        //        }
        //    }

        //}


        [HttpPost]
        public JsonResult _CreateTransactionFae(FaeViewModel obj, string accion = null, string comentario = null)
        {
            var mensaje = "";
            var usuarioLog = User.Identity.Name;
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var idCab = 0;
                    var SecuenFae = 0;
                    //por el secuencial pregunta si existe... si existe elimina toda la transaccion para volverla a guardar
                    #region EliminarTRansaccion


                    var secuencial = int.Parse(obj.secuencial);
                    Cab_transFae cab = db.Cab_transFae.FirstOrDefault(x => x.Secuencial == secuencial);

                    if (cab == null)
                    {
                        cab = new Cab_transFae();
                        cab.Secuencial = secuencial;
                        db.Cab_transFae.Add(cab);
                    }

                   #endregion


            
                    cab.codEmpleado = int.Parse(obj.codigoEmpleado);
                    cab.destino = int.Parse(obj.destino);
                    cab.motivoAnticipo = obj.motivoAnticipo;
                    cab.usaTc = int.Parse(obj.usaTc);
                    cab.nivel1 = obj.nivel1;
                    cab.UsuarioCreacion = usuarioLog;
                    cab.nivel2 = obj.nivel2;
                    //cab.nameFile = obj.namefile;
                    cab.IdAutoridadFinanciera = int.Parse(obj.AutoFinanciera);
                    cab.pais = int.Parse(obj.pais);
                    cab.ciudad = int.Parse(obj.ciudad);
                    cab.Secuencial = int.Parse(obj.secuencial);
                    //cab.nameFile = obj.namefile;
                    //cab.inputFile = obj.inputfile;
                    cab.fechaCreacion = DateTime.Now;
                    cab.fechadesde = DateTime.Parse(obj.fechadesde);
                    cab.fechahasta = DateTime.Parse(obj.fechahasta);
                    cab.FechaSolicitud = DateTime.Now;
                    cab.Estado = accion;
                    cab.Comentarios = obj.Comentario;
                
                    db.SaveChanges();
                    SecuenFae = cab.Secuencial;
                    idCab = cab.Id;

                    #region LogicaArchivo
                    if (obj.archivoprueba != null && obj.archivoprueba.ContentLength > 0)
                    {

                        var file = new Sharepoint_File();
                        file.IdDet = idCab;
                        file.Formulario = "FAE";
                        file.ContentType = obj.archivoprueba.ContentType;
                        file.Archive = ConvertHttpPostedFileBaseABase64(obj.archivoprueba);
                        file.NameFile = obj.archivoprueba.FileName;
                        file.Extension = "JPG";
                        db.Sharepoint_File.Add(file);
                        //eliminamos el file viejo
                        if (obj.idFile != 0)
                        {
                            db.Sharepoint_File.Remove(db.Sharepoint_File.Find(obj.idFile));
                        }
                        db.SaveChanges();
                    }
                    else
                    {

                        //Consultamos que tenga un id - y actualizamos IdFile
                        if (obj.idFile != 0)
                        {
                            var file = db.Sharepoint_File.FirstOrDefault(x => x.Id == obj.idFile);
                            file.IdDet = idCab;
                            db.SaveChanges();

                        }


                    }
                    #endregion

                    #region LogicaAsistente
                 

                    
                   
                        var ListaAsisEliminar = db.Fae_Asistentes.Where(s => s.Id_Cab_TransFae == idCab).ToList();

                        if (ListaAsisEliminar.Any())
                        {
                            db.Fae_Asistentes.RemoveRange(ListaAsisEliminar);
                            db.SaveChanges();

                        }
                        
                      
                            List<Fae_Asistentes> ListAsist  = obj.ArrayAsistente.Select(x => new Fae_Asistentes
                            {
                                Nombre = x.nombre,
                                Id_Cab_TransFae = idCab

                            }).ToList();

                        db.Fae_Asistentes.AddRange(ListAsist);
                        db.SaveChanges();





                    #endregion


                    List<Det_TransFae> ListDetEliminar = db.Det_TransFae.Where(x => x.IdCab_TransFae == idCab).ToList();

                    if (ListDetEliminar.Any())
                    {

                        db.Det_TransFae.RemoveRange(ListDetEliminar);
                        db.SaveChanges();
                    }

                    List<Det_TransFae> ListDetalle = new List<Det_TransFae>();
                    foreach (var det in obj.DetallGasto)
                    {
                       
                          
                            Det_TransFae dete = new Det_TransFae();
                            dete.IdCab_TransFae = cab.Id;
                            dete.Idtipoconsumo = int.Parse(det.idtipoconsumo);
                            dete.total = decimal.Parse(det.total);
                            dete.valor = decimal.Parse(det.valor);
                            dete.cantInvitados = obj.ArrayAsistente.Count();
                            dete.descripcionGastos = det.descripcionGastos;
                           ListDetalle.Add(dete);
          
                    }
                    db.Det_TransFae.AddRange(ListDetalle);
                    db.SaveChanges();
                    //List<Flujo> listaflujos = new List<Flujo>();

                    //if (obj.estado.Equals("A") || obj.estado.Equals("R") || obj.estado.Equals("P"))
                    //{

                    //    if (!db.Flujo.Any(s => s.Secuencial == SecuenFae && s.Formulario == "FAE"))
                    //    {


                    //        Flujo flu = new Flujo();
                    //        flu.Secuencial = SecuenFae;
                    //        flu.Nivel = 1;
                    //        flu.Estado = "P";
                    //        flu.Formulario = "FAE";
                    //        flu.Correo_Revisor = obj.nivel1;
                    //        flu.Revisor = obj.AutoFinanciera;
                    //        flu.FechaCreacion = DateTime.Now;
                    //        listaflujos.Add(flu);


                    //        Flujo flu1 = new Flujo();
                    //        flu1.Secuencial = SecuenFae;
                    //        flu1.Nivel = 2;
                    //        flu1.Estado = "P";
                    //        flu1.Formulario = "FAE";
                    //        flu1.Correo_Revisor = obj.nivel2;
                    //        flu1.Revisor = "233340";
                    //        flu1.FechaCreacion = DateTime.Now;
                    //        listaflujos.Add(flu1);



                    //        db.Flujo.AddRange(listaflujos);
                    //        db.SaveChanges();



                    //    }
                    //    else
                    //    {
                    //        db.Flujo.RemoveRange(db.Flujo.Where(s => s.Secuencial == SecuenFae && s.Formulario == "FAE").ToList());
                    //        db.SaveChanges();


                    //        Flujo flu = new Flujo();
                    //        flu.Secuencial = SecuenFae;
                    //        flu.Nivel = 1;
                    //        flu.Formulario = "FAE";
                    //        flu.Estado = "P";
                    //        flu.Correo_Revisor = obj.nivel1;
                    //        flu.Revisor = obj.AutoFinanciera;
                    //        flu.FechaCreacion = DateTime.Now;
                    //        listaflujos.Add(flu);


                    //        Flujo flu1 = new Flujo();
                    //        flu1.Secuencial = SecuenFae;
                    //        flu1.Nivel = 2;
                    //        flu1.Formulario = "FAE";
                    //        flu1.Estado = "P";
                    //        flu1.Correo_Revisor = "Alegria.Molina@dole.com";
                    //        flu1.Revisor = "233340";
                    //        flu1.FechaCreacion = DateTime.Now;
                    //        listaflujos.Add(flu1);

                    //        db.Flujo.AddRange(listaflujos);
                    //        db.SaveChanges();


                    //    }


                    //    listaflujos.OrderBy(s => s.Nivel).ToList();
                    //    Sharepoint_Flujos flujo = new Sharepoint_Flujos();
                    //    flujo.Secuencial = int.Parse(obj.secuencial);
                    //    flujo.Estado = obj.estado;
                    //    flujo.Formulario = "FAE";
                    //    flujo.Nivel = listaflujos[0].Nivel;
                    //    flujo.Aprobador = listaflujos[0].Revisor;
                    //    flujo.FechaCreacion = DateTime.Now;
                    //    flujo.Comentario = obj.nivel1;
                    //    db.Sharepoint_Flujos.Add(flujo);



                    //    //var cab1 = db.Cab_transFae.FirstOrDefault(s => s.Secuencial == flujo.Secuencial);
                    //    //cab1.Estado = obj.estado;
                    //    //cab1.nivel1 = obj.nivel1;
                    //    //cab1.nivel2 = obj.nivel2;

                    //    db.SaveChanges();
                    //    mensaje = "ok";
                    //    //Session["IdCabecera"] = idCabFae;
                    //    //Session["Formulario"] = "FAE";
                    //    dbContextTransaction.Commit();

                    //    return Json(mensaje, JsonRequestBehavior.DenyGet);

                    //}

                    //Session["IdCabecera"] = idCabFae;
                    //Session["Formulario"] = "FAE";
                    dbContextTransaction.Commit();
                    mensaje = "ok";

                    return Json(new
                    {
                        Mensaje = mensaje,
                        Data = new { secuencial = obj.secuencial, Form = "FAE" }
                    }, JsonRequestBehavior.DenyGet);
                }
                catch (Exception ex)
                {

                    dbContextTransaction.Rollback();
                    mensaje = "Error";

                 

                }
            }

            return Json(new
            {
                Mensaje = mensaje,
                Data = new { secuencial = obj.secuencial, Form = "FAE" }
            }, JsonRequestBehavior.DenyGet);



        }
        [HttpPost]
        public JsonResult _CreateTransaction(ObjetoViewModel obj, string accion = null, string comentario = null)
        {
            var mensaje = "";
            var idCab = 0;
            var usuarioLog = User.Identity.Name;

            if (obj.motivogasto != null && obj.slctTipo != null && obj.codigoEmpleado != null)
            {

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                            #region LogicaCabecera

                        var secuencial = int.Parse(obj.secuencial);
                        Cab_Trans cab = db.Cab_Trans.FirstOrDefault(x => x.Secuencial == secuencial);

                        if (cab == null)
                        {
                            cab = new Cab_Trans();
                            cab.Secuencial = secuencial;
                            db.Cab_Trans.Add(cab);
                        }

                        cab.Nivel1 = obj.Nivel1;
                        cab.Nivel2 = obj.Nivel2;
                        cab.Nivel3 = obj.Nivel3;
                        cab.MotivoGasto = obj.motivogasto;
                        cab.Secuencial = int.Parse(obj.secuencial);
                        cab.Estado = accion;
                        cab.FechaSolicitud = DateTime.Now;
                        cab.SlTipoConsumo = obj.slctTipo;
                        cab.Codigo = obj.codigoEmpleado;
                        cab.IdAutoridadFinanciera = obj.IdAutoridadFinanciera;
                        cab.IdDirector = obj.IdDirector;
                        cab.CentroCosto = obj.CentroCosto;
                        cab.CodCentroCostoA = obj.CodCentroCostoA;
                        cab.FechaCreacion = DateTime.Now;
                        cab.UsuarioCreacion = usuarioLog;
                        cab.SecuenExterno = obj.secuenExterno;

                        cab.FechaDesde = obj.fechadesde;
                        cab.FechaHasta = obj.fechahasta;
                        db.SaveChanges();
                        idCab = cab.Id;

                        #endregion

                        if (obj.DetallConsumo.Any())
                        {

                            #region LogigaDetalle
                            List<Det_Trans> ListDetEliminar = db.Det_Trans.Where(x => x.Id_Cab == idCab).ToList();

                            if (ListDetEliminar.Any())
                            {

                                db.Det_Trans.RemoveRange(ListDetEliminar);
                                db.SaveChanges();
                            }

                            List<Det_Trans> ListDet = new List<Det_Trans>();
                            foreach (var list in obj.DetallConsumo)
                            {
                                var idDet = 0;
                                Det_Trans det = new Det_Trans();
                                det.Id_Cab = idCab;
                                det.IdTipoConsumo = int.Parse(list.idtipoconsumo);
                                det.IdMoneda = int.Parse(list.idmoneda);
                                det.inputfile = list.inputfile;
                                det.namefile = list.namefile;
                                det.Tcambio = decimal.Parse(list.tcambio);
                                det.Monto = decimal.Parse(list.monto);
                                det.Valor = decimal.Parse(list.valor);
                                det.destinoD = int.Parse(list.idtipoproveedor);
                                det.IdProveedor = int.Parse(list.idtipoproveedor);
                                if (db.Proveedores.Any(s => s.Ruc == list.rucproveedor))
                                {
                                    det.IdProvee = db.Proveedores.FirstOrDefault(s => s.Ruc == list.rucproveedor).Id;

                                }


                                if (!string.IsNullOrEmpty(list.AtencionNegocios))
                                    det.AtencionNegocios = int.Parse(list.AtencionNegocios);
                                if (!string.IsNullOrEmpty(list.rucproveedor))

                                    det.Descripcion = list.descripcionGastos;
                                db.Det_Trans.Add(det);
                                db.SaveChanges();
                                idDet = det.Id;


                                #region LogicaArchivo
                                if (list.archivoprueba != null && list.archivoprueba.ContentLength > 0)
                                {

                                    var file = new Sharepoint_File();
                                    file.IdDet = idDet;
                                    file.Formulario = "RPG";
                                    file.ContentType = list.archivoprueba.ContentType;
                                    file.Archive = ConvertHttpPostedFileBaseABase64(list.archivoprueba);
                                    file.NameFile = list.archivoprueba.FileName;
                                    file.Extension = "JPG";
                                    db.Sharepoint_File.Add(file);
                                    //eliminamos el file viejo
                                    if (list.idFile != 0)
                                    {
                                        db.Sharepoint_File.Remove(db.Sharepoint_File.Find(list.idFile));
                                    }
                                    db.SaveChanges();
                                }
                                else
                                {

                                    //Consultamos que tenga un id - y actualizamos IdFile
                                    if (list.idFile != 0)
                                    {
                                        var file = db.Sharepoint_File.FirstOrDefault(x => x.Id == list.idFile);
                                        file.IdDet = idDet;
                                        db.SaveChanges();

                                    }


                                }
                                #endregion


                                #region Logicafactura
                                var fact = db.Facturas.FirstOrDefault(x => x.Id_Det == idDet);
                                if (fact == null)
                                {
                                    fact = new Facturas();
                                    fact.Id_Det = idDet;
                                    db.Facturas.Add(fact);

                                }

                                fact.NumFactura = list.numerofactura;
                                fact.TipoFactura = list.idtipofactura;
                                fact.FechaFactura = DateTime.Parse(list.fechafactura);

                                db.SaveChanges();

                                #endregion


                                #region LogicaInvitados
                                List<Invitados> lstinvitados = new List<Invitados>();
                                if (list.ArrayInvitados != null)
                                {

                                    List<Invitados> ListInviEliminar = db.Invitados.Where(x => x.Id_Det_Trans == idDet).ToList();
                                    if (ListInviEliminar.Any())
                                    {
                                        db.Invitados.RemoveRange(ListInviEliminar);
                                        db.SaveChanges();
                                    }
                                    foreach (var i in list.ArrayInvitados)
                                    {
                                        Invitados Invitados = new Invitados();
                                        Invitados.Id_Det_Trans = idDet;
                                        Invitados.Nombre = i.Nombre;
                                        Invitados.Cargo = i.Cargo;
                                        Invitados.Empresa = i.Empresa;
                                        lstinvitados.Add(Invitados);

                                    }
                                    db.Invitados.AddRange(lstinvitados);
                                    db.SaveChanges();

                                }

                                #endregion


                            }
                            #endregion

                            //if (accion != null)
                            //{
                            //    if(accion != "G")
                            //    {
                            //        ActualizarFlujos(int.Parse(obj.secuencial), "RPG", accion, comentario);

                            //    }
                               
                            //}
                            dbContextTransaction.Commit();

                        
                            mensaje = "ok";

                        }
                        else
                        {
                            mensaje = "No tiene detalles";
                        }

                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message;
                        dbContextTransaction.Rollback();

                    }
                   
                }
            }
            else
            {
                mensaje = "Llene los campos requeridos";
            }

            return Json(new
            {
                Mensaje = mensaje,
                Data = new { secuencial =obj.secuencial, Form = "RPG" }
            }, JsonRequestBehavior.DenyGet);
        }
        
        private void ActualizarFlujos(int secuencial , string form , string accion, string comentario)
        {         
                try
                {

                    List<Flujo> listaflujos = new List<Flujo>();
                  
                    if (form == "RPG" )
                    {

                    var obj = new Cab_Trans();
                    //1.Consulto si esta creado en Flujos, sino crearla(esto por primera vez)
                    listaflujos = db.Flujo.Where(Flujo => Flujo.Secuencial == secuencial && Flujo.Formulario == form).ToList();
               
                        obj = db.Cab_Trans.FirstOrDefault(x => x.Secuencial == secuencial);
                



                                 #region CrearFlujoSiNoExiste

                    //No existe flujo - CREARLO
                    if (!listaflujos.Any())
                                {
                              

                                // Función para crear un objeto Flujo
                                Flujo CrearFlujo(int nivel, string correoRevisor)
                                    {
                                    var ListaEmpleados = db.Maestro_SharePoint.ToList();

                                    return new Flujo
                                    {
                                        Secuencial = secuencial,
                                        FechaCreacion = DateTime.Now,
                                        Formulario = form,
                                        Estado = "P",
                                        Nivel = nivel,
                                        Revisor = ListaEmpleados.FirstOrDefault(s => s.e_mail != null && s.e_mail.Trim().ToLower() == correoRevisor.Trim().ToLower())?.trabajador,
                                        Correo_Revisor = correoRevisor
                                        };
                                    }

                                    string[] correosRevisores = { obj.Nivel1, obj.Nivel2, obj.Nivel3 };
                                    for (int i = 0; i < correosRevisores.Length; i++)
                                    {
                                        if (correosRevisores[i] != null)
                                        {
                                            Flujo flujo = CrearFlujo(i, correosRevisores[i]);
                                            listaflujos.Add(flujo);
                                        }
                                    }

                                    db.Flujo.AddRange(listaflujos);
                                    db.SaveChanges();


                                }
                                else
                                {
                                    listaflujos = db.Flujo.Where(Flujo => Flujo.Secuencial == secuencial && Flujo.Formulario == form && Flujo.Estado != "A").ToList();
                                }

                                #endregion


                                listaflujos = listaflujos.OrderBy(s => s.Nivel).ToList();

                                #region UpdateTablaFlujo


                                if (listaflujos.Any())
                                {
                                                   
                                 
                                          if(accion == "R" )
                                            {
                                                obj.Estado = accion;
                                                db.Entry(obj).State = EntityState.Modified;

                                            }
                                          if(listaflujos.Count == 1 && accion == "A")
                                            {
                                                obj.Estado = accion;
                                                db.Entry(obj).State = EntityState.Modified;
                                            }
                                            
                                      
                                     //Actualizar el primer pendiente en flujos
                                        Flujo Updateflujo = listaflujos[0];
                                        Updateflujo.Estado = accion;
                                        db.Entry(Updateflujo).State = EntityState.Modified;
                       

                                

                                #region Insertar Accion Sharepoint_Flujo

                                Sharepoint_Flujos SharepointFlujo = new Sharepoint_Flujos();
                                SharepointFlujo.Secuencial = secuencial;
                                SharepointFlujo.FechaCreacion = DateTime.Now;
                                SharepointFlujo.Aprobador = listaflujos[0].Revisor;
                                SharepointFlujo.Nivel = listaflujos[0].Nivel;
                                SharepointFlujo.Formulario = listaflujos[0].Formulario;
                                SharepointFlujo.Estado = accion;
                                SharepointFlujo.Comentario = comentario;
                                db.Sharepoint_Flujos.Add(SharepointFlujo);


                                #endregion

                            }
                            else
                            {
                                obj.Estado = "A";
                                db.Entry(obj).State = EntityState.Modified;

                            }





                    #endregion








                }
                else
                {
                    var obj = new Cab_transFae();

                    //1.Consulto si esta creado en Flujos, sino crearla(esto por primera vez)
                    listaflujos = db.Flujo.Where(Flujo => Flujo.Secuencial == secuencial && Flujo.Formulario == form).ToList();

                    obj = db.Cab_transFae.FirstOrDefault(x => x.Secuencial == secuencial);




                    #region CrearFlujoSiNoExiste

                    //No existe flujo - CREARLO
                    if (!listaflujos.Any())
                    {


                        // Función para crear un objeto Flujo
                        Flujo CrearFlujo(int nivel, string correoRevisor)
                        {
                            var ListaEmpleados = db.Maestro_SharePoint.ToList();

                            return new Flujo
                            {
                                Secuencial = secuencial,
                                FechaCreacion = DateTime.Now,
                                Formulario = form,
                                Estado = "P",
                                Nivel = nivel,
                                Revisor = ListaEmpleados.FirstOrDefault(s => s.e_mail != null && s.e_mail.Trim().ToLower() == correoRevisor.Trim().ToLower())?.trabajador,
                                Correo_Revisor = correoRevisor
                            };
                        }

                        string[] correosRevisores = { obj.nivel1, obj.nivel2};
                        for (int i = 0; i < correosRevisores.Length; i++)
                        {
                            if (correosRevisores[i] != null)
                            {
                                Flujo flujo = CrearFlujo(i, correosRevisores[i]);
                                listaflujos.Add(flujo);
                            }
                        }

                        db.Flujo.AddRange(listaflujos);
                        db.SaveChanges();


                    }
                    else
                    {
                        listaflujos = db.Flujo.Where(Flujo => Flujo.Secuencial == secuencial && Flujo.Formulario == form && Flujo.Estado != "A").ToList();
                    }

                    #endregion


                    listaflujos = listaflujos.OrderBy(s => s.Nivel).ToList();

                    #region UpdateTablaFlujo


                    if (listaflujos.Any())
                    {


                        if (accion == "R")
                        {
                            obj.Estado = accion;
                            db.Entry(obj).State = EntityState.Modified;

                        }
                        if (listaflujos.Count == 1 && accion == "A")
                        {
                            obj.Estado = accion;
                            db.Entry(obj).State = EntityState.Modified;
                        }


                        //Actualizar el primer pendiente en flujos
                        Flujo Updateflujo = listaflujos[0];
                        Updateflujo.Estado = accion;
                        db.Entry(Updateflujo).State = EntityState.Modified;




                        #region Insertar Accion Sharepoint_Flujo

                        Sharepoint_Flujos SharepointFlujo = new Sharepoint_Flujos();
                        SharepointFlujo.Secuencial = secuencial;
                        SharepointFlujo.FechaCreacion = DateTime.Now;
                        SharepointFlujo.Aprobador = listaflujos[0].Revisor;
                        SharepointFlujo.Nivel = listaflujos[0].Nivel;
                        SharepointFlujo.Formulario = listaflujos[0].Formulario;
                        SharepointFlujo.Estado = accion;
                        SharepointFlujo.Comentario = comentario;
                        db.Sharepoint_Flujos.Add(SharepointFlujo);


                        #endregion

                    }
                    else
                    {
                        obj.Estado = "A";
                        db.Entry(obj).State = EntityState.Modified;

                    }





                    #endregion

                }



                db.SaveChanges();

             
                }
                catch (Exception ex)
                {
                throw;

                }
            

       

        }

        
        [HttpPost]
        public ActionResult UploadFilesDet()
        {
            var file = Request.Files;
            var filesssss = Request.Form;

            if(filesssss.Count > 0)
            {
                var tipo = filesssss[1].Split('/')[1];

               
                var idcab = int.Parse(Session["IdCabecera"].ToString());
                var formulario = Session["Formulario"].ToString();

                try
                {
                    if(formulario == "RPG")
                    {
                        var listaDetalles = db.Det_Trans.Where(s => s.Id_Cab == idcab && s.namefile != null).Select(f => new { f.Id, f.namefile }).ToList();

                        List<Sharepoint_File> ListSf = new List<Sharepoint_File>();
                        for (var x = 0; x < file.Count; x++)
                        {

                            var a = file[x];
                            Sharepoint_File sf = new Sharepoint_File();
                            MemoryStream stream = new MemoryStream();
                            if (listaDetalles.Select(s => s.namefile).Contains(a.FileName))
                            {

                                a.InputStream.CopyTo(stream);
                                var e = stream.ToArray();

                                if (tipo == "pdf")
                                {
                                    PdfRasterizer pdfRasterizer = new PdfRasterizer();
                                    var imageBuffer = pdfRasterizer.RasterizeToImageObjects(e, 0, 0);

                                    var jjjj = imageBuffer[0].ImageObject;
                                    var pdfaimagen = GetImageBuffer(jjjj);
                                    sf.Archive = pdfaimagen;
                                }
                                else
                                {
                                    sf.Archive = stream.ToArray();
                                }
                                sf.IdDet = listaDetalles.FirstOrDefault(s => s.namefile.Contains(a.FileName)).Id;
                                sf.ContentType = a.ContentType;
                                sf.NameFile = a.FileName;
                                sf.Formulario = "RPG";
                                //sf.IdCab = int.Parse(idcab.ToString());
                                sf.Extension = a.ContentType.Split('/')[1];
                                ListSf.Add(sf);
                                db.Sharepoint_File.AddRange(ListSf);
                                db.SaveChanges();

                            }

                        }


                    }
                    else
                    {
                        var listaDetalles = db.Cab_transFae.Where(s => s.Id == idcab && s.nameFile != null).Select(f => new { f.Id, f.nameFile }).ToList();

                        List<Sharepoint_File> ListSf = new List<Sharepoint_File>();
                        for (var x = 0; x < file.Count; x++)
                        {

                            var a = file[x];
                            Sharepoint_File sf = new Sharepoint_File();
                            MemoryStream stream = new MemoryStream();
                            if (listaDetalles.Select(s => s.nameFile).Contains(a.FileName))
                            {

                                a.InputStream.CopyTo(stream);
                                var e = stream.ToArray();

                                if (tipo == "pdf")
                                {
                                    PdfRasterizer pdfRasterizer = new PdfRasterizer();
                                    var imageBuffer = pdfRasterizer.RasterizeToImageObjects(e, 0, 0);

                                    var jjjj = imageBuffer[0].ImageObject;
                                    var pdfaimagen = GetImageBuffer(jjjj);
                                    sf.Archive = pdfaimagen;
                                }
                                else
                                {
                                    sf.Archive = stream.ToArray();
                                }
                                sf.IdDet = listaDetalles.FirstOrDefault(s => s.nameFile.Contains(a.FileName)).Id;
                               
                                sf.ContentType = a.ContentType;
                                sf.NameFile = a.FileName;
                                //sf.IdCab = int.Parse(idcab.ToString());
                                sf.Formulario = "FAE";
                                sf.Extension = a.ContentType.Split('/')[1];
                                ListSf.Add(sf);
                                db.Sharepoint_File.AddRange(ListSf);
                                db.SaveChanges();

                            }

                        }

                    }





                }
                catch (Exception ex)
                {

                    throw;
                }


            }



            //string type = Request.Form

            //    //Request.Files(x => x.Key == "type").FirstOrDefault().Value;
            //string id = Request.Files.Where(x => x.Key == "id").FirstOrDefault().Value;

            return Json(JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public ActionResult UploadIMG()
        {
            //var file = Request.Files;
            //var filesssss = Request.Form;
            //var idcab = int.Parse(TempData["IdCabecera"].ToString());
            //if (idcab != null)
            //{
            //    TempData["IdCabeceras"] = idcab;
            //}

            //List<Sharepoint_File> ListSf = new List<Sharepoint_File>();

            //for (var x = 0; x < file.Count; x++)
            //{
            //    var a = file[x];
            //    Sharepoint_File sf = new Sharepoint_File();

            //    MemoryStream stream = new MemoryStream();
            //    a.InputStream.CopyTo(stream);
            //    var e = stream.ToArray();



            //    sf.Archive = stream.ToArray();
            //    sf.ContentType = a.ContentType;
            //    sf.NameFile = a.FileName;
            //    sf.IdCab = int.Parse(idcab.ToString());
            //    sf.Extension = a.ContentType.Split('/')[1];
            //    ListSf.Add(sf);

            //}
            //db.Sharepoint_File.AddRange(ListSf);
            //db.SaveChanges();





            //string type = Request.Form

            //    //Request.Files(x => x.Key == "type").FirstOrDefault().Value;
            //string id = Request.Files.Where(x => x.Key == "id").FirstOrDefault().Value;

            return Json(JsonRequestBehavior.DenyGet);
        }
        public ActionResult Flujos()
        {



            List<ListFlujosModel> listmodelFlujo = new List<ListFlujosModel>();
            //var dataflujos = db.Sharepoint_Flujos.OrderByDescending(s => s.Id).ToList();

            //var list = dataflujos.Select(x => x.Secuencial).ToList();
            //var SecDistintos = list.Distinct().ToList();
            //List<int> listaId = new List<int>();

            //foreach (var i in SecDistintos)
            //{

            //    if(dataflujos.Any(s => s.Secuencial == i && s.Estado.Trim() == "P"))
            //    {
            //        listaId.Add(dataflujos.FirstOrDefault(f => f.Secuencial == i && f.Estado.Trim() == "P").Id);

            //    }
              


            //}
            //var nuevalista = db.Sharepoint_Flujos.Where(g => listaId.Contains(g.Id)).ToList();


            //foreach (var i in nuevalista)
            //{

            //    ListFlujosModel modelFlujo = new ListFlujosModel();
            //    var ListaEstados = db.Estados_Flujos.ToList();
            //    var cab = db.Cab_Trans.FirstOrDefault(s => s.Secuencial == i.Secuencial);
            //    var datemp = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == cab.Codigo);
            //    modelFlujo.Id = cab.Id;
            //    if (cab.SlTipoConsumo.Trim() == "3")
            //    {
            //        modelFlujo.secuencial = "RPG-0000" + cab.Secuencial;

            //    }
            //    else
            //    {
            //        modelFlujo.secuencial = "00000" + cab.Secuencial;
            //    }

            //    //modelFlujo.area = datemp.depto_des;
            //    modelFlujo.cia = datemp.nombre_cia.Trim();
            //    modelFlujo.codigoEmpleado = cab.Codigo;
            //    //modelFlujo.destino = cab.SlDestino.Trim() == "1" ? "Nacional" : "Exterior";
            //    modelFlujo.empleado = datemp.nombre_trab.Trim();
            //    var estado = i.Estado.Trim();
            //    modelFlujo.estado = ListaEstados.FirstOrDefault(s => s.Codigo.Equals(estado)).Descripcion;
            //    modelFlujo.motivogasto = cab.MotivoGasto;
            //    modelFlujo.comentario = i.Comentario == null ? "" : i.Comentario.Replace(".", string.Empty).Replace("|", "\n");
            //    modelFlujo.fechasolicitud = i.FechaCreacion.ToString();
            //    listmodelFlujo.Add(modelFlujo);

            //}

            return View(listmodelFlujo);
        }
        [HttpPost]
        public PartialViewResult SearchFlujos(FilterFlujosModel filter)
        {

            List<SelectListItem> motivoAnticipo = GetMotivoAnticipoList();

            List<MyReportModel> lstreport = new List<MyReportModel>();
            var usuarioLog = User.Identity.Name;
            ViewBag.Usuario = User.Identity.Name;
            var mensaje = "";

            if (usuarioLog != null)
            {
                var query = db.Cab_Trans.Where(g => g.Estado == filter.Estados).AsQueryable();
                var query2 = db.Cab_transFae.Where(g => g.Estado == filter.Estados).AsQueryable();

                if (filter.fdesde.HasValue)
                {
                    query = query.Where(s => s.FechaSolicitud >= filter.fdesde);
                    query2 = query2.Where(s => s.FechaSolicitud >= filter.fdesde);
                }

                if (filter.fhasta.HasValue)
                {
                    query = query.Where(s => s.FechaSolicitud >= filter.fhasta);
                    query2 = query2.Where(s => s.FechaSolicitud >= filter.fhasta);
                }

                var listaCab = query.ToList();
                var listaCabFae = query2.ToList();
                var indice = 0;
                foreach (var i in listaCab)
                {
                    indice++;
                    MyReportModel obj = new MyReportModel();
                    obj.indice = indice;
                    obj.id = i.Id;
                    obj.secuencial = "RPG-000" + i.Secuencial;
                    obj.estado = db.Estados_Flujos.FirstOrDefault(s => s.Codigo.Trim() == i.Estado.Trim()).Descripcion;
                    obj.formulario = "RPG";
                    obj.slctTipo = "Reembolso de Gastos";
                    obj.motivogasto = i.MotivoGasto;
                    obj.fechasolicitud = i.FechaSolicitud.ToString("dddd, dd MMMM yyyy");
                    lstreport.Add(obj);
                }
                foreach (var i in listaCabFae)
                {
                    indice++;
                    MyReportModel obj = new MyReportModel();
                    obj.indice = indice;
                    obj.id = i.Id;
                    obj.secuencial = "FAE-000" + i.Secuencial;
                    obj.estado = db.Estados_Flujos.FirstOrDefault(s => s.Codigo.Trim() == i.Estado.Trim()).Descripcion;
                    obj.formulario = "FAE";
                    obj.slctTipo = "Anticipo de efectivo";
                    obj.motivogasto = i.motivoAnticipo;
                    obj.fechasolicitud = i.FechaSolicitud.ToString("dddd, dd MMMM yyyy");
                    lstreport.Add(obj);
                }


            }
            else
            {
                mensaje = "Inicia sesion para continuar";
            }


           
            //var empleados = db.Cab_Trans.Where(s => s.Estado.Trim() == filter.Estados).OrderByDescending(s => s.Secuencial).ToList();
            //var empleadosFae = db.Cab_transFae.Where(s => s.Estado.Trim() == filter.Estados).OrderByDescending(s => s.Secuencial).ToList();
            //var listaEstados = db.Estados_Flujos.ToList();
            //var listTR = db.TipoReportes.ToList();
            //var listmodelFlujo = new List<ListFlujosModel>();
            //try
            //{
            //    if (empleados != null)
            //    {
            //        listmodelFlujo = empleados.Select(i => new ListFlujosModel
            //        {
            //            Id = i.Id,
            //            estado = listaEstados.SingleOrDefault(s => s.Codigo.Equals(i.Estado.Trim())).Descripcion,
            //            fechasolicitud = i.FechaSolicitud.ToString(),
            //            motivogasto = i.MotivoGasto,
            //            empleado = i.UsuarioCreacion,
            //            form = "RPG",
            //            secuencial = i.SlTipoConsumo.Trim() == "3" ? "RPG-00000" + i.Secuencial : "00000" + i.Secuencial,
            //            sltipo = listTR.SingleOrDefault(s => s.Id == int.Parse(i.SlTipoConsumo)).Descripcion
            //        }).ToList();

            //    }


            //    if (empleadosFae != null)
            //    {
            //        listmodelFlujo.AddRange(empleadosFae.Select(i => new ListFlujosModel
            //        {
            //            Id = i.Id,
            //            estado = listaEstados.SingleOrDefault(s => s.Codigo.Equals(i.Estado.Trim())).Descripcion,
            //            fechasolicitud = i.FechaSolicitud.ToString(),
            //            motivogasto = motivoAnticipo.SingleOrDefault(s => s.Value == i.motivoAnticipo).Text,
            //            empleado = i.UsuarioCreacion,
            //            form = "FAE",
            //            secuencial = "FAE-00000" + i.Secuencial
            //            //sltipo = listTR.SingleOrDefault(s => s.Id == int.Parse(i.SlTipoConsumo)).Descripcion
            //        }));
            //    }



            //}
            //catch (Exception ex)
            //{

            //    var a = ex.Message;
            //}




            return PartialView("_SearchFlujos", lstreport);
        }

    



        #region MetodosPrivate
        private List<FlujoApro> FlujoAprobaciones(int secuencial)
        {

            var ListaFlujos = db.Flujo.Where(s => s.Secuencial == secuencial).Select(s => new FlujoApro
            {
                Aprobador = db.Maestro_SharePoint.FirstOrDefault(x => x.trabajador == s.Revisor).nombre_trab,
                Estado = s.Estado.Trim(),
                Nivel = s.Nivel,
                FechaCreacion = s.FechaCreacion


            }).ToList();


            return ListaFlujos;
        }

        private EnvioCorreoModel ObtenerEnvioCorreoModel(int secuencial, string Form)
        {
         
            
            EnvioCorreoModel obj = new EnvioCorreoModel();

            var formu = Form;
            var ListaFLujos = db.Flujo.Where(s => s.Secuencial == secuencial && s.Estado != "A" && s.Formulario == formu)
                                      .OrderBy(o => o.Nivel)
                                      .ToList();
            try
            {
                if (ListaFLujos.Any())
                {
                    var isRPG = formu == "RPG";
                    string trabajador;
                    if (isRPG)
                    {
                        var cab = db.Cab_Trans.FirstOrDefault(s => s.Secuencial == secuencial);
                        trabajador = cab.Codigo;
                        obj.secuen = cab.Secuencial;
                        obj.Id = cab.Id;
                    }
                    else
                    {
                        var cab = db.Cab_transFae.FirstOrDefault(s => s.Secuencial == secuencial);
                        trabajador = cab.codEmpleado.ToString();
                        obj.secuen = cab.Secuencial;
                        obj.Id = cab.Id;
                    }

                    var codrevisor = ListaFLujos[0].Revisor;
                  
          
                    bool readOnly = true; // valor estático para el parámetro readOnly
                    bool aprobador = true; // valor estático para el parámetro aprobador

                    string linkParams = $"?readOnly={readOnly}&aprobador={aprobador}";
                    string link = $"http://172.28.90.100:8093/PIC/EditReport/{obj.Id}{linkParams}";
                 
                    obj.Link = link;

                    //obj.Link = $"http://172.28.90.100:8093/PIC/RevisarReporte{(isRPG ? "" : "Fae")}?id={obj.Id}";
                    obj.TipoReporte = isRPG ? "Reembolso de gasto" : "Anticipo de efectivo";
                    obj.Formulario = formu;
                    obj.CorreoAprobador = ListaFLujos[0].Correo_Revisor.Trim();
                    obj.CorreoSolicitante = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == trabajador).e_mail.Trim();
                    obj.NombreAprobador = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == codrevisor).nombre_trab;
                    obj.NombreSolicitante = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == trabajador).nombre_trab;

                }


                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        private List<ViewTCModel> ObtenerTarjetasCredito(int? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            DataPic db = new DataPic();
            List<ViewTCModel> Lstmodel = new List<ViewTCModel>();
            if (estado.HasValue)
            {
                if (estado == 100)
                {
                    estado = null;
                }
            }
            var query = db.TC_EstadoCuenta.Where(s => s.FechaConsumo.Year == 2022).Distinct().AsQueryable();
            if (estado.HasValue)
            {
                query = query.Where(s => s.Estado == estado.Value);
            }

            if (fechaDesde.HasValue)
            {
                query = query.Where(s => s.FechaConsumo >= fechaDesde.Value);
            }
            if (fechaHasta.HasValue)
            {
                query = query.Where(s => s.FechaConsumo <= fechaHasta.Value);
            }
            var Corte = query.Select(s => s.Corte).Distinct().ToList();
            foreach (var item in Corte)
            {
                var query1 = db.TC_EstadoCuenta.Where(c => c.Corte == item).Distinct().AsQueryable();

                if (estado.HasValue)
                {
                    query1 = query1.Where(s => s.Estado == estado.Value);
                }

                if (fechaDesde.HasValue)
                {
                    query1 = query1.Where(s => s.FechaConsumo >= fechaDesde.Value);
                }
                if (fechaHasta.HasValue)
                {
                    query1 = query1.Where(s => s.FechaConsumo <= fechaHasta.Value);
                }

                var query2 = query1.ToList();

                ViewTCModel model = new ViewTCModel();
                model.SumTC = query2.Count();
                model.Corte = item.ToString();
                model.Tc = new List<Tc>();

                var ListaTc = query2.Select(l => l.NumeroTC).Distinct().ToList();

                List<Tc> Listo = new List<Tc>();
                foreach (var i in ListaTc)
                {

                    var query3 = db.TC_EstadoCuenta.Where(g => g.Corte == item && g.NumeroTC == i).Distinct().AsQueryable();

                    if (estado.HasValue)
                    {
                        query3 = query3.Where(s => s.Estado == estado.Value);
                    }

                    if (fechaDesde.HasValue)
                    {
                        query3 = query3.Where(s => s.FechaConsumo >= fechaDesde.Value);
                    }
                    if (fechaHasta.HasValue)
                    {
                        query3 = query3.Where(s => s.FechaConsumo <= fechaHasta.Value);
                    }

                    var query4 = query3.ToList();
                    Tc o = new Tc();
                    o.Tcredito = i;
                    var EstCuenta = query4;
                    o.SumItem = EstCuenta.Count();
                    o.SumaValor = EstCuenta.Sum(s => s.Valor);
                    o.Consumos = EstCuenta.Select(e => new Consumo
                    {

                        Id = e.Id,
                        Estado = e.Estado,
                        Documento = e.Documento.ToString(),
                        Descripcion = e.Descripcion,
                        Cedula = e.Cedula,
                        Fecha = e.FechaConsumo.ToString("dd 'de' MMMM 'del' yyyy"),
                        Nombre = e.Nombre,
                        Valor = e.Valor.ToString()



                    }).ToList();

                    Listo.Add(o);
                }
                model.Tc.AddRange(Listo);



                Lstmodel.Add(model);
            }

            return Lstmodel;
        }

        private byte[] ConvertHttpPostedFileBaseABase64(HttpPostedFileBase archivo)
        {
            byte[] Img64;

            using (var binaryReader = new BinaryReader(archivo.InputStream))
            {
                Img64 = binaryReader.ReadBytes(archivo.ContentLength);
            }

            return Img64;
        }

        private byte[] GetImageData(int imageId)
        {
            // Reemplaza esta parte con tu lógica de acceso a datos para obtener la imagen de la base de datos
            using (var db = new DataPic())
            {
                var imageRecord = db.Sharepoint_File.SingleOrDefault(i => i.Id == imageId).Archive;
                if (imageRecord != null)
                {
                    return imageRecord;
                }
            }

            // Si no se encuentra la imagen, devuelve null o una imagen predeterminada
            return null;
        }

        private EmpleadoModel GetEmpleadoModel(string codigo)
        {
            EmpleadoModel ep = new EmpleadoModel();
            var objE = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == codigo);

            if (objE == null)
            {
                ep.Bandera = "V";
                return ep;
            }

            var Gerentes = db.GerentesAreas.Any(x => x.CodTrabajador == codigo);
            var Directores = db.DirectorAreas.Any(x => x.CodTrabajador == codigo);

            if (Gerentes || Directores)
            {
                ep.CodAreaResponsable = db.DirectorAreas
                    .Where(s => s.AreaResp == objE.area_resp)
                    .Select(s => s.CodTrabajador)
                    .FirstOrDefault();

                ep.Bandera = Gerentes ? "G" : "D";
            }
            else
            {
                var ListaGerentes = db.GerentesAreas
                    .Where(s => s.AreaResp == objE.area_resp)
                    .ToList();

                if (ListaGerentes.Any())
                {
                    ep.CodAreaResponsable = ListaGerentes
                        .FirstOrDefault(s => s.Depto == objE.depto)?.CodTrabajador
                        ?? ListaGerentes.First().CodTrabajador;
                }
                else
                {
                    ep.CodAreaResponsable = "";
                }

                ep.Bandera = "T";
            }

            ep.Codigo = codigo;
            ep.Nombre = objE.nombre_trab.Trim();
            ep.CentroCosto = objE.centr_costo.Trim() + " - " + objE.centro_costo_des.Trim();
            ep.Departamento = objE.depto_des.Trim();
            ep.CorreoAutoridadF = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == ep.CodAreaResponsable)?.e_mail;
            ep.Compania = objE.nombre_cia.Trim();
            ep.Area = objE.area_resp_des.Trim();

            return ep;
        }

        private List<SelectListItem> GetMotivoAnticipoList()
        {
            return new List<SelectListItem>
                    {
                    new SelectListItem { Value = "1", Text = "Capacitación" },
                    new SelectListItem { Value = "2", Text = "Eventos / Ferias" },
                    new SelectListItem { Value = "3", Text = "Otros" },
                    new SelectListItem { Value = "4", Text = "Reuniones de trabajo" },
                    new SelectListItem { Value = "5", Text = "Trabajos en otras cias.DOLE" },
                    new SelectListItem { Value = "6", Text = "Tramites Visa (trabajo)" }
                    };
        }

        #endregion



        public ActionResult RevisarReporteUser(int id)
        {


            #region Listas

            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });

            #endregion
            var idS = id;
            var data = db.Cab_Trans.FirstOrDefault(s => s.Id == idS);
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var lstDirectores = db.DirectorAreas.ToList();
            var ListaTipConsumo = db.TipoConsumo.ToList();
            var ListaMoneda = db.Sharepoint_Monedas.ToList();
            var ListDet = db.Det_Trans.Where(v => v.Id_Cab == data.Id).ToList();
            var listaflujos = db.Estados_Flujos.ToList();
            var lstSHFs = db.Sharepoint_Flujos.Where(s => s.Secuencial == data.Secuencial).OrderBy(x => x.FechaCreacion).ToList();
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);

            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-", Codigo = 0 });
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", 0);
            var listaEstadoFlujos = db.Flujo.Where(z => z.Secuencial == data.Secuencial && z.Estado.Trim() == "P").OrderBy(s => s.Id).ToList();
            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-", CodTrabajador = "" });
            var datemp = db.Maestro_SharePoint.First(x => x.trabajador.Equals(data.Codigo));
            RpgAllModel obj = new RpgAllModel();
            var estado = lstSHFs.FirstOrDefault().Estado;
            #region Cabecera
            obj.Id = data.Id;
            obj.estado = data.Estado.Trim();
            obj.secuencial = data.Secuencial.ToString(); ;
            obj.codigoEmpleado = data.Codigo;
            obj.nombre = datemp.nombre_trab;
            obj.departamento = datemp.depto_des;
            obj.razonsocial = datemp.nombre_cia;
            obj.area = datemp.area_resp_des;
            obj.status = listaflujos.FirstOrDefault(s => s.Codigo == estado.Trim()).Descripcion;
            obj.fechasolicitud = data.FechaSolicitud.ToString();
            obj.centrocosto = data.CentroCosto;
            obj.centrocostocargadoa = data.CodCentroCostoA;
            obj.director = data.IdDirector;
            obj.autoridadFinanciera = data.IdAutoridadFinanciera;
            obj.slctTipo = data.SlTipoConsumo;
            obj.checkIn1 = data.CodCentroCostoA != "0" ? 1 : 0;
            obj.checkDirector = data.IdDirector != null ? 1 : 0;
            if (listaEstadoFlujos.Any())
            {
                obj.nivel = listaEstadoFlujos.FirstOrDefault(s => s.Estado.Trim() == "P").Nivel;
                obj.Nivel1 = listaEstadoFlujos.FirstOrDefault(s => s.Estado.Trim() == "P").Correo_Revisor;
            }

            obj.slctTipo = data.SlTipoConsumo.Trim();
            obj.motivogasto = data.MotivoGasto.Trim();
            obj.fechadesde = data.FechaDesde;
            obj.fechahasta = data.FechaHasta;

            //obj.Nivel1 = listaEstadoFlujos.FirstOrDefault(s => s.Estado.Trim() == "P").Correo_Revisor;
            if (!string.IsNullOrEmpty(data.Nivel2))
                obj.Nivel2 = data.Nivel2.Trim();
            if (!string.IsNullOrEmpty(data.Nivel3))
                obj.Nivel3 = data.Nivel3.Trim();
            obj.DetallConsumo = new List<ConsumoViewModel2>();
            obj.FlujoApro = new List<FlujoApro>();






            #endregion
            #region Detalle
            List<ConsumoViewModel2> ListaDetalle = new List<ConsumoViewModel2>();
            #endregion

            var usuarioLog = User.Identity.Name;
            var idDetalle = 1;
            foreach (var a in ListDet)
            {
                var proveedor = db.Proveedores.FirstOrDefault(s => s.Id == a.IdProvee);
                var factura = db.Facturas.FirstOrDefault(s => s.Id_Det == a.Id);
                var ListInvitados = db.Invitados.Where(v => v.Id_Det_Trans == a.Id).ToList();
                var ListaFIle = db.Sharepoint_File.Where(s => s.IdDet == a.Id).ToList();

                ConsumoViewModel2 det = new ConsumoViewModel2();
                det.id = idDetalle;

                det.idDetFile = a.Id;
                det.idtipoconsumo = a.IdTipoConsumo;
                det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Codigo == a.IdTipoConsumo).Descripcion;
                det.idmoneda = a.IdMoneda;
                det.destinoD = a.destinoD.ToString();
                det.moneda = ListaMoneda.FirstOrDefault(s => s.Id == a.IdMoneda).Moneda;
                det.tcambio = a.Tcambio.ToString();
                det.monto = decimal.Round(decimal.Parse(a.Monto.ToString()), 2).ToString();
                det.valor = a.Valor.ToString();
                det.inputfile = a.inputfile;
                det.namefile = a.namefile;

                det.descripcionGastos = a.Descripcion;
                det.AtencionNegocios = a.AtencionNegocios.ToString();
                if (db.Sharepoint_File.Any(s => s.IdDet == a.Id))
                {
                    det.file = "ImagenBase";
                    det.filename = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == a.Id).NameFile;
                    det.idFile = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == a.Id).Id;

                }

                if (proveedor != null)
                {
                    det.rucproveedor = proveedor.Ruc == null ? "" : proveedor.Ruc;
                    det.razonsocial = proveedor.RazonSocial;

                }
                //det.idtipoproveedor = proveedor.Tipo == null ? 0 : proveedor.Tipo;
                if (!string.IsNullOrEmpty(factura.TipoFactura))
                {

                    var fact = factura.TipoFactura.Trim();
                    det.tipofactura = fact.Trim().Equals("1") ? "Fisica" : "Electronica";
                    det.idtipofactura = factura.TipoFactura.Trim();
                }

                det.idtipoproveedor = a.IdProveedor;


                det.numerofactura = factura.NumFactura;
                det.fechafactura = factura.FechaFactura.ToString("yyyy-MM-d");
                det.descripcionGastos = a.Descripcion;


                det.ArrayInvitados = new List<Models.Invitadoss>();
                //det.ArrayFiles = new List<Models.Filess>();
                List<Models.Invitadoss> listainvi = new List<Models.Invitadoss>();

                foreach (var i in ListInvitados)
                {
                    Models.Invitadoss invi = new Models.Invitadoss();
                    invi.nombre = i.Nombre;
                    invi.cargo = i.Cargo;
                    invi.empresa = i.Empresa;
                    listainvi.Add(invi);

                }


                //file.Archive = "ImagenBase";
                //file.IdCab = i.IdCab;
                //file.IdDet = i.IdDet;
                //file.NameFile = i.NameFile;
                //file.ContentType = i.ContentType;
                //file.Extension = i.Extension;
                //listafile.Add(file);



                det.ArrayInvitados.AddRange(listainvi);
                idDetalle = idDetalle + 1;
                ListaDetalle.Add(det);


            }

            var ListaFlujos = db.Flujo.Where(s => s.Secuencial == data.Secuencial).Select(s => new FlujoApro
            {
                Aprobador = db.Maestro_SharePoint.FirstOrDefault(x => x.trabajador == s.Revisor).nombre_trab,
                Estado = s.Estado.Trim(),
                Nivel = s.Nivel,
                FechaCreacion = s.FechaCreacion


            }).ToList();

            obj.FlujoApro.AddRange(ListaFlujos);
            obj.DetallConsumo.AddRange(ListaDetalle);

            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.autoridadFinanciera);
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.director);
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", data.CodCentroCostoA);
            return View(obj);
            //List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            //{
            //    Id = s.Id,
            //    Codigo = s.Codigo,
            //    Name = s.Codigo + "-" + s.Descripcion

            //}).ToList();
            //lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });


            //var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();


            //lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            //var lstDirectores = db.DirectorAreas.ToList();
            //lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-", CodTrabajador = "" });

            //var idS = id;
            //var ListaTipConsumo = db.TipoConsumo.ToList();
            //var ListaMoneda = db.Sharepoint_Monedas.ToList();
            //var data = db.Cab_Trans.FirstOrDefault(s => s.Id == idS);
            //var datemp = db.Maestro_SharePoint.First(x => x.trabajador.Equals(data.Codigo));
            //var ListDet = db.Det_Trans.Where(v => v.Id_Cab == data.Id).ToList();



            //RpgAllModel obj = new RpgAllModel();
            //obj.Id = data.Id;
            //obj.secuencial = data.Secuencial.ToString(); ;
            //obj.codigoEmpleado = data.Codigo;
            //obj.nombre = datemp.nombre_trab;
            //obj.departamento = datemp.depto_des;
            //obj.razonsocial = datemp.nombre_cia;
            //obj.area = datemp.area_resp_des;

            //obj.centrocosto = data.CentroCosto;
            //obj.centrocostocargadoa = data.CodCentroCostoA;
            //obj.director = data.IdDirector;
            //obj.autoridadFinanciera = data.IdAutoridadFinanciera;
            //obj.slctTipo = data.SlTipoConsumo;
            //obj.checkIn1 = data.CodCentroCostoA != "0" ? 1 : 0;
            //obj.checkDirector = data.IdDirector != null ? 1 : 0;
            ////obj.destino = data.SlDestino.Trim();
            //obj.slctTipo = data.SlTipoConsumo.Trim();
            //obj.motivogasto = data.MotivoGasto.Trim();
            //obj.fechadesde = data.FechaDesde;
            //obj.fechahasta = data.FechaHasta;
            //obj.Nivel1 = data.Nivel1.Trim();
            //obj.Nivel2 = data.Nivel2.Trim();
            //if (!string.IsNullOrEmpty(data.Nivel3))
            //    obj.Nivel3 = data.Nivel3.Trim();
            //obj.DetallConsumo = new List<ConsumoViewModel2>();



            //List<ConsumoViewModel2> ListaDetalle = new List<ConsumoViewModel2>();
            //foreach (var a in ListDet)
            //{
            //    var proveedor = db.Proveedores.FirstOrDefault(s => s.Id == a.IdProvee);
            //    var factura = db.Facturas.FirstOrDefault(s => s.Id_Det == a.Id);
            //    var ListInvitados = db.Invitados.Where(v => v.Id_Det_Trans == a.Id).ToList();

            //    ConsumoViewModel2 det = new ConsumoViewModel2();
            //    det.idtipoconsumo = a.IdTipoConsumo;
            //    det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Codigo == a.IdTipoConsumo).Descripcion;
            //    det.idmoneda = a.IdMoneda;
            //    det.moneda = ListaMoneda.FirstOrDefault(s => s.Id == a.IdMoneda).Moneda;
            //    det.tcambio = a.Tcambio.ToString();
            //    det.monto = decimal.Round(decimal.Parse(a.Monto.ToString()), 2).ToString();
            //    det.valor = a.Valor.ToString();
            //    det.descripcionGastos = a.Descripcion;
            //    if (proveedor != null)
            //    {
            //        det.rucproveedor = proveedor.Ruc == null ? "" : proveedor.Ruc;
            //        det.razonsocial = proveedor.RazonSocial;

            //    }
            //    //det.idtipoproveedor = proveedor.Tipo == null ? 0 : proveedor.Tipo;

            //    //det.tipoproveedor = proveedor.Tipo;
            //    det.idtipofactura = factura.TipoFactura.Trim();
            //    det.tipofactura = factura.TipoFactura.Equals(1) ? "Fisica" : "Electronica";
            //    det.numerofactura = factura.NumFactura;
            //    det.fechafactura = factura.FechaFactura.ToString("yyyy-MM-d");
            //    det.descripcionGastos = a.Descripcion;
            //    det.AtencionNegocios = a.AtencionNegocios == 1? "1":"0";
            //    det.ArrayInvitados = new List<Models.Invitadoss>();
            //    List<Models.Invitadoss> listainvi = new List<Models.Invitadoss>();

            //    foreach (var i in ListInvitados)
            //    {
            //        Models.Invitadoss invi = new Models.Invitadoss();
            //        invi.nombre = i.Nombre;
            //        invi.cargo = i.Cargo;
            //        invi.empresa = i.Empresa;
            //        listainvi.Add(invi);

            //    }
            //    det.ArrayInvitados.AddRange(listainvi);
            //    ListaDetalle.Add(det);



            //}
            //obj.DetallConsumo.AddRange(ListaDetalle);

            //ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.autoridadFinanciera);
            //ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.director);
            //ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", data.CodCentroCostoA);
            //return View(obj);
        }



        public ActionResult RevisarReportefae(int id)
        {

            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_transFae.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();



            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");


            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-" });
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);



            #region Listas


            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });


            #endregion
            var idS = id;
            var data = db.Cab_transFae.FirstOrDefault(s => s.Id == idS);

            var ListaTipConsumo = db.TipoConsumo.ToList();
            var ListaMoneda = db.Sharepoint_Monedas.ToList();
            var ListDet = db.Det_TransFae.Where(v => v.IdCab_TransFae == data.Id).ToList();


            var codigo = data.codEmpleado;
            var datemp = db.Maestro_SharePoint.First(x => x.trabajador == codigo.ToString());
            var detFae = db.Det_TransFae.Where(s => s.IdCab_TransFae == data.Id).ToList();
            var lstFlujo = db.Flujo.Where(s => s.Secuencial == data.Secuencial && s.Estado == "P").ToList();
            FaeViewModel obj = new FaeViewModel();

            #region Cabecera
            obj.Id = data.Id.ToString();
            obj.estado = data.Estado.Trim();
            obj.secuencial = data.Secuencial.ToString(); ;
            obj.codigoEmpleado = data.codEmpleado.ToString();
            obj.nombre = datemp.nombre_trab;
            obj.AutoFinanciera = data.IdAutoridadFinanciera.ToString();
            obj.departamento = datemp.depto_des;
            obj.razonsocial = datemp.nombre_cia;
            obj.centrocosto = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centro_costo_des;
            obj.area = datemp.area_resp_des;
            obj.CentroCostoCargado = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centr_costo;
            var ListaRechazados = db.Sharepoint_Flujos.Where(s => s.Secuencial == data.Secuencial && s.Estado.Trim() == "R").ToList();
            obj.destino = data.destino.ToString();
            obj.MotivoAnticipoEfectivo = data.motivoAnticipo;
            obj.Comentario = data.Comentarios;
            obj.usaTc = data.usaTc.ToString();
            obj.fechadesde = data.fechadesde.ToString("yyyy-MM-dd");
            obj.fechahasta = data.fechahasta.ToString("yyyy-MM-dd");
            obj.cantInvitados = db.Det_TransFae.FirstOrDefault(v => v.IdCab_TransFae == data.Id).cantInvitados.ToString();
            obj.DetallGasto = new List<DetalleFae>();
            if(lstFlujo.Count >0)
            obj.nivel1 = lstFlujo[0].Correo_Revisor;
            obj.FlujoApro =new  List<FlujoApro>();
            TimeSpan dias = data.fechahasta - data.fechadesde;

            List<DetalleFae> Lstdet = new List<DetalleFae>();
            foreach (var x in detFae)
            {

                DetalleFae det = new DetalleFae();
                det.id = x.Id;
                det.cantInvitados = x.cantInvitados.ToString();
                det.descripcionGastos = x.descripcionGastos;
                det.idtipoconsumo = x.Idtipoconsumo.ToString();
                det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Id == x.Idtipoconsumo).Descripcion;
                det.valor = x.valor.ToString();
                var total = x.valor * x.cantInvitados * dias.Days;
                det.total = total.ToString();
                //det.ArrayAsistente = new List<Asist>();
                Lstdet.Add(det);
                List<Asist> LstAsis = new List<Asist>();
                //var asistentes = db.Fae_Asistentes.Where(s => s.Id_Det_TransFae == det.id).ToList();
                //foreach (var f in asistentes)
                //{
                //    Asist asi = new Asist();
                //    asi.nombre = f.Nombre;
                //    LstAsis.Add(asi);

                //}
                //det.ArrayAsistente.AddRange(LstAsis);


            }
            obj.DetallGasto.AddRange(Lstdet);
            if (ListaRechazados.Any())
            {
                var listarechOrder = ListaRechazados.OrderByDescending(s => s.FechaCreacion).ToList();

            }

            var ListaFlujos = db.Flujo.Where(s => s.Secuencial == data.Secuencial && s.Formulario == "FAE").Select(s => new FlujoApro
            {
                Aprobador = db.Maestro_SharePoint.FirstOrDefault(x => x.trabajador == s.Revisor).nombre_trab,
                Estado = s.Estado.Trim(),
                Nivel = s.Nivel,
                FechaCreacion = s.FechaCreacion


            }).ToList();
            obj.FlujoApro = ListaFlujos;

            var lstPaises = db.Paises.ToList();
            lstPaises.Add(new Paises { Id = 0, Nombre = "-Seleccione-" });
            ViewBag.SlPais = new SelectList(lstPaises.OrderBy(o => o.Nombre), "Id", "Nombre", data.pais);
            var lstCiudades = db.Ciudades.Where(s => s.IdPais == data.pais).ToList();

            List<lstCiudades> lstciu = new List<lstCiudades>();
            foreach (var w in lstCiudades)
            {
                lstCiudades a = new lstCiudades();
                a.idPais = w.Id;
                a.Nombre = w.Nombre;
                lstciu.Add(a);

            }
            ViewBag.Ciudades = new SelectList(lstciu.OrderBy(o => o.Nombre), "idPais", "Nombre", data.ciudad);

            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.AutoFinanciera);
            //ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.CentroCostoCargado.Trim());
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", obj.CentroCostoCargado.Trim());


            #endregion


            if(obj.estado == "A")
            {
                var fechaActual = DateTime.Now;
                var diDs = fechaActual - DateTime.Parse(obj.fechahasta) ;
                if(diDs.Days <=5 && diDs.Days >= 0)
                {
                    obj.AlcanceFae = "1";
                }
            

            }



            return View(obj);
        }
        [HttpPost]
        public PartialViewResult SearchReportes(FilterFlujosModel filter)
        {


            List<MyReportModel> lstreport = new List<MyReportModel>();
            var usuarioLog = User.Identity.Name;
            ViewBag.Usuario = User.Identity.Name;
            var mensaje = "";

            if(usuarioLog != null)
            {
                var query = db.Cab_Trans.Where(g => g.Estado == filter.Estados).AsQueryable();
                var query2 = db.Cab_transFae.Where(g => g.Estado == filter.Estados).AsQueryable();

                if (filter.fdesde.HasValue)
                {
                    query = query.Where(s => s.FechaSolicitud >= filter.fdesde);
                    query2 = query2.Where(s => s.FechaSolicitud >= filter.fdesde);
                }

                if (filter.fhasta.HasValue)
                {
                    query = query.Where(s => s.FechaSolicitud >= filter.fhasta);
                    query2 = query2.Where(s => s.FechaSolicitud >= filter.fhasta);
                }

                var listaCab = query.ToList();
                var listaCabFae = query2.ToList();
                var indice = 0;
                foreach( var i in listaCab)
                {
                    indice++;
                    MyReportModel obj = new MyReportModel();
                    obj.indice = indice;
                    obj.id = i.Id;
                    obj.secuencial = "RPG-000"+ i.Secuencial;
                    obj.status = db.Estados_Flujos.FirstOrDefault(s => s.Codigo.Trim() == i.Estado.Trim()).Descripcion;
                    obj.estado = i.Estado.Trim();
                    obj.formulario = "RPG";
                    var idTipoConsumo = int.Parse(i.SlTipoConsumo.Trim());
                    obj.slctTipo = db.TipoReportes.FirstOrDefault(s => s.Id == idTipoConsumo).Descripcion;
                    obj.motivogasto = i.MotivoGasto;
                    obj.fechasolicitud = i.FechaSolicitud.ToString("dddd, dd MMMM yyyy");
                    lstreport.Add(obj);
                }
                foreach (var i in listaCabFae)
                {
                    indice++;
                    MyReportModel obj = new MyReportModel();
                    obj.indice = indice;
                    obj.id = i.Id;
                    obj.secuencial = "FAE-000" + i.Secuencial;
                    obj.status = db.Estados_Flujos.FirstOrDefault(s => s.Codigo.Trim() == i.Estado.Trim()).Descripcion;
                    obj.estado = i.Estado.Trim();
                    obj.formulario = "FAE";
                    obj.slctTipo = "Anticipo de efectivo";
                    obj.motivogasto = i.motivoAnticipo;
                    obj.fechasolicitud = i.FechaSolicitud.ToString("dddd, dd MMMM yyyy");
                    lstreport.Add(obj);
                }


            }
            else
            {
                mensaje = "Inicia sesion para continuar";
            }




            return PartialView("_SearchReportes", lstreport.OrderBy(x => x.indice));
        }

        public ActionResult MisReportes()
        {

            return View();
        }
        public ActionResult Alcance(int Id)
        {

            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_transFae.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();
            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });


            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-" });


            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });


            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);



            #region Listas


            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });


            #endregion
            var idS = Id;
            var data = db.Cab_transFae.FirstOrDefault(s => s.Id == idS);

            var ListaTipConsumo = db.TipoConsumo.ToList();
            var ListaMoneda = db.Sharepoint_Monedas.ToList();
            var ListDet = db.Det_TransFae.Where(v => v.IdCab_TransFae == data.Id).ToList();
            var codigo = data.codEmpleado;
            var datemp = db.Maestro_SharePoint.First(x => x.trabajador == codigo.ToString());
            var detFae = db.Det_TransFae.Where(s => s.IdCab_TransFae == data.Id).ToList();
            FaeViewModel obj = new FaeViewModel();


            #region Cabecera
            obj.Id = data.Id.ToString();
            obj.estado = data.Estado.Trim();
            obj.secuencial = data.Secuencial.ToString(); ;
            obj.codigoEmpleado = data.codEmpleado.ToString();
            obj.nombre = datemp.nombre_trab;
            obj.AutoFinanciera = data.IdAutoridadFinanciera.ToString();
            obj.departamento = datemp.depto_des;
            obj.razonsocial = datemp.nombre_cia;
            obj.centrocosto = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centro_costo_des;
            obj.area = datemp.area_resp_des;
            obj.CentroCostoCargado = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centr_costo;
            var ListaRechazados = db.Sharepoint_Flujos.Where(s => s.Secuencial == data.Secuencial && s.Estado.Trim() == "R").ToList();
            obj.destino = data.destino.ToString();
            obj.Comentario = data.Comentarios;
            obj.MotivoAnticipoEfectivo = data.motivoAnticipo;
            obj.pais = data.pais.ToString();
            obj.ciudad = data.ciudad.ToString();
            obj.usaTc = data.usaTc.ToString();
            obj.fechadesde = data.fechahasta.AddDays(1).ToString("yyyy-MM-dd");
     
            obj.cantInvitados = db.Det_TransFae.FirstOrDefault(v => v.IdCab_TransFae == data.Id).cantInvitados.ToString();
            //obj.DetallGasto = new List<DetalleFae>();
            obj.nivel1 = data.nivel1;
            obj.nivel2 = data.nivel2;
    
            //obj.lstCiudades = new List<lstCiudades>();

            var lstPaises = db.Paises.ToList();
            lstPaises.Add(new Paises { Id = 0, Nombre = "-Seleccione-" });
            ViewBag.SlPais = new SelectList(lstPaises.OrderBy(o => o.Nombre), "Id", "Nombre", data.pais);

            if (obj.destino == "2")
            {
                obj.nivel2 = "Alegria.Molina@dole.com";

            }


            TimeSpan dias = data.fechahasta - data.fechadesde;
            var lstCiudades = db.Ciudades.Where(s => s.IdPais == data.pais).ToList();

            List<lstCiudades> lstciu = new List<lstCiudades>();
            foreach (var w in lstCiudades)
            {
                lstCiudades a = new lstCiudades();
                a.idPais = w.Id;
                a.Nombre = w.Nombre;
                lstciu.Add(a);

            }
            ViewBag.Ciudades = new SelectList(lstciu.OrderBy(o => o.Nombre), "idPais", "Nombre", data.ciudad);


            //List<DetalleFae> Lstdet = new List<DetalleFae>();
            //foreach (var x in detFae)
            //{

            //    DetalleFae det = new DetalleFae();
            //    det.id = x.Id;
            //    det.cantInvitados = x.cantInvitados.ToString();
            //    det.descripcionGastos = x.descripcionGastos;
            //    det.idtipoconsumo = x.Idtipoconsumo.ToString();
            //    det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Id == x.Idtipoconsumo).Descripcion;
            //    det.valor = x.valor.ToString();
            //    var total = x.valor * x.cantInvitados * dias.Days;
            //    det.total = total.ToString();
            //    det.ArrayAsistente = new List<Asist>();
            //    Lstdet.Add(det);
            //    List<Asist> LstAsis = new List<Asist>();
            //    var asistentes = db.Fae_Asistentes.Where(s => s.Id_Det_TransFae == det.id).ToList();
            //    foreach (var f in asistentes)
            //    {
            //        Asist asi = new Asist();
            //        asi.nombre = f.Nombre;
            //        LstAsis.Add(asi);

            //    }
            //    det.ArrayAsistente.AddRange(LstAsis);


            //}

            //obj.DetallGasto.AddRange(Lstdet);


            if (ListaRechazados.Any())
            {
                var listarechOrder = ListaRechazados.OrderByDescending(s => s.FechaCreacion).ToList();

            }



            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.AutoFinanciera);
            //ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.CentroCostoCargado.Trim());
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", obj.CentroCostoCargado.Trim());


            #endregion




            return View(obj);
      
        }

            public ActionResult EditReport  (int id, bool readOnly = false, bool aprobador = false)
            {



            #region Listas

            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });


            #endregion


            var idS = id;
            var data = db.Cab_Trans.FirstOrDefault(s => s.Id == idS);
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var lstDirectores = db.DirectorAreas.ToList();
            var ListaTipConsumo = db.TipoConsumo.ToList();
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Descripcion", 53);


            var datemp = db.Maestro_SharePoint.First(x => x.trabajador.Equals(data.Codigo));
            RpgAllModel obj = new RpgAllModel();

            #region Cabecera
            obj.Id = data.Id;
            obj.estado = data.Estado.Trim();
            obj.status = db.Estados_Flujos.FirstOrDefault(s => s.Codigo.Trim() == obj.estado).Descripcion;
            obj.secuencial = data.Secuencial.ToString(); 
            obj.codigoEmpleado = data.Codigo;
            obj.nombre = datemp.nombre_trab;
            obj.departamento = datemp.depto_des;
            obj.razonsocial = datemp.nombre_cia;
            obj.fechasolicitud = DateTime.Now.ToString("dddd, dd MMMM yyyy. HH:mm ");
            obj.area = datemp.area_resp_des;
            var ListaRechazados = db.Sharepoint_Flujos.Where(s => s.Secuencial == data.Secuencial && s.Estado.Trim() == "R").ToList();
            if (ListaRechazados.Any())
            {
                var listarechOrder = ListaRechazados.OrderByDescending(s => s.FechaCreacion).ToList();
                obj.comentario = listarechOrder[0].Comentario;
            }
              


            obj.centrocosto = data.CentroCosto;
            obj.centrocostocargadoa = data.CodCentroCostoA;
            obj.director = data.IdDirector;
            obj.autoridadFinanciera = data.IdAutoridadFinanciera;
            obj.slctTipo = data.SlTipoConsumo;
            obj.checkIn1 = data.CodCentroCostoA != "0" ? 1 : 0;
            obj.checkDirector = data.IdDirector != null ? 1 : 0;    
            
            obj.slctTipo = data.SlTipoConsumo.Trim();
            obj.motivogasto = data.MotivoGasto.Trim();
            obj.fechadesde = data.FechaDesde;
            obj.fechahasta = data.FechaHasta;
            if (!string.IsNullOrEmpty(data.Nivel1))
                obj.Nivel1 = data.Nivel1.Trim();
            if (!string.IsNullOrEmpty(data.Nivel2))
                obj.Nivel2 = data.Nivel2.Trim();
            if(!string.IsNullOrEmpty(data.Nivel3))
            obj.Nivel3 = data.Nivel3.Trim();
            obj.DetallConsumo = new List<ConsumoViewModel2>();
            obj.FlujoApro = new List<FlujoApro>();





            #endregion

            #region Detalle
            List<ConsumoViewModel2> ListaDetalle = new List<ConsumoViewModel2>();
          

            var usuarioLog = User.Identity.Name;
            var idDetalle = 1;
            var ListDet = db.Det_Trans.Where(v => v.Id_Cab == data.Id).ToList();

            foreach (var a in ListDet)
            {
         
                         

                ConsumoViewModel2 det = new ConsumoViewModel2();
                det.id = idDetalle;
                det.idDetFile = a.Id;
                det.idtipoconsumo = a.IdTipoConsumo;
                det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Codigo == a.IdTipoConsumo).Descripcion;
                
                det.idmoneda = a.IdMoneda;
                det.destinoD = a.IdProveedor.ToString();
                det.moneda = lstaMonedas.FirstOrDefault(s => s.Id == a.IdMoneda).Moneda;
                det.tcambio = decimal.Round(decimal.Parse(a.Tcambio.ToString()), 2).ToString();
                det.monto = decimal.Round(decimal.Parse(a.Monto.ToString()), 2).ToString();
                det.valor = decimal.Round(decimal.Parse(a.Valor.ToString()), 2).ToString();     
                det.descripcionGastos = a.Descripcion;
                det.AtencionNegocios = a.AtencionNegocios.ToString();
              
          
                if (db.Sharepoint_File.Any(s => s.IdDet == a.Id))
                {
           
                    var archi = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == a.Id);
                    det.filename = Convert.ToBase64String(archi.Archive);
                    det.idFile = archi.Id;

                }

                if (db.Facturas.Any(s => s.Id_Det == a.Id))
                {
                    var facturas = db.Facturas.FirstOrDefault(s => s.Id_Det == a.Id);
                    if (facturas.TipoFactura != null )
                    {
                        det.tipofactura = facturas.TipoFactura.Trim().Equals("1") ? "Fisica" : "Electronica";
                        det.idtipofactura = facturas.TipoFactura.Trim();
                       
                    }
                    det.numerofactura = facturas.NumFactura;
          
                    det.numerofactura = facturas.NumFactura;
                    det.fechafactura = facturas.FechaFactura.ToString("yyyy-MM-dd");

                }
                if (db.Proveedores.Any(s => s.Id == a.IdProvee))
                {
                    var provee = db.Proveedores.FirstOrDefault(s => s.Id == a.IdProvee);
                    det.idtipoproveedor = provee.Tipo;
                    det.tipoproveedor = provee.Tipo == 0?"Nacional":"Exterior";
                    det.rucproveedor = provee.Ruc;
                    det.razonsocial = provee.RazonSocial;


                }


                if(db.Invitados.Any(s => s.Id_Det_Trans == a.Id))
                {
                    var ListInvitados = db.Invitados.Where(s => s.Id_Det_Trans == a.Id).ToList();
                    det.ArrayInvitados = new List<Models.Invitadoss>();
                    //det.ArrayFiles = new List<Models.Filess>();
                    List<Models.Invitadoss> listainvi = new List<Models.Invitadoss>();

                    foreach (var i in ListInvitados)
                    {
                        Models.Invitadoss invi = new Models.Invitadoss();
                        invi.nombre = i.Nombre;
                        invi.cargo = i.Cargo;
                        invi.empresa = i.Empresa;
                        listainvi.Add(invi);

                    }
                    det.ArrayInvitados.AddRange(listainvi);
                }
               

                idDetalle = idDetalle + 1;
                ListaDetalle.Add(det);


            }
            #endregion


            var ListaFlujos = db.Flujo.Where(s => s.Secuencial == data.Secuencial && s.Formulario.Trim() == "RPG").Select(s => new FlujoApro
            {
                Aprobador = db.Maestro_SharePoint.FirstOrDefault(x => x.trabajador == s.Revisor).nombre_trab,
                Estado = s.Estado.Trim(),
                Nivel = s.Nivel,
                FechaCreacion = s.FechaCreacion


            }).ToList();

            obj.FlujoApro.AddRange(ListaFlujos);
            obj.DetallConsumo.AddRange(ListaDetalle);
            obj.aprobador = aprobador;
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });

            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.autoridadFinanciera);
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.director);
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", data.CodCentroCostoA);
            ViewBag.ReadOnly = readOnly;


            if (readOnly)
            {
                obj.FlujoApro = FlujoAprobaciones(data.Secuencial);
            }
            return View(obj);
        }



        public ActionResult EditReportFae(int id)
        {


            var idS = id;
            var data = db.Cab_transFae.FirstOrDefault(s => s.Id == idS);

            var ListaTipConsumo = db.TipoConsumo.ToList();
            var ListaMoneda = db.Sharepoint_Monedas.ToList();
            var ListDet = db.Det_TransFae.Where(v => v.IdCab_TransFae == data.Id).ToList();
            var codigo = data.codEmpleado;
            var datemp = db.Maestro_SharePoint.First(x => x.trabajador == codigo.ToString());
            var detFae = db.Det_TransFae.Where(s => s.IdCab_TransFae == data.Id).ToList();



            #region Listas
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_transFae.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();
            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });


            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-" });


            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });


            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);


            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            var lstCiudades = db.Ciudades.Where(s => s.IdPais == data.pais).ToList();

            List<lstCiudades> lstciu = new List<lstCiudades>();
            foreach (var w in lstCiudades)
            {
                lstCiudades a = new lstCiudades();
                a.idPais = w.Id;
                a.Nombre = w.Nombre;
                lstciu.Add(a);

            }
            ViewBag.Ciudades = new SelectList(lstciu.OrderBy(o => o.Nombre), "idPais", "Nombre", data.ciudad);
                        var lstPaises = db.Paises.ToList();

            lstPaises.Add(new Paises { Id = 0, Nombre = "-Seleccione-" });
            ViewBag.SlPais = new SelectList(lstPaises.OrderBy(o => o.Nombre), "Id", "Nombre", data.pais);

            #endregion


            FaeViewModel obj = new FaeViewModel();


            #region Cabecera
            obj.Id = data.Id.ToString();
            obj.estado = data.Estado.Trim();
            obj.secuencial = data.Secuencial.ToString(); ;
            obj.codigoEmpleado = data.codEmpleado.ToString();
            obj.nombre = datemp.nombre_trab;
            obj.AutoFinanciera = data.IdAutoridadFinanciera.ToString() ;
            obj.departamento = datemp.depto_des;
            obj.razonsocial = datemp.nombre_cia;
            obj.centrocosto= db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centro_costo_des;
            obj.area = datemp.area_resp_des;
            obj.CentroCostoCargado = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador.Trim() == data.codEmpleado.ToString()).centr_costo;
            var ListaRechazados = db.Sharepoint_Flujos.Where(s => s.Secuencial == data.Secuencial && s.Estado.Trim() == "R").ToList();
            obj.destino = data.destino.ToString();
            obj.Comentario = data.Comentarios;
            obj.MotivoAnticipoEfectivo = data.motivoAnticipo;
            obj.pais = data.pais.ToString();
            obj.ciudad = data.ciudad.ToString();
            obj.usaTc = data.usaTc.ToString();
            obj.fechadesde = data.fechadesde.ToString("yyyy-MM-dd");
            obj.fechahasta = data.fechahasta.ToString("yyyy-MM-dd");
            obj.cantInvitados = db.Det_TransFae.FirstOrDefault(v => v.IdCab_TransFae == data.Id).cantInvitados.ToString();
            obj.DetallGasto = new List<DetalleFae>();
            obj.nivel1 = data.nivel1;
            obj.nivel2 = data.nivel2;
            obj.idFile = db.Sharepoint_File.FirstOrDefault(x => x.IdDet == data.Id).Id;

            if(db.Sharepoint_File.Any(v => v.IdDet == data.Id))
            {
                var archi = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == data.Id);
                obj.filename = Convert.ToBase64String(archi.Archive);
                obj.idFile = archi.Id;

            }
        


                List<Asist> LstAsis = new List<Asist>();

                var asistentes = db.Fae_Asistentes.Where(s => s.Id_Cab_TransFae == data.Id).ToList();
                if (asistentes.Any())
                {
                    foreach (var f in asistentes)
                    {
                        Asist asi = new Asist();
                        asi.nombre = f.Nombre;
                        LstAsis.Add(asi);

                    }
                obj.ArrayAsistente = new List<Asist>();
                obj.ArrayAsistente.AddRange(LstAsis);
            }
       
            



            if (obj.destino == "2")
            {
                obj.nivel2 = "Alegria.Molina@dole.com";

            }
            
            
            TimeSpan dias = data.fechahasta - data.fechadesde;


         
            List<DetalleFae> Lstdet = new List<DetalleFae>();
            foreach (var x in detFae)
            {

                DetalleFae det = new DetalleFae();
                det.id = x.Id;
                det.cantInvitados = x.cantInvitados.ToString();
                det.descripcionGastos = x.descripcionGastos;
                det.idtipoconsumo = x.Idtipoconsumo.ToString();
                det.tipoconsumo = ListaTipConsumo.FirstOrDefault(s => s.Id == x.Idtipoconsumo).Descripcion;
                det.valor = x.valor.ToString();
                var total= x.valor * x.cantInvitados * dias.Days;
                det.total = total.ToString();
                //det.ArrayAsistente = new List<Asist>();
                Lstdet.Add(det);
       

            }

            var ListaFlujos = db.Flujo.Where(s => s.Secuencial == data.Secuencial && s.Formulario.Trim() == "FAE").Select(s => new FlujoApro
            {
                Aprobador = db.Maestro_SharePoint.FirstOrDefault(x => x.trabajador == s.Revisor).nombre_trab,
                Estado = s.Estado.Trim(),
                Nivel = s.Nivel,
                FechaCreacion = s.FechaCreacion


            }).ToList();

            obj.FlujoApro = new List<FlujoApro>();
            obj.FlujoApro.AddRange(ListaFlujos);

            obj.DetallGasto.AddRange(Lstdet);


            
            if (ListaRechazados.Any())
            {
                var listarechOrder = ListaRechazados.OrderByDescending(s => s.FechaCreacion).ToList();
               
            }



            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.AutoFinanciera);
            //ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", obj.CentroCostoCargado.Trim());
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", obj.CentroCostoCargado.Trim());


            #endregion

            return View(obj);


        }



        public ActionResult MostrarImagenDet(int id)
        {
            Models.File img = new Models.File();
            var i = db.Sharepoint_File.FirstOrDefault(s => s.IdDet == id);
            img.file = Convert.ToBase64String(i.Archive);
            img.name = i.NameFile;



            return View(img);
        }


        public ActionResult MostrarImagen(int id)
        {
            Models.File img = new Models.File();
            var i = db.Sharepoint_File.FirstOrDefault(s => s.Id == id);
            img.file = Convert.ToBase64String(i.Archive);
            img.name = i.NameFile;



            return View(img);
        }

        [HttpPost]
        public JsonResult ActualizarAF(ActualizarAF obj4)
        {
            var mensaje = "";
            try
            {
                var id = int.Parse(obj4.ids);
                var Cab = db.Cab_Trans.Find(id);
                Cab.IdDirector = obj4.idDirector;
                Cab.IdAutoridadFinanciera = obj4.idGerente;
                db.SaveChanges();
                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;

            }



            return Json(JsonRequestBehavior.DenyGet, mensaje);
        }

        [HttpPost]
        public JsonResult EliminarImagen(int Id)
        {

            db.Sharepoint_File.Remove(db.Sharepoint_File.Find(Id));
            db.SaveChanges();



            return Json(JsonRequestBehavior.DenyGet);
        }


        [HttpPost]
        [AsyncTimeout(10000)]
        public JsonResult EnvioCorreo(int secuencial, string Form , string comentario = null, string accion = null)
        {
            var message = "";


            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var cab = db.Cab_Trans.FirstOrDefault(s => s.Secuencial == secuencial);
                    ActualizarFlujos(secuencial, Form, accion, comentario);
                    var objeto = ObtenerEnvioCorreoModel(cab.Secuencial, Form);


                    var listaFlujo = db.Flujo.Where(s => s.Estado == "P" && s.Secuencial == secuencial && s.Formulario == Form).OrderBy(o => o.Nivel).ToList();
                    var listaFlujoAprobado = db.Flujo.Where(s => s.Estado == "A" && s.Secuencial == secuencial && s.Formulario == Form).OrderBy(o => o.Nivel).ToList();
                    var CountFlujo = db.Flujo.Count(x => x.Secuencial == secuencial && x.Formulario == Form);
                    if (objeto.CorreoAprobador != null)
                    {
                        var correosAprobadores = new Dictionary<string, string>
                        {
                            //{ "Rocio.Moran@dole.com", "edith.vega@dole.com" },
                            //{ "Maria.Mawyin@dole.com","ninah.benitez@dole.com" },
                            //{ "maria.mawyin@dole.com", "ninah.benitez@dole.com" },
                            //{ "Daniel.Teran@dole.com", "Leonardo.criollo@dole.com" }

                            { "Rocio.Moran@dole.com", "lenardo.criollo@dole.com" },
                            { "Maria.Mawyin@dole.com","Leonardo.criollo@dole.com" },
                            { "maria.mawyin@dole.com", "Leonardo.criollo@dole.com" },
                            { "Daniel.Teran@dole.com", "Leonardo.criollo@dole.com" }
                        };

                        if (correosAprobadores.ContainsKey(objeto.CorreoAprobador))
                        {
                            objeto.CorreoAprobador = correosAprobadores[objeto.CorreoAprobador];
                        }

                        try
                        {
                            
                                string body = ComponerCuerpoEmail(objeto, listaFlujoAprobado);
                            
                          

                            List<string> cc = new List<string> { objeto.CorreoSolicitante,"leonardo.criollo@dole.com" };

                            // Envía el email.
                            Mail.Send(objeto.CorreoAprobador, cc, $"Logban - Aprobaciones de Sharepoint {DateTime.Now.ToShortDateString()}", body);
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                        }
                    }

                    string ComponerCuerpoEmail(EnvioCorreoModel objeto2, List<Flujo> listaFlujoAprobado2)
                    {
                        var sb = new StringBuilder();

                        sb.Append("<p>Estimados</p>");
                        sb.Append("<p>Buenos días</p>");

                        // Aquí puedes agregar la lógica para determinar si es un RPG o FAE
                        // y luego llamar a la función correspondiente
                        if (objeto.Formulario == "RPG")
                        {
                            sb.Append(ComponerCuerpoEmailRPG(objeto2, listaFlujoAprobado2));
                        }
                        else
                        {
                            //sb.Append(ComponerCuerpoEmailFAE(objeto2, listaFlujoAprobado2, db));
                        }

                        sb.Append("<p><i>NOTA: Este es un mail automático, favor contestar.</i></p>");

                        return sb.ToString();
                    }

                    string ComponerCuerpoEmailRPG(EnvioCorreoModel objeto2, List<Flujo> listaFlujoAprobado2)
                    {
                        var body = "";
                        body += "<p>";
                        body += "Proceso de aprobación <b> Iniciado por:</b> " + objeto.NombreSolicitante;
                        body += " el " + DateTime.Now.ToString("dddd, d MMMM 'del' yyyy");
                        body += "</p>";

                        body += "</br>";

                        if (listaFlujoAprobado.Any())
                        {
                            foreach (var i in listaFlujoAprobado)
                            {

                                body += "<b> Aprobado por: </b>" + db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == i.Revisor).nombre_trab + " el " + i.FechaCreacion.ToString("dd-MMMM-yyyy");
                                body += "</br>";

                            }

                        }

                        body += "</br>";
                        body += "<b>Nivel " + listaFlujoAprobado.Count + ": </b>" + objeto.NombreAprobador + " FRM-RPG-0000" + objeto.secuen + " <a href= " + objeto.Link + ">" + "Link de aprobación" + "</a>";
                        body += "</br>";

                        body += "</br></br>";
                 
                        return body;

                    }







                    db.SaveChanges(); // Guarda los cambios en la base de datos

                    dbContextTransaction.Commit(); // Confirma la transacción y guarda los cambios en la base de datos
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback(); 
                
                }
            }
     
       
            return Json(message, JsonRequestBehavior.DenyGet);
        }

      
  


        [HttpPost]
        public JsonResult CargarCorreo(string Codigocorreo)
        {
            var correo = db.Maestro_SharePoint.FirstOrDefault(s => s.trabajador == Codigocorreo).e_mail;

            return Json(correo, JsonRequestBehavior.DenyGet);
        }
    
        [HttpPost]
        public JsonResult TotalFae(string idFaee) {
            decimal mensaje = 0;
            if(idFaee != "0")
            {
                var secuencial = int.Parse(idFaee);
                var IdsCab = db.Cab_transFae.Where(s => s.Secuencial == secuencial).Select(x => x.Id).ToList();
                var valor = db.Det_TransFae.Where(s => IdsCab.Contains(s.IdCab_TransFae)).Sum(t => t.total);
                mensaje = valor;
            }
       

            return Json(mensaje, JsonRequestBehavior.DenyGet);
        }



        public ActionResult TarjetasCredito(int? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var list= db.TC_EstadoCuenta.Select(s => s.Corte).Distinct().ToList();
            ViewBag.ListaFechas = new SelectList(list.Select(c => new SelectListItem
            {
                Value = c.ToString(),
                Text = c.ToString()
            }), "Value", "Text");

            var Lstmodel = ObtenerTarjetasCredito(estado, fechaDesde, fechaHasta);
            return View(Lstmodel);

        }
        public ActionResult ActualizarTarjetasCredito(int? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var Lstmodel = ObtenerTarjetasCredito(estado, fechaDesde, fechaHasta);
            return PartialView("_TarjetasCreditoPartial", Lstmodel);
        }

        [HttpPost]
        public PartialViewResult _DetalleDatatableFav()
        {
            // Obtener la lista de agencias de viajes con su ID
            var agencias = db.AgenciaViajes.Select(s => new { Id = s.Id, Nombre = s.Nombre }).ToList();


            // Crear un SelectList a partir de la lista de agencias
            var listaAgencias = new SelectList(agencias, "Id", "Nombre");
             

            // Enviar el SelectList a la vista a través del ViewBag
            ViewBag.ListaAgencias = listaAgencias;


            return PartialView("_DetalleDatatableFav");
        }
        public ActionResult TcJustificar(int ids)
        {
            DataPic db = new DataPic();
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Descripcion", 53);

            EstadCuentaModel model = new EstadCuentaModel();
            var obj = db.TC_EstadoCuenta.Find(ids);

            model.Identificador = obj.Identificador;
            model.Cedula = obj.Cedula;
            model.Corte = obj.Corte;
            model.Valor = obj.Valor;
            model.Descripcion = obj.Descripcion;
            model.Documento = obj.Documento;
            model.FechaConsumo = obj.FechaConsumo.ToString("dd 'de' MMMM 'del' yyyy");
            model.NumeroTC = obj.NumeroTC;
            model.Nombre = obj.Nombre;
            model.Id = ids;

            var obj2 = db.TC_Justificacion.FirstOrDefault(x => x.IdEC == ids);
            TcModel model2 = new TcModel();
            if (obj2 != null)
            {
               
                model2.tipofactura = obj2.TipoFactura.ToString();
                model2.valorFactura = obj2.ValorFactura.ToString();
                model2.comentario = obj2.ComentariosConsumo;
                model2.fecha = obj2.FechaFactura.ToString("yyyy-MM-dd");
                model2.slMoneda = obj2.IdTipoMoneda.ToString();
                model2.tipoconsumo = obj2.TipoConsumo.ToString();
                model2.Tcambio = obj2.TipoCambio.ToString();
               
                model2.rucproveedor = obj2.Ruc;
                model2.razonsocial = obj2.RazonSocial;
                model2.numfactura = obj2.NumFactura;
                model2.tipoproveedor = obj2.TipoProveedor.ToString();
                if(db.TC_Invitados.Any(s => s.IdJustificacion == obj2.Id))
                {
                    model2.invitados = db.TC_Invitados.Where(s => s.IdJustificacion == obj2.Id)
                        .Select(x => new InvitadoTc
                    {
                            Id  = x.Id,
                            nombre = x.Nombre,
                            cargo= x.Cargo,
                            empresa= x.Empresa

                    }).ToList();
                  
                }
                if(db.TC_Files.Any(s => s.IdJustificacion == obj2.Id))
                {

                    var archi = db.TC_Files.FirstOrDefault(s => s.IdJustificacion == obj2.Id).Archivo;
                    model2.ArchivoString = Convert.ToBase64String(archi);
                    Session["fullSizeImage"] = model2.ArchivoString;

                }
          

            }
            else
            {
                model2.Tcambio = "1";

            }
            EstadCuentaTcViewModel viewModel = new EstadCuentaTcViewModel
            {
                EstadCuenta = model,
                Tc = model2
            };


            return View(viewModel);
        }

            [HttpPost]
            public PartialViewResult _DetalleDatatable(int? id , InvitadosModel dataObj)
            {
                InvitadosModel invi = new InvitadosModel();
            if (id.HasValue && db.TC_Invitados.Any(s => s.Id == id))
            {
                if (db.TC_Invitados.Any(s => s.Id == id))
                {
                    invi = db.TC_Invitados.Where(s => s.Id == id).Select(x => new InvitadosModel
                    {
                        nombre = x.Nombre,
                        cargo = x.Cargo,
                        empresa = x.Empresa,
                        id = x.Id
                    }).FirstOrDefault();

                }
            }
            else
            {
                invi = dataObj;
            }
            return PartialView("_DetalleDatatable", invi);



            }    
        [HttpPost]
        public ActionResult TcEnviar(TcModel modelo)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                  
                    TC_Justificacion tc = db.TC_Justificacion.FirstOrDefault(x => x.IdEC == modelo.Id);

                    if (tc == null)
                    {
                        tc = new TC_Justificacion();
                        tc.IdEC = modelo.Id;
                        db.TC_Justificacion.Add(tc);
                    }

                    //tc.IdEC = modelo.Id;
                    tc.TipoConsumo = int.Parse(modelo.tipoconsumo);
                    tc.IdTipoMoneda = int.Parse(modelo.slMoneda);
                    tc.ValorFactura = decimal.Parse(modelo.valorFactura);
                    tc.ComentariosConsumo = modelo.comentario;
                    tc.NumFactura = modelo.numfactura;
                    tc.TipoCambio = decimal.Parse(modelo.Tcambio);
                    tc.FechaFactura = DateTime.Parse(modelo.fecha);
                    tc.Ruc = modelo.rucproveedor;
                    tc.RazonSocial = modelo.razonsocial;
                    tc.FechaCreacion = DateTime.Now;
                    tc.TipoProveedor = int.Parse(modelo.tipoproveedor);
                    tc.TipoFactura = int.Parse(modelo.tipofactura);
                    tc.UsuarioCreacion = "LCriolloC"; // User.Identity.Name;
                    db.SaveChanges();
                    if (modelo.invitados.Any())
                    {
                        // Encuentra todos los registros con el IdJustificacion igual a tc.Id y elimínalos
                        var invitadosExistentes = db.TC_Invitados.Where(x => x.IdJustificacion == tc.Id);
                        db.TC_Invitados.RemoveRange(invitadosExistentes);
                        db.SaveChanges();

                        // Ahora agrega los nuevos registros
                        List<TC_Invitados> Lstinvi = new List<TC_Invitados>();
                        foreach (var i in modelo.invitados)
                        {
                            TC_Invitados invi = new TC_Invitados();
                            invi.IdJustificacion = tc.Id;
                            invi.Nombre = i.nombre;
                            invi.Cargo = i.cargo;
                            invi.Empresa = i.empresa;
                            Lstinvi.Add(invi);
                        }
                        db.TC_Invitados.AddRange(Lstinvi);
                        db.SaveChanges();
                    }


                    if (modelo.archivo != null && modelo.archivo.ContentLength > 0)
                    {
                        TC_Files file = db.TC_Files.FirstOrDefault(x => x.IdJustificacion == tc.Id);

                        if (file == null)
                        {
                            file = new TC_Files();
                            file.IdJustificacion = tc.Id;
                            db.TC_Files.Add(file);
                        }
                        //TC_Files file = new TC_Files();
                        //file.IdJustificacion = tc.Id;
                        //file.IdJustificacion = tc.Id;
                        file.Archivo = ConvertHttpPostedFileBaseABase64(modelo.archivo);
                        file.Extension = "JPG";
                         db.SaveChanges();
                    }
                
                 
               
                    transaction.Commit();
                    var obj = db.TC_EstadoCuenta.Find(modelo.Id);
                    obj.Estado = 1;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);  
        }


  

        public ActionResult CargarTarjetasCredito(string corte, string ntc,string codEmpleado)
        {
            //utilizo int.TryParse() para intentar convertir corte y ntc a números enteros. Si la conversión tiene éxito, los valores convertidos se almacenarán en corteInt y ntcInt. Si la conversión falla, corteInt y ntcInt se establecerán en 0.
            int.TryParse(corte, out int corteInt);
            int.TryParse(ntc, out int ntcInt);
            var cedula = db.Maestro_SharePoint.FirstOrDefault(c => c.trabajador== codEmpleado).cedula;
            var ListaEC = db.TC_EstadoCuenta.Where(s => s.Corte == corteInt && s.NumeroTC == ntcInt && s.Cedula == cedula).ToList();
            //El operador de propagación nula ?. antes de llamar al método ToString(). Esto asegura que si FirstOrDefault() devuelve un valor nulo (es decir, no se encuentra ninguna coincidencia), no se producirá una excepción de referencia nula. 
            List<TcPartialModel> lstModel = ListaEC.Select(s => new TcPartialModel { 

             IdEC = s.Id.ToString(),
            consumo = s.Valor.ToString(),
            descripcion = s.Descripcion,
            documento = s.Documento.ToString(),
            fecha = s.FechaConsumo.ToString(),
            justificado= db.TC_Justificacion.FirstOrDefault(x => x.IdEC == s.Id)?.ValorFactura.ToString(),
               
            
            }).ToList();
            return PartialView("_TablaTCPartial", lstModel);
    
        }




        public ActionResult Show(int imageId)
            {

            // Obtén la imagen de la base de datos utilizando imageId
            byte[] imageData = GetImageData(imageId);

            // Convierte el array de bytes en una imagen
            MemoryStream ms = new MemoryStream(imageData);
            Image img = Image.FromStream(ms);

            // Convierte la imagen en base64
            MemoryStream imageStream = new MemoryStream();
            img.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = imageStream.ToArray();
            string base64Image = Convert.ToBase64String(imageBytes);

            // Devuelve el archivo de imagen
            return File(imageBytes, "image/png");
            //string base64Image = Session[imageId] as string;
            //if (string.IsNullOrEmpty(base64Image))
            //{
            //    // Manejar el caso en que la imagen no se encuentre en la sesión, por ejemplo, redirigir a una página de error
            //    return RedirectToAction("Error", "Home");
            //}

            //ViewBag.Base64Image = base64Image;
            //return View();
        }
        public ActionResult Fav()
        {
            List<SelectListItem> motivoAnticipo = new List<SelectListItem>();

            motivoAnticipo.Add(new SelectListItem() { Value = "1", Text = "Capacitación" });
            motivoAnticipo.Add(new SelectListItem() { Value = "2", Text = "Eventos / Ferias" });
            motivoAnticipo.Add(new SelectListItem() { Value = "3", Text = "Otros" });
            motivoAnticipo.Add(new SelectListItem() { Value = "4", Text = "Reuniones de trabajo" });
            motivoAnticipo.Add(new SelectListItem() { Value = "5", Text = "Trabajos en otras cias.DOLE" });
            motivoAnticipo.Add(new SelectListItem() { Value = "6", Text = "Tramites Visa (trabajo)" });

            ViewBag.usuarioLog = User.Identity.Name;
            var user = User.Identity.Name;
            var lstGerentes = db.GerentesAreas.Where(s => s.Estado == "1").ToList();
            var ListSecuencial = db.Cab_Trans.Select(s => s.Secuencial).ToList();
            var ultimoSecuencial = ListSecuencial.OrderBy(s => s).LastOrDefault();



            lstGerentes.Add(new GerentesAreas { Id = 0, NombreTrab = "-Seleccione-", CodTrabajador = "" });
            ViewBag.SlctGerentes = new SelectList(lstGerentes.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");

            var lstFaes = db.Cab_transFae.Where(s => s.UsuarioCreacion == user && s.Estado != "L").ToList();
            List<SelectListItem> lstFae = lstFaes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = "Secuencial:[FAE-0000" + s.Secuencial.ToString() + "] -" + motivoAnticipo.FirstOrDefault(x => x.Value == s.motivoAnticipo).Text

            }).ToList();
            lstFae.Add(new SelectListItem { Value = "0", Text = "-Seleccionee-" });
            ViewBag.SlctLiquidacionFae = new SelectList(lstFae, "Value", "Text", "0"); 

            var lstDirectores = db.DirectorAreas.ToList();
            lstDirectores.Add(new DirectorAreas { Id = 0, NombreTrab = "-Seleccionee-" });
            ViewBag.SlctDirectores = new SelectList(lstDirectores.OrderBy(s => s.Id), "CodTrabajador", "NombreTrab", "");
            var lstTipConsumo = db.TipoConsumo.Where(s => s.Estado == "1").ToList();
            lstTipConsumo.Add(new TipoConsumo { Id = 0, Descripcion = "-Seleccione-" });
            ViewBag.tipoconsumo = new SelectList(lstTipConsumo.OrderBy(s => s.Id), "Codigo", "Descripcion", "");
            List<ListCentroCosto> lstCD = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            List<ListCentroCosto> lstCC = db.CentroCostos2.Select(s => new ListCentroCosto
            {
                Id = s.Id,
                Codigo = s.Codigo,
                Name = s.Codigo + "-" + s.Descripcion

            }).ToList();
            lstCC.Add(new ListCentroCosto { Id = 0, Name = "-Seleccione-", Codigo = "" });
            ViewBag.SlctCentroCostoa = new SelectList(lstCC.OrderBy(o => o.Id), "Id", "Name", 0);
            var lstaMonedas = db.Sharepoint_Monedas.ToList();
            ViewBag.slMoneda = new SelectList(lstaMonedas.OrderBy(s => s.Moneda), "Id", "Codigo", 0);
            ReporteGastosModel rpg = new ReporteGastosModel();

            rpg.FechaSolicitud = DateTime.Now.ToString("dddd, dd MMMM yyyy. HH:mm ");
            rpg.Estatus = "Inicio";
            rpg.secuencial = (ultimoSecuencial + 1).ToString();
            return View(rpg);
        }


    }
}