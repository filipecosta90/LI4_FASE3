using li4_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace li4_backend.Controllers
{
    public class SincronizacaoController : Controller
    {
        li4_back_end_entities base_dados = new li4_back_end_entities();

        // GET: Sincronizacao
        public ActionResult Index(int id)
        {
            var refugiados_locais = base_dados.Refugiadoes;


            using (CsvFileWriter writer = new CsvFileWriter("C:/Users/li4/Documents/refugiados_locais.csv"))
            {
                foreach (var i in refugiados_locais)
                {
                    CsvRow row = new CsvRow();
                    row.Add(id.ToString());
                    row.Add(i.id_refugiado.ToString());
                    row.Add(i.data_registo.ToString());
                    row.Add(i.nome);
                    row.Add(i.data_nascimento.ToString());
                    row.Add(i.genero);
                    row.Add(i.info_adicional);
                    row.Add(i.paradeiro_conhecido.ToString());

                    // row.Add(i.msg_voz.ToString());
                    //  row.Add(i.foto.ToString());
                    row.Add(i.local_registo_latitude);
                    row.Add(i.local_registo_longitude);
                    row.Add(i.localizacao_actual_latitude);
                    row.Add(i.localizacao_actual_longitude);

                    writer.WriteRow(row);
                }
            }
            return View();
        }


        public ActionResult Importar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Importar(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {


                    using (CsvFileReader reader = new CsvFileReader(upload.InputStream))
                    {
                        CsvRow row = new CsvRow();
                        while (reader.ReadRow(row))
                        {
                            Refugiado novo_refugiado = new Refugiado();
                            novo_refugiado.utilizador = int.Parse(row[0]);
                            novo_refugiado.id_refugiado = int.Parse(row[1]) + 1000;
                            novo_refugiado.data_registo = DateTime.Parse(row[2]);
                            novo_refugiado.nome = row[3];
                            novo_refugiado.paradeiro_conhecido = Byte.Parse(row[7]);
                            base_dados.Refugiadoes.Add(novo_refugiado);
                            base_dados.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Mensagem");
        }

        public ActionResult Mensagem()
        {
            return View();


        }
    }
}

