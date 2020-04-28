using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarmaTestCase.Dtos
{
    public class PersonDto
    {
        [HiddenInput]
        public string FileName { get; set; }
        public string Id_document { get; set; }
        public string phone { get; set; }
        public string alternative_id { get; set; }
        public string driving_license { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string sex { get; set; }
        public string education { get; set; }
        public string marital_status { get; set; }
        public string children { get; set; }
        public List<SelectListItem> ColumnNames { get; set; }
    }
}
