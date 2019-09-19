using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Required.")]
        public Country CountryName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Item> Items { get; set; }
        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public enum Country
        {
            Poland = 0,
            England = 1,
            Germany = 2
        }
    }
}