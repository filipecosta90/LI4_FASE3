using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class RefugiadoController : Controller
    {

        li4_back_end_entities base_dados = new li4_back_end_entities();

        // GET: Refugiado
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
            return View(base_dados.Refugiadoes.ToList());
        }
    }
}