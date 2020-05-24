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
   [RoutePrefix("api/varios")]
  public class editorialesController : ApiController
    {
        biblosEntities1 entidad = new biblosEntities1();

        [HttpGet]
        [Route("todas")]
        public IHttpActionResult GetAll()
        {
            IEnumerable<string> ooo = entidad.mlibs.Select(l => l.editorial.Trim()).Distinct().OrderBy(s => s).ToList();
            return Ok(ooo);
        }

        [HttpGet]
        [Route("librosedit")]
        public IHttpActionResult GetLibrosEditorial(string editorial, string orden="")
        {
            List<mlib> libros;
            if (string.IsNullOrEmpty(orden) || (!orden.Equals("autor")))
            {
                libros = (from l in entidad.mlibs where l.editorial.ToUpper().Trim() == editorial.ToUpper().Trim() orderby l.titulo select l).ToList();
            }
            else
            {
                libros = (from l in entidad.mlibs where l.editorial.ToUpper().Trim() == editorial.ToUpper().Trim() orderby l.autor select l).ToList();
            }
            return Ok(libros);
        }

        [HttpGet]
        [Route("temas")]
        public IHttpActionResult GetAllTemas()
        {
            IEnumerable<string> ooo = entidad.mlibs.Select(l => l.tema.Trim()).Distinct().OrderBy(s => s).ToList();
            return Ok(ooo);
        }

        [HttpGet]
        [Route("librostema")]
        public IHttpActionResult GetLibrosTema(string tema, string orden = "")
        {
            List<mlib> libros;
            if (string.IsNullOrEmpty(orden) || (!orden.Equals("autor")))
            {
                libros = (from l in entidad.mlibs where l.tema.ToUpper().Trim() == tema.ToUpper().Trim() orderby l.titulo select l).ToList();
            }
            else
            {
                libros = (from l in entidad.mlibs where l.tema.ToUpper().Trim() == tema.ToUpper().Trim() orderby l.autor select l).ToList();
            }
            return Ok(libros);
        }

    }
}
