using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{

    public class EventoController : Controller
    {
        li4_back_end_entities base_dados = new li4_back_end_entities();
        int user_actual = -1;
        // GET: Evento

        [HttpGet]
        public ActionResult Index(int?  id)
        {
            if (id != null) { 
            Utilizador user_bd = base_dados.Utilizadors.Find(id);
            if (user_bd != null)
            {
                user_actual = (int)id;
                return View(base_dados.Eventoes.ToList());
            }
        }
        return RedirectToAction("Index", "Home");
        }


        public ActionResult Criar()
        {

             //   Utilizador user_bd = base_dados.Utilizadors.Find(user_actual);
         //       if (user_bd != null)
          //      {
                    Evento novo_evento = new Evento();
                    return View(novo_evento);
           //     }
            
       //     return RedirectToAction("Index", "Home" );
        }

        [HttpPost]
        public ActionResult Criar(Evento novo_evento)
        {
            if (ModelState.IsValid)
            {
                novo_evento.utilizador = user_actual;
                base_dados.Eventoes.Add(novo_evento);
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novo_evento);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Evento evento = base_dados.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        [HttpPost]
        public ActionResult Editar(Evento evento)
        {
            if (ModelState.IsValid)
            {
                base_dados.Entry(evento).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Evento evento = base_dados.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        [HttpGet]
        public ActionResult Detalhe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Evento evento = base_dados.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        [HttpPost]
        public ActionResult Detalhe(Evento evento)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(evento);
        }


        [HttpPost]
        public ActionResult Eliminar(int? id, Evento evento1)
        {
            Evento evento = new Evento();
            if (ModelState.IsValid)
            {
                evento = base_dados.Eventoes.Find(id);
                base_dados.Entry(evento).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }
    }
}