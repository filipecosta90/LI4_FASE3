using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using li4_backend.Models;
using System.Net;

namespace li4_backend.Controllers
{
    public class LocalInteresseController : Controller
    {
         li4_back_end_entities base_dados = new li4_back_end_entities();

        // GET: LocalIntervencao
        public ActionResult Index()
        {
            return View(base_dados.LocalInteresses.ToList());
        }

        [HttpGet]
        public ActionResult Criar()
        {
            LocalInteresse novo_local = new LocalInteresse();
            var maxValue = base_dados.LocalInteresses.Max(x => x.id_local_interesse);
            novo_local.id_local_interesse = maxValue + 1;
            return View(novo_local);
        }

        [HttpPost]
        public ActionResult Criar(LocalInteresse novo_local)
        {
            if (ModelState.IsValid)
            {
                base_dados.LocalInteresses.Add(novo_local);
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novo_local);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            LocalInteresse local = base_dados.LocalInteresses.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        [HttpPost]
        public ActionResult Editar(LocalInteresse local)
        {
            if (ModelState.IsValid)
            {
                base_dados.Entry(local).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(local);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            LocalInteresse local = base_dados.LocalInteresses.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        [HttpGet]
        public ActionResult Detalhe(int? id)
        {
            if (id == null) { 
                return RedirectToAction("Index");
            }
            LocalInteresse local = base_dados.LocalInteresses.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            return View(local);
        }

        [HttpPost]
        public ActionResult Detalhe(LocalInteresse local)
        {
            if (ModelState.IsValid)
            {
               return RedirectToAction("Index");
            }
            return View(local);
        }


        [HttpPost]
        public ActionResult Eliminar(int? id, LocalInteresse local1)
        {
            LocalInteresse local = new LocalInteresse();
            if (ModelState.IsValid)
            {
                local = base_dados.LocalInteresses.Find(id);
                base_dados.Entry(local).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(local);
        }
    }
}