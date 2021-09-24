using System;
using System.ComponentModel.DataAnnotations;

namespace EstaparApp.Models
{
    public class Marca
    {
        [Key]
        [Display(Name = "Marca")] 
        public long Id { get; set; }

        [Required]
        [Display(Name = "Marca")] 
        public string Nome { get; set; }
    }
}
