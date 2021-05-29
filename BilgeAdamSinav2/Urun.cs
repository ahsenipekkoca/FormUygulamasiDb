using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamSinav2
{
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }

        [Required]
        [MaxLength(50)]
        public string UrunAdi { get; set; }

        public int StokMiktari { get; set; }
        public decimal BirimFiyati { get; set; }

        

    }


}
