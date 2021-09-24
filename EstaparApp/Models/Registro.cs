using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace EstaparApp.Models
{
    public class Registro
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Data de Entrada")]
        public DateTime Entrada { get; set; }

        [Required]
        [Display(Name = "Hora de Entrada")]
        public TimeSpan HoraEntrada { get; set; }

        [Display(Name = "Data de Saída")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Saida { get; set; }

        [Display(Name = "Hora de Saída")]
        public TimeSpan? HoraSaida { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public long MarcaId { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public long ModeloId { get; set; }

        [Display(Name = "Modelo")] 
        public string Modelo { get; set; }

        [Required]
        [Display(Name = "Manobrista")] 
        public long ManobristaId { get; set; }

        [Display(Name = "Manobrista")]
        public string Manobrista { get; set; }

        [Required]
        [StringLength(8)]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Display(Name = "Manobrado")] 
        public bool Manobrado { get; set; }

    }
}
