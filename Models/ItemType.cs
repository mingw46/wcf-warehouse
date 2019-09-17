using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class ItemType
    {
        [Key]
        public int ItemTypeID { get; set; }
        
        [Required]
        public string ItemCode { get; set; }

        [Required(ErrorMessage = "Required")]
        public string CountryCode { get; set; }

        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
