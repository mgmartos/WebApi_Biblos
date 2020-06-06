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
    [RoutePrefix("api/lecturas")]
    public class LecturasController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();

        [HttpGet]
        [Route("todos")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<Lectura> oooo = entidad.Lecturas.ToList();
            return Ok(oooo.OrderBy(f =>f.fecha));
        }

        [HttpPost]
        [Route("Getlectura")]
        public IHttpActionResult PostLectura(cletra let)
        {
            string tit = let.letra;
            Lectura ooo = entidad.Lecturas.Where(l => l.titulo.Contains(tit)).FirstOrDefault();
            return Ok(ooo);
        }




        [HttpPost]
        [Route("altalectura")]
        public string PostInsertLectura(cletra let)
        {
            string codigo = "";
            JObject o = JObject.Parse(let.letra);
            string titulo = (string)o["titulo"];
            Lectura leido = o.ToObject<Lectura>();


            Lectura ooo = entidad.Lecturas.Where(l => l.titulo.Equals(leido.titulo)).FirstOrDefault();
            if (ooo == null)
            {
                Lectura res = entidad.Lecturas.Add(leido);
                entidad.SaveChanges();
                codigo = res.titulo.ToString();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Lectura orig = (from x in entidad.Lecturas
                                    where x.titulo == leido.titulo
                                    select x).First();
                    Type mtype = orig.GetType(); PropertyInfo[] m_propiedades_orig = mtype.GetProperties();
                    Type mtype_def = leido.GetType(); PropertyInfo[] m_propiedades_def = mtype_def.GetProperties();

                    for (int i = 0; i < m_propiedades_orig.Count(); i++)
                    {
                        m_propiedades_orig[i].SetValue(orig, m_propiedades_def[i].GetValue(leido));

                    }

                    entidad.SaveChanges();
                    codigo = orig.titulo.ToString();
                }
            }
            return codigo;
        }
    }
}
