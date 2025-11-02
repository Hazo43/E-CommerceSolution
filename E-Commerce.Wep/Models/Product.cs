using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Wep.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Name IS Required")]
        [MaxLength (3 , ErrorMessage = "Invalid Length ")]
        public string Name { get; set; } 
    }
}
