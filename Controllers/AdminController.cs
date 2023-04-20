using DoleEcIntranet.Data;
using DoleEcIntranet.Models;
using DoleEcIntranet.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Controllers
{
   
    [DoleEcIntranetAuthorize]
    public class AdminController : Controller
    {
        private EntityIntranet db = new EntityIntranet();
        private ApplicationDbContext context = new ApplicationDbContext();


        #region NoticiasDole
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        // GET: Admin
        public ActionResult NoticiasDoleIndex()
        {           

            List<NoticiasDoleIndexViewModel> model = new List<NoticiasDoleIndexViewModel>();

            var data = db.Contenidos.Where(q => q.IdCategoria == (int)Tools.Enum.Categorias.Noticias_Dole)
                .ToList();

            if (data.Any())
            {
                model = data.Select(s => new NoticiasDoleIndexViewModel()
                {
                    Id = s.Id,
                    TituloArticulo = s.TituloArticulo,
                    ResumenContenido = s.Contenido1,                  
                    FechaCreacion = s.FechaCreacion,
                    UsuarioCreador = s.UsuarioCreacion
                    

                }).ToList();
            }
               

             
            return View("NoticiasDoleIndex",model);
        }

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult CreateNoticiasDole()
        {            

            MB.FileBrowser.MagicSession.Current.FileBrowserAccessMode = IZ.WebFileManager.AccessMode.Delete;
            MB.FileBrowser.MagicSession.Current.AllowedFileTypes = " jpg,jpeg,doc,docx,zip,gif,png,pdf,rar,svg,svgz,xls,xlsx,ppt,pps,pptx,mp4";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult CreateNoticiasDole(NoticiasDoleIndexViewModel model)
        {
            

            if (ModelState.IsValid)
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    model.ImageFile.InputStream.CopyTo(ms);
                    byte[] arrayImg = ms.GetBuffer();
                    //resize image 140 x 100
                    

                    Contenidos reg = new Contenidos()
                    {
                        TituloArticulo = model.TituloArticulo,
                        Contenido2 = model.Contenido,
                        Contenido1 = model.ResumenContenido,
                        IdCategoria = (int)Tools.Enum.Categorias.Noticias_Dole,
                        IdEstado = (int)Tools.Enum.Estados.Creado,
                        Orden = 1,
                        ContenidoImg = arrayImg,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = User.Identity.Name
                    };

                    db.Contenidos.Add(reg);

                }


                db.SaveChanges();

                return RedirectToAction("NoticiasDoleIndex");

            }

            
            return View();

        }

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditNoticiasDole(int id)
        {

            // perimiso para filemanager de administrador
            MB.FileBrowser.MagicSession.Current.FileBrowserAccessMode = IZ.WebFileManager.AccessMode.Delete;
            MB.FileBrowser.MagicSession.Current.AllowedFileTypes = " jpg,jpeg,doc,docx,zip,gif,png,pdf,rar,svg,svgz,xls,xlsx,ppt,pps,pptx,mp4";


            //Obtener Articulos de la Categoria
            var articulo = db.Contenidos.FirstOrDefault(f => f.Id == id && f.IdCategoria == (int)Tools.Enum.Categorias.Noticias_Dole);


            NoticiasDoleIndexViewModel model = new NoticiasDoleIndexViewModel()
            {
                Id = id,
                Categoria = (int)Tools.Enum.Categorias.Noticias_Dole,
                TituloArticulo = articulo.TituloArticulo,
                Estado = articulo.IdEstado,
                ResumenContenido = articulo.Contenido1,
                Order = articulo.Orden,
                Contenido = articulo.Contenido2
            };            


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditNoticiasDole(int id, NoticiasDoleIndexViewModel collection)
        {

            ModelState.Remove("ImageFile");


            if (ModelState.IsValid)
            {
                var articulo = db.Contenidos.FirstOrDefault(f => f.Id == id);

                if (collection.ImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        collection.ImageFile.InputStream.CopyTo(ms);
                        byte[] arrayImg = ms.GetBuffer();
                        articulo.ContenidoImg = arrayImg;
                    }
                }


                articulo.TituloArticulo = collection.TituloArticulo;
                articulo.Contenido1 = collection.ResumenContenido;
                articulo.Contenido2 = collection.Contenido;
                articulo.FechaCreacion = DateTime.Now;
                articulo.FechaModificacion = DateTime.Now;
                articulo.UsuarioModificacion = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("NoticiasDoleIndex");
            }

            return View();
        }


        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult DeleteNoticiasDole(int id)
        {
            var delete = db.Contenidos.Find(id);

            db.Contenidos.Remove(delete);

            db.SaveChanges();

            return RedirectToAction("NoticiasDoleIndex");

        }

        #endregion
        
        #region NoticiasDale
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        // GET: Admin
        public ActionResult NoticiasDaleIndex()
        {

            List<NoticiasDoleIndexViewModel> model = new List<NoticiasDoleIndexViewModel>();

            var data = db.Contenidos.Where(q => q.IdCategoria == (int)Tools.Enum.Categorias.Noticias_Dale)
                .ToList();

            if (data.Any())
            {
                model = data.Select(s => new NoticiasDoleIndexViewModel()
                {
                    Id = s.Id,
                    TituloArticulo = s.TituloArticulo,
                    ResumenContenido = s.Contenido1,
                    FechaCreacion = s.FechaCreacion,
                    UsuarioCreador = s.UsuarioCreacion


                }).ToList();
            }



            return View("NoticiasDaleIndex", model);
        }

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult CreateNoticiasDale()
        {

            MB.FileBrowser.MagicSession.Current.FileBrowserAccessMode = IZ.WebFileManager.AccessMode.Delete;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult CreateNoticiasDale(NoticiasDoleIndexViewModel model)
        {


            if (ModelState.IsValid)
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    model.ImageFile.InputStream.CopyTo(ms);
                    byte[] arrayImg = ms.GetBuffer();
                    //resize image 140 x 100


                    Contenidos reg = new Contenidos()
                    {
                        TituloArticulo = model.TituloArticulo,
                        Contenido2 = model.Contenido,
                        Contenido1 = model.ResumenContenido,
                        IdCategoria = (int)Tools.Enum.Categorias.Noticias_Dale,
                        IdEstado = (int)Tools.Enum.Estados.Creado,
                        Orden = 1,
                        ContenidoImg = arrayImg,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = User.Identity.Name
                    };

                    db.Contenidos.Add(reg);

                }


                db.SaveChanges();

                return RedirectToAction("NoticiasDaleIndex");

            }


            return View();

        }

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditNoticiasDale(int id)
        {

            // perimiso para filemanager de administrador
            MB.FileBrowser.MagicSession.Current.FileBrowserAccessMode = IZ.WebFileManager.AccessMode.Delete;

            //Obtener Articulos de la Categoria
            var articulo = db.Contenidos.FirstOrDefault(f => f.Id == id && f.IdCategoria == (int)Tools.Enum.Categorias.Noticias_Dale);


            NoticiasDoleIndexViewModel model = new NoticiasDoleIndexViewModel()
            {
                Id = id,
                Categoria = (int)Tools.Enum.Categorias.Noticias_Dale,
                TituloArticulo = articulo.TituloArticulo,
                Estado = articulo.IdEstado,
                ResumenContenido = articulo.Contenido1,
                Order = articulo.Orden,
                Contenido = articulo.Contenido2
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditNoticiasDale(int id, NoticiasDoleIndexViewModel collection)
        {

            ModelState.Remove("ImageFile");


            if (ModelState.IsValid)
            {
                var articulo = db.Contenidos.FirstOrDefault(f => f.Id == id);

                if (collection.ImageFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        collection.ImageFile.InputStream.CopyTo(ms);
                        byte[] arrayImg = ms.GetBuffer();
                        articulo.ContenidoImg = arrayImg;
                    }
                }


                articulo.TituloArticulo = collection.TituloArticulo;
                articulo.Contenido1 = collection.ResumenContenido;
                articulo.Contenido2 = collection.Contenido;
                articulo.FechaCreacion = DateTime.Now;
                articulo.FechaModificacion = DateTime.Now;
                articulo.UsuarioModificacion = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("NoticiasDaleIndex");
            }

            return View();
        }


        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult DeleteNoticiasDale(int id)
        {
            var delete = db.Contenidos.Find(id);

            db.Contenidos.Remove(delete);

            db.SaveChanges();

            return RedirectToAction("NoticiasDaleIndex");

        }

        #endregion

        #region reclutamiento Interno

        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditReclutamientoInterno()
        {

            // perimiso para filemanager de administrador
            MB.FileBrowser.MagicSession.Current.FileBrowserAccessMode = IZ.WebFileManager.AccessMode.Delete;

            //Obtener unico articulo
            var articulo = db.Contenidos.FirstOrDefault(f => f.IdCategoria == (int)Tools.Enum.Categorias.Reclutamiento_Interno);

            ReclutamientoInternoViewModel model = new ReclutamientoInternoViewModel()
            {
                Id = articulo.Id,                  
                Contenido = articulo.Contenido2,
                fechaPublicacion = articulo.FechaPublicacion.Value,
                fechaExpiracion = articulo.FechaExpiracion.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult EditReclutamientoInterno(ReclutamientoInternoViewModel collection)
        {

            if (ModelState.IsValid)
            {
                var articulo = db.Contenidos.FirstOrDefault(f => f.Id == collection.Id);
                                              
                articulo.Contenido2 = collection.Contenido;
                articulo.FechaPublicacion = collection.fechaPublicacion;
                articulo.FechaExpiracion = collection.fechaExpiracion;
                articulo.FechaModificacion = DateTime.Now;
                articulo.UsuarioModificacion = User.Identity.Name;

                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }

            return View();
        }

        #endregion


    }
}