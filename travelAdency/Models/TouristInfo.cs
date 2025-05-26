using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAdency.Models
{
    [Table(Name = "Информация о туристах")]
    public class TouristInfo
    {
        [Column(Name = "Код_туриста")]
        public int TouristId { get; set; }

        [Column(Name = "Серия_паспорта")]
        public string PassportSeries { get; set; }

        [Column(Name = "Город")]
        public string City { get; set; }

        [Column(Name = "Страна")]
        public string Country { get; set; }

        [Column(Name = "Телефон")]
        public string Phone { get; set; }

        [Column(Name = "Индекс")]
        public string PostalCode { get; set; }
    }
}
