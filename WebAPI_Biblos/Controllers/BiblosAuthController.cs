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
    [RoutePrefix("api/biblosauth")]
    public class BiblosAuthController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();
        [HttpGet]
        [Route("todos")]
        public IHttpActionResult GetAll()
        {
            List<Autore> ooo = entidad.Autores.ToList();
            return Ok(ooo);
        }
        [HttpGet]
        [Route("autor")]
        public IHttpActionResult GetAutor(int id)
        {
            Autore ooo = entidad.Autores.Where(l => l.idAutor.Equals(id)).FirstOrDefault();
            return Ok(ooo);
        }

        [HttpGet]
        [Route("autorletra")]
        public IHttpActionResult GetAutoresLetra(string letra)
        {
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(letra.ToUpper())).OrderBy(o => o.NombreAutor).ToList();
            return Ok(ooo);
        }

        [HttpPost]
        [Route("autorletrap")]
        public IHttpActionResult PostAutoresLetra(cletra let)
        {
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(let.letra.ToUpper())).OrderBy(o => o.NombreAutor).ToList();
            return Ok(ooo);
        }

      
        AutoresL opera (int id, string nombre)
        {
            var cantidad = entidad.mlibs.Where(c => c.CodAutor == id).Count();
            var urls = entidad.Urls.Where(c => c.codigo_padre == id && c.tipo == 2).Count();
            return  new AutoresL(id, nombre, cantidad,urls);
        }

        [HttpPost]
        [Route("autorletralibros")]
        public IHttpActionResult PostAutorLetraLibros(cletra let)
        {
            List<AutoresL> auth = new List<AutoresL>();
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(let.letra.ToUpper())).OrderBy(o => o.NombreAutor).ToList();
            foreach (Autore a in ooo)
            {
                auth.Add(opera(a.idAutor, a.NombreAutor));
            }
            return Ok(auth);
        }

        

        [HttpPost]
        [Route("librosautor")]
        public IHttpActionResult PostLibrosAutor(cletra codigo)
        {
            int cod = 3603;
            int.TryParse(codigo.letra, out cod);
            List<mlib> ooo = entidad.mlibs.Where(l => l.CodAutor.Equals(cod)).ToList();
            return Ok(ooo);
        }

        [HttpPost]
        [Route("altaAutor")]
        public string PostInsertAutor(cletra let)
        {

            JObject o = JObject.Parse(let.letra);
            string nombre = (string)o["NombreAutor"];
            Autore autor = o.ToObject<Autore>();
            string codigo = "";

            Autore ooo = entidad.Autores.Where(l => l.idAutor.Equals(autor.idAutor)).FirstOrDefault();
            if (ooo == null)
            {
                Autore res = entidad.Autores.Add(autor);
                entidad.SaveChanges();
                codigo = res.idAutor.ToString();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Autore orig = (from x in entidad.Autores
                                 where x.idAutor == autor.idAutor
                                 select x).First();
                    Type mtype = orig.GetType(); PropertyInfo[] m_propiedades_orig = mtype.GetProperties();
                    Type mtype_def = autor.GetType(); PropertyInfo[] m_propiedades_def = mtype_def.GetProperties();

                    for (int i = 0; i < m_propiedades_orig.Count(); i++)
                    {
                        m_propiedades_orig[i].SetValue(orig, m_propiedades_def[i].GetValue(autor));

                    }

                    entidad.SaveChanges();
                    codigo = orig.idAutor.ToString();
                }

            }
            return codigo;
        }


    }
}
