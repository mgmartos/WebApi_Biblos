

namespace WebAPI_Biblos
{
    public partial class AutoresL : WebAPI_Biblos.Autore
    {
        public int Cantidad { get; set; }
        public int Urls { get; set; }
        public AutoresL(int id, string n, int c, int u)
        {
            this.idAutor = id;
            this.NombreAutor = n;
            this.Cantidad = c;
            this.Urls = u;
        }
    }
}