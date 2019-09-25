using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("User")]
        public int AssignedUserID { get; set; }
        public virtual User User { get; set; }

        public int ItemID { get; set; }
        public virtual Item Item { get; set; }


    }
}
