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
    [Authorize]
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
        public IHttpActionResult PostLibrosLetra(cletra let)
        {

            List<mlib> ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().StartsWith(let.letra.ToUpper())).ToList();
            return Ok(ooo);
        }

        //[HttpPost]
        //[Route("LibrosAutor")]
        //public IHttpActionResult PostLibrosAutor(int codigo)
        //{

        //    List<mlib> ooo = entidad.mlibs.Where(l => l.CodAutor.Equals(codigo)).ToList();
        //    return Ok(ooo);
        //}


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

                    entidad.SaveChanges();
                    codigo = orig.idLibro.ToString();
                }

            }
            return codigo;
        }

        

    }
}
