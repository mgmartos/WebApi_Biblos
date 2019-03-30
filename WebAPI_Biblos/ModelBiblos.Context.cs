﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class biblosEntities1 : DbContext
    {
        public biblosEntities1()
            : base("name=biblosEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Autore> Autores { get; set; }
        public virtual DbSet<mlib> mlibs { get; set; }
        public virtual DbSet<Url> Urls { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    
        public virtual ObjectResult<SelectAutoresLetra_Result> SelectAutoresLetra(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectAutoresLetra_Result>("SelectAutoresLetra", nombreParameter);
        }
    
        public virtual ObjectResult<SelectLibrosPorLetra_Result> SelectLibrosPorLetra(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectLibrosPorLetra_Result>("SelectLibrosPorLetra", nombreParameter);
        }
    }
}
