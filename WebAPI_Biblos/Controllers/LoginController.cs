using System;
using System.Net;
using System.Web.Http;
using System.Threading;
using WebAPI_Biblos.Models;
using System.Collections.Generic;
using System.Linq;



namespace WebAPI_Biblos.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            biblosEntities1 entidad = new biblosEntities1();
            Cifra cifrador = new Cifra();
            string pass = cifrador.Cifrar(login.PassWord);
            string nopass = cifrador.Descifrar(pass);
            var usu = entidad.Usuarios.Where(u => u.Nombre.Equals(login.UserName)).FirstOrDefault();
            if ((pass != usu.Password) || (usu.FechaAutorizado < DateTime.Today))
            {
                return Unauthorized();
            }
            var token = TokenGenerator.GenerateTokenJwt(login.UserName);
                return Ok(token);


            //TODO: Validate credentials Correctly, this code is only for demo !!
            //bool isCredentialValid = (login.PassWord == "123456");
            //if (isCredentialValid)
            //{
            //    var token = TokenGenerator.GenerateTokenJwt(login.UserName);
            //    return Ok(token);
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }
    }

}
