using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class HomeController : Controller
    {

        li4_back_end_entities base_dados = new li4_back_end_entities();
        Utilizador user_actual;
        public ActionResult Index()
        {
            user_actual = new Utilizador();
            return View();
        }

        [HttpPost]
        public ActionResult Index( Utilizador user_corrente)
        {
            if (ModelState.IsValid)
            {
                Utilizador user_bd = base_dados.Utilizadors.Find(user_corrente.id_utlizador);
                if (user_bd != null)
                {
                    if (user_corrente.tipo == "admin" && user_bd.tipo == "admin" && user_corrente.password == user_bd.password)
                    {
                        Response.Cookies["userid"].Value = user_bd.id_utlizador.ToString();
                        return RedirectToActionPermanent("IndexAdmin", user_bd);
                    }
                    else
                    {
                        if (user_corrente.password == user_bd.password)
                        {
                            Response.Cookies["userid"].Value = user_bd.id_utlizador.ToString();
                            return RedirectToActionPermanent("IndexUser", user_bd);
                        }
                    }
                }
                else {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

                public ActionResult IndexAdmin(Utilizador user_corrente)
        {
            Utilizador user_bd = base_dados.Utilizadors.Find(user_corrente.id_utlizador);
            if (user_bd != null)
            {
                return View(user_corrente);
            }
            else return RedirectToAction("Index");
        }

        public ActionResult IndexUser(Utilizador user_corrente)
        {
            Utilizador user_bd = base_dados.Utilizadors.Find(int.Parse(Request.Cookies["userid"].Value));
            if (user_bd != null)
            {
                return View(user_corrente);
            }
            else return RedirectToAction("Index");
        }

       
    }

   

}