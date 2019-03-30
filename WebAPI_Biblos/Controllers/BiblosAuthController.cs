using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace WebAPI_Biblos.Controllers
{
    [Authorize]
    [RoutePrefix("api/biblosauth")]
    public class BiblosAuthController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Autore> ooo = entidad.Autores.ToList();
            return Ok(ooo);
        }
        [HttpGet]
        public IHttpActionResult GetAutor(int id)
        {
            Autore ooo = entidad.Autores.Where(l => l.idAutor.Equals(id)).FirstOrDefault();
            return Ok(ooo);
        }

        [HttpGet]
        public IHttpActionResult GetAutoresLetra(string letra)
        {
            List<Autore> ooo = entidad.Autores.Where(l => l.NombreAutor.ToUpper().StartsWith(letra.ToUpper())).ToList();
            return Ok(ooo);
        }
    }
}
