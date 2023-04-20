using DoleEcIntranet.Data;
using DoleEcIntranet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoleEcIntranet.Controllers
{
    public class UsersRoleController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext(); 

        // GET: UsersRole
        public ActionResult Index()
        {

            var UserInfo = context.Users.FirstOrDefault();

            if(UserInfo!= null)
            {
                //Usuarios
                List<DtoGeneric> users = context.Users.Select(s => new DtoGeneric()
                {
                    Id = s.Id,
                    Name = s.UserName
                }).ToList();
                ViewBag.IdUser = new SelectList(users.OrderBy(o => o.Name), "Id", "Name", UserInfo.Id);

                //Roles
                List<DtoGeneric> roles = context.Roles.Select(s => new DtoGeneric()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();

                var userNet = context.Users.First(f => f.Id == UserInfo.Id);
                int idRol = userNet.Roles.First().RoleId;

                ViewBag.IdRol = new SelectList(roles.OrderBy(o => o.Name), "Id", "Name", idRol);

            }
            else
            {
                ViewBag.IdUser = new SelectList(null, "Id", "Name");
                ViewBag.IdRol = new SelectList(null, "Id", "Name");
            }
                       
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsersRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserInfo1 = context.Users.FirstOrDefault(f=>f.Id==model.IdUser);  
                //Quitamos el rol actual
                UserInfo1.Roles.Remove(UserInfo1.Roles.First());

                //Actualizamos rol de usuario
                CustomUserRole rolAdd = new CustomUserRole()
                {
                    UserId = model.IdUser,
                    RoleId = model.IdRol
                };
                UserInfo1.Roles.Add(rolAdd);


                context.SaveChanges();

                return RedirectToAction("Index");
            }

           

            var UserInfo = context.Users.FirstOrDefault();

            if (UserInfo != null)
            {
                //Usuarios
                List<DtoGeneric> users = context.Users.Select(s => new DtoGeneric()
                {
                    Id = s.Id,
                    Name = s.UserName
                }).ToList();
                ViewBag.IdUser = new SelectList(users.OrderBy(o => o.Name), "Id", "Name", UserInfo.Id);

                //Roles
                List<DtoGeneric> roles = context.Roles.Select(s => new DtoGeneric()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();

                var userNet = context.Users.First(f => f.Id == UserInfo.Id);
                int idRol = userNet.Roles.First().RoleId;

                ViewBag.IdRol = new SelectList(roles.OrderBy(o => o.Name), "Id", "Name", idRol);

            }
            else
            {
                ViewBag.IdUser = new SelectList(null, "Id", "Name");
                ViewBag.IdRol = new SelectList(null, "Id", "Name");
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetIdRol(int idUser)
        {
            var userNet = context.Users.First(f => f.Id == idUser);
            int idRol = userNet.Roles.First().RoleId;

            return Json(idRol, JsonRequestBehavior.AllowGet);

        }
    }
}