using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class EstatisticaController : Controller
    {
        li4_back_end_entities base_dados = new li4_back_end_entities();

        // GET: Estatistica
        public ActionResult Index(int? id)
        {
            if (id == null && Request.Cookies["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                ViewBag.id_user = int.Parse(Request.Cookies["userid"].Value);
            }
            else
            {
                ViewBag.id_user = (int)id;
            }
            ViewBag.user_vol = "1";
            return View(base_dados.Estatisticas.ToList());
        }

        [HttpGet]
        public ActionResult Criar(int? id)
        {
            if (id != null)
            {
                Utilizador user_bd = base_dados.Utilizadors.Find(id);
                if (user_bd != null)
                {
                    ViewBag.id_user = (int)id;
                    Estatistica nova_estatistica = new Estatistica();
                    var maxValue = 0;
                    if (base_dados.Estatisticas.Count() > 0)
                    {
                        maxValue = base_dados.Estatisticas.Max(x => x.id_estatistica);
                    }

                    nova_estatistica.id_estatistica = maxValue + 1;
                    nova_estatistica.utilizador = (int)id;
                    return View(nova_estatistica);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Estatistica nova_estatistica)
        {
            if (ModelState.IsValid)
            {
                base_dados.Estatisticas.Add(nova_estatistica);
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nova_estatistica);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Estatistica estatistica = base_dados.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            return View(estatistica);
        }

        [HttpPost]
        public ActionResult Editar(int? id, Estatistica estatistica)
        {
            if (ModelState.IsValid)
            {

                base_dados.Entry(estatistica).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estatistica);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Estatistica estatistica = base_dados.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            return View(estatistica);
        }

        [HttpPost]
        public ActionResult Eliminar(int? id, Estatistica estatistica1)
        {
            Estatistica estatistica = new Estatistica();
            if (ModelState.IsValid)
            {
                estatistica = base_dados.Estatisticas.Find(id);
                base_dados.Entry(estatistica).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index", "Estatistica");
            }
            return View(estatistica);
        }

        [HttpGet]
        public ActionResult Detalhe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Estatistica estatistica = base_dados.Estatisticas.Find(id);
            if (estatistica == null)
            {
                return HttpNotFound();
            }
            return View(estatistica);
        }

        [HttpPost]
        public ActionResult Detalhe(Estatistica estatistica)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(estatistica);
        }


    }

}