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
                if (user_bd != null) { 
                if (user_corrente.tipo == "admin" && user_bd.tipo == "admin" && user_corrente.password == user_bd.password )
                {
                    return RedirectToAction("IndexAdmin" , user_corrente);
                }
                else 
                {
                        if (user_corrente.password == user_bd.password ) { 
                    return RedirectToAction("IndexUser", user_corrente);
                }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult IndexAdmin(Utilizador user_corrente)
        {
            return View();
        }

        public ActionResult IndexUser(Utilizador user_corrente)
        {
            return View();
        }



    }
}