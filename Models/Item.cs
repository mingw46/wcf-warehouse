using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int ItemTypeID { get; set; }

        public virtual ItemType ItemType { get; set; }

        [Required(ErrorMessage = "Required" )]
        public string SerialNumber { get; set; }

        public Nullable<DateTime> OccupiedDate { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public int? ClientID { get; set; }
        public virtual Client Client { get; set; }

        
    }
}
