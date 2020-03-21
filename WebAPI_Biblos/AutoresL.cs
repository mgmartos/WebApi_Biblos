

namespace WebAPI_Biblos
{
    public partial class AutoresL : WebAPI_Biblos.Autore
    {
        public int Cantidad { get; set; }
        public AutoresL(int id, string n, int c)
        {
            this.idAutor = id;
            this.NombreAutor = n;
            this.Cantidad = c;
        }
    }
}