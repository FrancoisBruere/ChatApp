using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Log
    {
        [Key]
        [Required]
        public int Log_Id { get; set; }
        public string Log_Level { get; set; }
        public string Event_Name { get; set; }
        public string Source { get; set; }
        public string Exception_Message { get; set; }
        public string Stack_Trace { get; set; }
        public string Created_date { get; set; }
    }
    
}
