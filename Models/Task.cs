using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Ticket
    {
        [Key]
        public int TaskID { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int AssignedUserID { get; set; }

    }
}
