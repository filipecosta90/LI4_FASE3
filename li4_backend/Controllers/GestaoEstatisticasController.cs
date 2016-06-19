using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class GestaoEstatisticasController : Controller
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
            ViewBag.user_admin = "1";
            ViewBag.numero_estatisticas = base_dados.Estatisticas.Count();
            ViewBag.numero_intervencoes_imediatas = base_dados.Estatisticas.Count(model => model.intervencao_imediata == 1);
            ViewBag.media_fome = base_dados.Estatisticas.Average(model => model.fome);
            ViewBag.media_perigo = base_dados.Estatisticas.Average(model => model.perigo);
            ViewBag.media_esforco = base_dados.Estatisticas.Average(model => model.esforco);
            ViewBag.media_terceiros = base_dados.Estatisticas.Average(model => model.terceiros);
            ViewBag.media_autonomia = base_dados.Estatisticas.Average(model => model.autonomia);
            ViewBag.media_saude = base_dados.Estatisticas.Average(model => model.saude);
            return View(base_dados.Estatisticas.ToList());
        }
    }
}