using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace In_class_Linq_query_on_Azure.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }


    }
}
