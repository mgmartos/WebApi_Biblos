using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_Biblos.Models;

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
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(letra.ToUpper())).ToList();
            return Ok(ooo);
        }

        [HttpPost]
        [Route("autorletrap")]
        public IHttpActionResult PostAutoresLetra(cletra let)
        {
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(let.letra.ToUpper())).ToList();
            return Ok(ooo);
        }

      
        AutoresL opera (int id, string nombre)
        {
            var cantidad = entidad.mlibs.Where(c => c.CodAutor == id).Count();
            return  new AutoresL(id, nombre, cantidad);
        }

        [HttpPost]
        [Route("autorletralibros")]
        public IHttpActionResult PostAutorLetraLibros(cletra let)
        {
            List<AutoresL> auth = new List<AutoresL>();
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(let.letra.ToUpper())).ToList();
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




    }
}
