//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI_Biblos
{
    using System;
    using System.Collections.Generic;
    
    public partial class mlib
    {
        public int idLibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public string coleccion { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string tema { get; set; }
        public Nullable<int> calificacion { get; set; }
        public Nullable<int> paginas { get; set; }
        public string comentario { get; set; }
        public string prestamo { get; set; }
        public Nullable<System.DateTime> fecha_prestamo { get; set; }
        public Nullable<int> numobras { get; set; }
        public string origen { get; set; }
        public int CodAutor { get; set; }
    }
}
