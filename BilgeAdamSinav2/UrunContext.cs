using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamSinav2
{
    public class UrunContext : DbContext
    {
        public UrunContext()
        {
            Database.Connection.ConnectionString = "server=. ; database =UrunDbAhsen; uid=sa; pwd=123";
        }

        public DbSet <Urun> Urunler { get; set; }
    }
}
