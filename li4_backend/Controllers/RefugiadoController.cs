using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ActionResult Criar(int? id)
        {
            if (id != null)
            {
                Utilizador user_bd = base_dados.Utilizadors.Find(id);
                if (user_bd != null)
                {
                    ViewBag.id_user = (int)id;
                    Refugiado novo_refugiado = new Refugiado();
                    novo_refugiado.utilizador = (int)id;
                    return View(novo_refugiado);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Refugiado novo_refugiado, HttpPostedFileBase upload_msg_voz, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        novo_refugiado.foto = reader.ReadBytes(upload.ContentLength);
                    }
                }
                if (upload_msg_voz != null && upload_msg_voz.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload_msg_voz.InputStream))
                    {
                        novo_refugiado.msg_voz = reader.ReadBytes(upload_msg_voz.ContentLength);
                    }
                }
                base_dados.Refugiadoes.Add(novo_refugiado);
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novo_refugiado);
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Refugiado refugiado = base_dados.Refugiadoes.Find(id);
            if (refugiado == null)
            {
                return HttpNotFound();
            }
            return View(refugiado);
        }

        [HttpPost]
        public ActionResult Editar(Refugiado refugiado, HttpPostedFileBase upload_msg_voz, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        refugiado.foto = reader.ReadBytes(upload.ContentLength);
                    }
                }
                if (upload_msg_voz != null && upload_msg_voz.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload_msg_voz.InputStream))
                    {
                        refugiado.msg_voz = reader.ReadBytes(upload_msg_voz.ContentLength);
                    }
                }
                base_dados.Entry(refugiado).State = System.Data.Entity.EntityState.Modified;
                base_dados.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(refugiado);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Refugiado refugiado = base_dados.Refugiadoes.Find(id);
            if (refugiado == null)
            {
                return HttpNotFound();
            }
            return View(refugiado);
        }

        [HttpPost]
        public ActionResult Eliminar(int? id, Refugiado refugiado1)
        {
            Refugiado refugiado = new Refugiado();
            if (ModelState.IsValid)
            {
                refugiado = base_dados.Refugiadoes.Find(id);
                base_dados.Entry(refugiado).State = System.Data.Entity.EntityState.Deleted;
                base_dados.SaveChanges();
                return RedirectToAction("Index", "Refugiado");
            }
            return View(refugiado);
        }

        [HttpGet]
        public ActionResult Detalhe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Refugiado refugiado = base_dados.Refugiadoes.Find(id);
            if (refugiado == null)
            {
                return HttpNotFound();
            }
            return View(refugiado);
        }

        [HttpPost]
        public ActionResult Detalhe(Refugiado refugiado)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(refugiado);
        }

        public async Task<ActionResult> RenderImage(int id)
        {
            Refugiado refugiado = base_dados.Refugiadoes.Find(id);
            byte[] photoBack = refugiado.foto;
            return File(photoBack, "image/png");
        }

        public async Task<ActionResult> RenderAudio(int id)
        {
            Refugiado refugiado = base_dados.Refugiadoes.Find(id);
            byte[] audioBack = refugiado.msg_voz;
            return File(audioBack, "audio/mp3");
        }

    }
}