using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NinnoFeliz.Models;



#nullable disable



namespace NinnoFeliz.Data
{
    public class NinnoFelizContext : DbContext
    {
        public NinnoFelizContext()
        {
        }



        public NinnoFelizContext(DbContextOptions<NinnoFelizContext> options)
             : base(options)
        {
        }



        public virtual DbSet<AbonadorCargoMensuale> AbonadorCargoMensuales { get; set; }
        public virtual DbSet<Abonadore> Abonadores { get; set; }
        public virtual DbSet<Alergia> Alergias { get; set; }
        public virtual DbSet<Asistencia> Asistencias { get; set; }
        public virtual DbSet<CargoMensuale> CargoMensuales { get; set; }
        public virtual DbSet<Encargado> Encargados { get; set; }
        public virtual DbSet<EncargadoMatricula> EncargadoMatriculas { get; set; }
        public virtual DbSet<EncargadoRegistroDeBaja> EncargadoRegistroDeBajas { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Ingrediente> Ingredientes { get; set; }
        public virtual DbSet<Matricula> Matriculas { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuPlato> MenuPlatos { get; set; }
        public virtual DbSet<Mese> Meses { get; set; }
        public virtual DbSet<NaiPlato> NaiPlatos { get; set; }
        public virtual DbSet<Ninno> Ninnos { get; set; }
        public virtual DbSet<NinnoAlergiaIngrediente> NinnoAlergiaIngredientes { get; set; }
        public virtual DbSet<NinnoEncargado> NinnoEncargados { get; set; }
        public virtual DbSet<NinnoMenu> NinnoMenus { get; set; }
        public virtual DbSet<Parentezco> Parentezcos { get; set; }
        public virtual DbSet<Plato> Platos { get; set; }
        public virtual DbSet<PlatoIngrediente> PlatoIngredientes { get; set; }
        public virtual DbSet<RegistroBaja> RegistroBajas { get; set; }
        public virtual DbSet<TipoAlergia> TipoAlergias { get; set; }
        public virtual DbSet<UsoComedore> UsoComedores { get; set; }





    }
}