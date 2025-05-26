using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace travelAdency.Models
{
    [Table(Name = "Туристы")]
    public class Turist
    {
        [Column(Name = "Код_туриста", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Имя")]
        public string FirstName { get; set; }
        [Column(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Column(Name = "Фамилия")]
        public string LastName { get; set; }
    }
}
