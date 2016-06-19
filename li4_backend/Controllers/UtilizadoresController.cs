using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using li4_backend.Models;
using System.Net;

namespace li4_backend.Controllers
{
    public class UtilizadoresController : Controller
    {
        li4_back_end_entities base_dados = new li4_back_end_entities();
        public ActionResult Index()
        {
            ViewBag.user_admin = "1";
            return View( base_dados.Utilizadors.ToList() );
        }
        [HttpGet]
        public ActionResult Criar() {
            Utilizador novo_user = new Utilizador();

            var maxValue = 0;
            if (base_dados.Utilizadors.Count() > 0)
            {
                maxValue = base_dados.Utilizadors.Max(x => x.id_utlizador);
            }

            novo_user.id_utlizador = maxValue + 1;
            return View(novo_user);
      }

        [HttpPost]
        public ActionResult Criar(Utilizador novo_user) {
            if (ModelState.IsValid) {
                base_dados.Utilizadors.Add(novo_user);
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novo_user);
         }

        [HttpGet]
        public ActionResult Editar( int? id )
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Utilizador user = base_dados.Utilizadors.Find(id);
            if (user == null){
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Editar(Utilizador user )
        {
            if (ModelState.IsValid)
            {
                base_dados.Entry(user).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Utilizador user = base_dados.Utilizadors.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Eliminar(int? id, Utilizador user1)
        {
            Utilizador user = new Utilizador();
            if (ModelState.IsValid)
            {
                user = base_dados.Utilizadors.Find(id);
                base_dados.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}
