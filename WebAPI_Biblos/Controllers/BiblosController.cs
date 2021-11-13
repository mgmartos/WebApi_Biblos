using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Biblos.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace WebAPI_Biblos.Controllers
{
    //[Authorize]
    [RoutePrefix("api/biblos")]
    public class BiblosController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();
        [HttpGet]
        [Route("libro")]
        public IHttpActionResult GetLibro(int id)
        {
            mlib ooo = entidad.mlibs.Where(l => l.idLibro.Equals(id)).FirstOrDefault();
            return Ok(ooo);
        }

        [HttpGet]
        [Route("todos")]
        public IHttpActionResult GetAll()
        {
           // List<mlib> ooo = entidad.mlibs.ToList();
            IEnumerable<mlib> oooo = entidad.mlibs.ToList();

            return Ok(oooo);
        }

       

        [HttpPost]
        [Route("LibrosLetra")]
        public IHttpActionResult PostLibrosLetra(cletra letr)
        {
            List<mlib> ooo; 
            
            if (letr.letra.ToUpper().CompareTo("A") < 0  || (letr.letra.ToUpper().CompareTo("ZZZZZZZZZZZ") > 0))
            {
                ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().CompareTo("A") < 0 || l.titulo.ToUpper().CompareTo("ZZZZZZZZZZZZ") > 0).OrderBy(l=>l.titulo).ToList();
            }
            else
            {
            ooo = (from book in entidad.mlibs
                                 where book.titulo.ToUpper().StartsWith(letr.letra.ToUpper())
                                    select book).OrderBy(l => l.titulo).ToList();
            }

            //var ql = from c in entidad.mlibs join o in entidad.Urls on c.idLibro equals o.codigo_padre into url
            //         where c.titulo.StartsWith(letr.letra.ToUpper())
            //         select new { c, urls = url.Count() };

            var ql = from c in ooo join o in entidad.Urls on c.idLibro equals o.codigo_padre into url
                     //where c.titulo.StartsWith(letr.letra.ToUpper())                    
                     select new { c, urls = url.Where(u=>u.tipo==1).Count()};
            return Ok(ql);

            //var q =from c in entidad.mlibs join o in entidad.Urls on c.idLibro equals o.codigo_padre  into url select new { c, urls = url.Count() };
            ////return Ok(q)
            ///
            //ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().StartsWith(letr.letra.ToUpper())).ToList();
            //var tt = entidad.Database.ExecuteSqlCommand("Select * from mlib where paginas > 200");

            //return Ok(ooo);
        }

        [HttpPost]
        [Route("LibrosLetraUrls")]
        public IHttpActionResult PostLibrosLetraurls(cletra letr)
        {
            List<mlib> ooo;
            if (letr.letra.ToUpper().CompareTo("A") < 0 || (letr.letra.ToUpper().CompareTo("ZZZZZZZZZZZ") > 0))
            {
                ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().CompareTo("A") < 0 || l.titulo.ToUpper().CompareTo("ZZZZZZZZZZZZ") > 0).ToList();
                return Ok(ooo);
            }
            //ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().StartsWith(letr.letra.ToUpper())).ToList();

            ooo = (from book in entidad.mlibs
                   where book.titulo.ToUpper().StartsWith(letr.letra.ToUpper())
                   select book).ToList();

            //var tt = entidad.Database.ExecuteSqlCommand("Select * from mlib where paginas > 200");


            var q = from c in entidad.mlibs join o in entidad.Urls on c.idLibro equals o.codigo_padre into url select new { c, urls = url.Count() };
            return Ok(q);

            //return Ok(ooo);
        }

        [HttpGet]
        [Route("temas")]
        public IHttpActionResult Get()
        {
            IEnumerable<string> ltemas = entidad.mlibs.Select(l => l.tema.Trim()).Distinct().OrderBy(s => s).ToList();
            return Ok(ltemas);
        }

        [HttpGet]
        [Route("inicio")]
        public IHttpActionResult GetCantidades()
        {
            var ListaCantidades = new List<string>();
            ListaCantidades.Add(entidad.mlibs.Count().ToString()); 
            ListaCantidades.Add(entidad.Autores.Count().ToString());
            ListaCantidades.Add(entidad.mlibs.Select(l => l.editorial.Trim()).Distinct().OrderBy(s => s).ToList().Count.ToString());
            IEnumerable<string> cantidades = ListaCantidades;
            return Ok(cantidades);            
        }


        [HttpPost]
        [Route("altalibro")]
        public string PostInsertLibro(cletra let)
        {
          
            JObject o = JObject.Parse(let.letra);
            string titulo = (string)o["titulo"];
            mlib libro = o.ToObject<mlib>();
            string codigo = "";

            mlib ooo = entidad.mlibs.Where(l => l.idLibro.Equals(libro.idLibro)).FirstOrDefault();
            if (ooo == null)
            { 
                mlib res = entidad.mlibs.Add(libro);    
                entidad.SaveChanges();
                codigo = res.idLibro.ToString();        
            }
            else
            {
                if (ModelState.IsValid)
                {
                    mlib orig = (from x in entidad.mlibs
                                 where x.idLibro == libro.idLibro
                                 select x).First();
                    Type mtype = orig.GetType(); PropertyInfo[] m_propiedades_orig = mtype.GetProperties();
                    Type mtype_def = libro.GetType(); PropertyInfo[] m_propiedades_def = mtype_def.GetProperties();

                    for (int i = 0; i < m_propiedades_orig.Count(); i++)
                    {
                        m_propiedades_orig[i].SetValue(orig, m_propiedades_def[i].GetValue(libro));

                    }
                    try
                    { 
                    entidad.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        string error = ex.Message;
                    }
                    codigo = orig.idLibro.ToString();
                }

            }
            return codigo;
        }

        [HttpPost]
        [Route("altalink")]
        public string PostInsertLink(cletra let)
        {

            JObject o = JObject.Parse(let.letra);
            Url link = o.ToObject<Url>();     
            Url res = entidad.Urls.Add(link);
            entidad.SaveChanges();
            string dir = res.direccion.ToString();
            return dir;
        }

        [HttpGet]
        [Route("enlaces")]
        public IHttpActionResult GetEnlaces(int tipo, int padre)
        {

            var link = from l in entidad.Urls where l.tipo == tipo && l.codigo_padre == padre select l;
            return Ok(link);
        }

        [HttpGet]
        [Route("buscar")]
        public IHttpActionResult BuscarLibros(string titulo, string autor)
        {
            List<mlib> ooo;
            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor))
            {
            ooo = (from book in entidad.mlibs
                   where ((book.titulo.ToUpper().Contains(titulo.ToUpper())) || (book.autor.ToUpper().Contains(autor.ToUpper())))
                   select book).ToList();
            }
            else
            {
                ooo = (from book in entidad.mlibs
                       where ((book.titulo.ToUpper().Contains(titulo.ToUpper())) && (book.autor.ToUpper().Contains(autor.ToUpper())))
                       select book).ToList();
            }
            return Ok(ooo);
        }




        }


    }
