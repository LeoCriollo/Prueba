using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoleEcIntranet.Data.DataBalanza;
using DoleEcIntranet.Models;
using DoleEcIntranet.Tools;

namespace DoleEcIntranet.Controllers
{
    [DoleEcIntranetAuthorize]
    public class BalanzaController : Controller
    {
        private BalanzaContext db = new BalanzaContext();

        // GET: Balanza
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminBalanaza)]
        public ActionResult Index()
        {
            List<TipoEmpaqueModels> model = db.TiposEmpaques.Select(s => new TipoEmpaqueModels()
            {
                Id = s.Id,
                Descripcion = s.Descripcion,
                Codigo = s.Codigo,
                Pais = s.Pais,
                PesoCaja = s.PesoCaja,
                PesoLibraFin = s.PesoLibraFin,
                PesoLibraIni = s.PesoLibraIni
            }).ToList();

            return View(model);
        }


        // GET: Balanza/Create
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminBalanaza)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Balanza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdminBalanaza)]
        public ActionResult Create(TipoEmpaqueModels model)
        {
            if (db.TiposEmpaques.Any(f => f.Codigo.Equals(model.Codigo)))
                ModelState.AddModelError("", "Codigo de Empaque ya existe");

            if (ModelState.IsValid)
            {
                TiposEmpaques reg = new TiposEmpaques()
                {
                    Codigo = model.Codigo,
                    FechaCreacion = DateTime.Now,
                    PesoCaja = model.PesoCaja,
                    Descripcion = model.Descripcion,
                    Pais = model.Pais,
                    PesoLibraFin = model.PesoLibraFin,
                    PesoLibraIni = model.PesoLibraIni,
                    UsuarioCreador = User.Identity.Name
                };

                db.TiposEmpaques.Add(reg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Balanza/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposEmpaques reg = db.TiposEmpaques.FirstOrDefault(f=> f.Id==id);
            if (reg == null)
            {
                return HttpNotFound();
            }

            TipoEmpaqueModels model = new TipoEmpaqueModels()
            {
                Id = reg.Id,
                Codigo = reg.Codigo,
                Descripcion = reg.Descripcion,
                Pais = reg.Pais,
                PesoCaja = reg.PesoCaja,
                PesoLibraFin = reg.PesoLibraFin,
                PesoLibraIni = reg.PesoLibraIni
            };



            return View(model);
        }
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.ReportBalanza)]
        public ActionResult ReportBalanza()
        {
            return View();
        }
        // POST: Balanza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoEmpaqueModels model)
        {
            if (ModelState.IsValid)
            {
                var line = db.TiposEmpaques.First(f => f.Id == model.Id);
                line.Pais = model.Pais;
                line.PesoLibraFin = model.PesoLibraFin;
                line.PesoCaja = model.PesoCaja;
                line.PesoLibraIni = model.PesoLibraIni;
                line.UsuarioActualizador = User.Identity.Name;
                line.FechaActualizacion = DateTime.Now;
                line.Descripcion = model.Descripcion;
                line.Codigo = model.Codigo;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [DoleEcIntranetAuthorize(Roles = Tools.Enum.ReportBalanza)]
        public ActionResult ConsultCupo()
        {
            ConsultaCupo model = new ConsultaCupo()
            {
                fechaConsulta = DateTime.Now
            };

            List<DtoGeneric> listaFincas = new List<DtoGeneric>();
            listaFincas = Tools.Tools.FillCombo();
            listaFincas.Add(new DtoGeneric() { Id = 0, Name = "Selecionar Finca-Empacadora" });

            ViewBag.IdFincaEmpacadora = new SelectList(listaFincas.OrderBy(o => o.Id), "Id", "Name", 0);



            return View(model);
        }


        [HttpPost]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.ReportBalanza)]

        public PartialViewResult ConsultCupo(ConsultaCupo request)
        {
            List<DataConsultaCupo> model = new List<DataConsultaCupo>();
            var datacom = Tools.Tools.FillCombo();

            var dataQuery = db.CupoDiario.Where(w => w.IdFinca == request.idFincaEmpacadora && w.FechaHora.Year == request.fechaConsulta.Year && w.FechaHora.Month == request.fechaConsulta.Month && w.FechaHora.Day == request.fechaConsulta.Day).ToList();
            if (dataQuery.Any())
            {
                foreach(var item in dataQuery)
                {
                    DataConsultaCupo line = new DataConsultaCupo();
                    line.cupo = item.Cupo;
                    line.tipoEmpaque = db.TiposEmpaques.First(f => f.Id == item.IdEmpaque).Codigo;
                    line.cajasProcesadas = db.CapturaPesaje.Count(c => c.IdFinca == item.IdFinca &&
                    c.IdEmpaque == item.IdEmpaque && (c.FechaHoraCaptura.Year == item.FechaHora.Year &&
                    c.FechaHoraCaptura.Month == item.FechaHora.Month && c.FechaHoraCaptura.Day == item.FechaHora.Day));
                    line.finca = datacom.First(f => f.Id == item.IdFinca).Name;

                    model.Add(line);

                }
            }

            return PartialView("_ConsultCupo", model);
        }


        //// GET: Balanza/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TipoEmpaqueModels tipoEmpaqueModels = db.TipoEmpaqueModels.Find(id);
        //    if (tipoEmpaqueModels == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tipoEmpaqueModels);
        //}

        //// POST: Balanza/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TipoEmpaqueModels tipoEmpaqueModels = db.TipoEmpaqueModels.Find(id);
        //    db.TipoEmpaqueModels.Remove(tipoEmpaqueModels);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
