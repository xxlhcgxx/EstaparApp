using System;
using System.ComponentModel.DataAnnotations;

namespace EstaparApp.Models
{
    public class Modelo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Modelo")] 
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Marca")] 
        public long MarcaId { get; set; }

        [Display(Name = "Marca")] 
        public virtual Marca Marca { get; set; }

    }
}
