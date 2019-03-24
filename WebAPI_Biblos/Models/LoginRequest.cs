using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI_Biblos.Models
{
    public class LoginRequest
    {
        public string UserName { get; set;}
        public string PassWord { get; set;}
    }
}