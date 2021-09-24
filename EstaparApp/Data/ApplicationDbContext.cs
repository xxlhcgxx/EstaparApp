using EstaparApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstaparApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Manobrista> Manobrista { get; set; }
        public DbSet<Registro> Registro { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESENV300\SQLEXPRESS;Initial Catalog=EstaparDataBase;Integrated Security=True;");
        }
    }
}
