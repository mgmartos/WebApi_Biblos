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
    [RoutePrefix("api/biblos")]
    public class BiblosController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();
        [HttpGet]
        public IHttpActionResult GetLibro(int id)
        {
            mlib ooo = entidad.mlibs.Where(l => l.idLibro.Equals(id)).FirstOrDefault();
            return Ok(ooo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<mlib> ooo = entidad.mlibs.ToList();
            return Ok(ooo);
        }

        [HttpGet]
        public IHttpActionResult GetLibrosLetra(string letra)
        {
            List<mlib> ooo = entidad.mlibs.Where(l => l.titulo.ToUpper().StartsWith(letra.ToUpper())).ToList();
            return Ok(ooo);
        }
        [HttpGet]
        [Route("temas")]
        public IHttpActionResult Get()
        {
            IEnumerable<string> ltemas = entidad.mlibs.Select(l => l.tema.Trim()).Distinct().OrderBy(s => s).ToList();
            return Ok(ltemas);
        }
        //Marca Local II

    }
}
