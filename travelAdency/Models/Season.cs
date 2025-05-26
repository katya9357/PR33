using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAdency.Models
{
    [Table(Name = "Сезоны")]
    public class Season
    {
        [Column(Name = "Код_сезона", IsPrimaryKey = true, IsDbGenerated = true)]
        public int SeasonId { get; set; }

        [Column(Name = "Код_тура")]
        public int TourId { get; set; }

        [Column(Name = "Дата_начала")]
        public DateTime StartDate { get; set; }

        [Column(Name = "Дата_конца")]
        public DateTime EndDate { get; set; }

        [Column(Name = "Сезон_закрыт")]
        public bool IsClosed { get; set; }

        [Column(Name = "Количество_мест")]
        public int AvailableSeats { get; set; }
    }
}
