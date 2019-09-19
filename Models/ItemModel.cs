using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class ItemModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }

        public string ClientName { get; set; }

        public string SerialNumber { get; set; }

        public Nullable<DateTime> OccupiedDate { get; set; }
        public DateTime CreationDate { get; set; }
    }

}