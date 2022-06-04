using CERNA_T3.Mapeo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CERNA_T3.Models
{
    public class Configuracion : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<Raza> Razas { get; set; }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public Configuracion(DbContextOptions<Configuracion> o) : base(o) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new SexoMap());
            modelBuilder.ApplyConfiguration(new RazaMap());
            modelBuilder.ApplyConfiguration(new EspecieMap());
            modelBuilder.ApplyConfiguration(new HistoriaMap());
        }
    }
}
