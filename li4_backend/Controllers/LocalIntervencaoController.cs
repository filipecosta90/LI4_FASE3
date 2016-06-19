using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class LocalIntervencaoController : Controller
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
            ViewBag.numero_locais = base_dados.LocalInteresses.Count();
            return View(base_dados.LocalInteresses.ToList());
        }

        [HttpGet]
        public ActionResult Criar(int? id)
        {
            if (ModelState.IsValid)
            {
                LocalInteresse local = base_dados.LocalInteresses.Find(id);
                RegistoCampo novo_registo_campo = new RegistoCampo();

                var maxValue = 0;
                if (base_dados.RegistoCampoes.Count() > 0)
                {
                    maxValue = base_dados.RegistoCampoes.Max(x => x.idRegistoCampo);
                }
                novo_registo_campo.idRegistoCampo = maxValue + 1;
                novo_registo_campo.data = DateTime.Now;
                Utilizador user_bd = base_dados.Utilizadors.Find(int.Parse(Request.Cookies["userid"].Value));
                user_bd.RegistoCampo_idRegistoCampo = novo_registo_campo.idRegistoCampo;
                local.Utilizadors.Add(user_bd);
                local.RegistoCampoes.Add(novo_registo_campo);
                base_dados.RegistoCampoes.Add(novo_registo_campo);
                base_dados.SaveChanges();
                base_dados.Entry(user_bd).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                base_dados.Entry(local).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
            }
            return RedirectToAction("IndexUser", "Home", new { id = ViewBag.id_user });
        }

    }
}