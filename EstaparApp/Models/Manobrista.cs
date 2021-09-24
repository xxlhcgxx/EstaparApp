using System;
using System.ComponentModel.DataAnnotations;

namespace EstaparApp.Models
{
    public class Manobrista
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nome")] 
        public string Nome { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Data de Nascimento")] 
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Cpf")]
        public String Cpf { get; set; }

    }
}
