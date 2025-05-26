using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelAdency.Models
{
    [Table(Name = "Оплата")]
    public class Payment
    {
        [Column(Name = "Код_оплаты", IsPrimaryKey = true, IsDbGenerated = true)]
        public int PaymentId { get; set; }

        [Column(Name = "Код_путёвки")]
        public int TourId { get; set; }

        [Column(Name = "Дата_оплаты")]
        public DateTime PaymentDate { get; set; }

        [Column(Name = "Сумма")]
        public decimal Amount { get; set; }
    }
}
