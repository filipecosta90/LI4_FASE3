using li4_backend.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class EventoController : Controller
    {
        li4_back_end_entities base_dados = new li4_back_end_entities();
        public static int user_actual = 0;
        // GET: Evento

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id == null && Request.Cookies["userid"] == null) {
                return RedirectToAction("Index", "Home");
            }
            if (id == null) {
                ViewBag.id_user = int.Parse(Request.Cookies["userid"].Value);
            }
            else { 
                ViewBag.id_user = (int)id;
            }
            ViewBag.user_vol = "1";
            return View(base_dados.Eventoes.ToList());
        }

        [HttpGet]
        public ActionResult Criar(int? id)
        {
            if (id != null)
            {
                Utilizador user_bd = base_dados.Utilizadors.Find(id);
                if (user_bd != null)
                {
                    var maxValue = 0;
                    if (base_dados.Eventoes.Count() > 0)
                    {
                        maxValue = base_dados.Eventoes.Max(x => x.id_evento);
                    }
                    ViewBag.id_user = (int)id;
                    Evento novo_evento = new Evento();
                    novo_evento.id_evento = maxValue + 1;
                    novo_evento.utilizador = (int) id;
                    return View(novo_evento);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Evento novo_evento, HttpPostedFileBase upload)
       {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        novo_evento.foto = reader.ReadBytes(upload.ContentLength);
                    }
                }
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
        public ActionResult Editar(Evento evento, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        evento.foto = reader.ReadBytes(upload.ContentLength);
                    }
                }
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

        [HttpPost]
        public ActionResult Eliminar(int? id, Evento evento1)
        {
            Evento evento = new Evento();
            if (ModelState.IsValid)
            {
                evento = base_dados.Eventoes.Find(id);
                base_dados.Entry(evento).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index", "Evento");
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

        public async Task<ActionResult> RenderImage(int id)
        {
            Evento evento =  base_dados.Eventoes.Find(id);
            byte[] photoBack = evento.foto;
            return File(photoBack, "image/png");
        }


    }
}