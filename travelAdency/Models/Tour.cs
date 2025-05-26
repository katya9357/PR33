using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAdency.Models
{
    [Table(Name = "Туры")]
    public class Tour
    {
        [Column(Name = "Код_тура", IsPrimaryKey = true, IsDbGenerated = true)]
        public int TourId { get; set; }

        [Column(Name = "Название")]
        public string Name { get; set; }

        [Column(Name = "Цена")]
        public decimal Price { get; set; }

        [Column(Name = "Информация")]
        public string Information { get; set; }
    }
}
