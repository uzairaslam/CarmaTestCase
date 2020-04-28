using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarmaTestCase.Models
{
    public class Person
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Id_document { get; set; }
        [MaxLength(50)]
        public string phone { get; set; }
        [MaxLength(50)]
        public string alternative_id { get; set; }
        [MaxLength(50)]
        public string driving_license { get; set; }
        [MaxLength(50)]
        public string first_name { get; set; }
        [MaxLength(50)]
        public string last_name { get; set; }
        [MaxLength(20)]
        public string sex { get; set; }
        [MaxLength(50)]
        public string education { get; set; }
        [MaxLength(50)]
        public string marital_status { get; set; }
        [MaxLength(20)]
        public string children { get; set; }
    }
}
