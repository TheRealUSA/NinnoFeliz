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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=DESKTOP-HPDH4AT; database=NinnoFeliz; trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AbonadorCargoMensuale>(entity =>
            {
                entity.HasKey(e => e.IdAbonadorCargoMensual)
                    .HasName("PK_Abonador_CargoMensual");

                entity.Property(e => e.IdAbonadorCargoMensual).ValueGeneratedNever();

                entity.HasOne(d => d.IdAbonadorNavigation)
                    .WithMany(p => p.AbonadorCargoMensuales)
                    .HasForeignKey(d => d.IdAbonador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_CargoMensual_Abonador");

                entity.HasOne(d => d.IdCargoNavigation)
                    .WithMany(p => p.AbonadorCargoMensuales)
                    .HasForeignKey(d => d.IdCargo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonador_CargoMensual_Cargo");
            });

            modelBuilder.Entity<Abonadore>(entity =>
            {
                entity.HasKey(e => e.IdAbonador)
                    .HasName("PK_Abonador");

                entity.Property(e => e.IdAbonador).ValueGeneratedNever();

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.Abonadores)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AbonadorEncargado");
            });

            modelBuilder.Entity<Alergia>(entity =>
            {
                entity.HasKey(e => e.IdAlergia)
                    .HasName("PK_Alergia");

                entity.Property(e => e.IdAlergia).ValueGeneratedNever();

                entity.Property(e => e.NombreAlergia).IsUnicode(false);

                entity.HasOne(d => d.IdTipoAlergiaNavigation)
                    .WithMany(p => p.Alergia)
                    .HasForeignKey(d => d.IdTipoAlergia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoAlergia");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia)
                    .HasName("PK_Asistencia");

                entity.Property(e => e.IdAsistencia).ValueGeneratedNever();

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsistenciaNinno");
            });

            modelBuilder.Entity<CargoMensuale>(entity =>
            {
                entity.HasKey(e => e.IdCargo)
                    .HasName("PK_CargoMensual");

                entity.Property(e => e.IdCargo).ValueGeneratedNever();

                entity.Property(e => e.CargoMensual).IsUnicode(false);

                entity.HasOne(d => d.IdUsoComedorNavigation)
                    .WithMany(p => p.CargoMensuales)
                    .HasForeignKey(d => d.IdUsoComedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CargoMensual_UsoComedor");
            });

            modelBuilder.Entity<Encargado>(entity =>
            {
                entity.HasKey(e => e.IdEncargado)
                    .HasName("PK_Encargado");

                entity.Property(e => e.IdEncargado).ValueGeneratedNever();

                entity.Property(e => e.Apell1Encargado).IsUnicode(false);

                entity.Property(e => e.Apell2Encargado).IsUnicode(false);

                entity.Property(e => e.DirecciónEncargado).IsUnicode(false);

                entity.Property(e => e.NombreEncargado).IsUnicode(false);

                entity.Property(e => e.TelefonoEncargado).IsUnicode(false);

                entity.HasOne(d => d.IdParentezcoNavigation)
                    .WithMany(p => p.Encargados)
                    .HasForeignKey(d => d.IdParentezco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EncargadoParentezco");
            });

            modelBuilder.Entity<EncargadoMatricula>(entity =>
            {
                entity.HasKey(e => e.IdEncargadoMatricula)
                    .HasName("PK_Encargado_Matricula");

                entity.Property(e => e.IdEncargadoMatricula).ValueGeneratedNever();

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.EncargadoMatriculas)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_Matricula_Encarg");

                entity.HasOne(d => d.NumeroMatriculaNavigation)
                    .WithMany(p => p.EncargadoMatriculas)
                    .HasForeignKey(d => d.NumeroMatricula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_Matricula_Matr");
            });

            modelBuilder.Entity<EncargadoRegistroDeBaja>(entity =>
            {
                entity.HasKey(e => e.IdEncargadoRegistroBaja)
                    .HasName("PK_Encargado_RegistroDeBaja");

                entity.Property(e => e.IdEncargadoRegistroBaja).ValueGeneratedNever();

                entity.HasOne(d => d.IdEncargadoNavigation)
                    .WithMany(p => p.EncargadoRegistroDeBajas)
                    .HasForeignKey(d => d.IdEncargado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_RegistroDeBaja_Encargado");

                entity.HasOne(d => d.IdRegistroBajaNavigation)
                    .WithMany(p => p.EncargadoRegistroDeBajas)
                    .HasForeignKey(d => d.IdRegistroBaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Encargado_RegistroDeBaja_Regis");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.IdGenero).ValueGeneratedNever();

                entity.Property(e => e.DetalleGen).IsUnicode(false);
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(e => e.IdIngrediente)
                    .HasName("PK_Ingrediente");

                entity.Property(e => e.IdIngrediente).ValueGeneratedNever();

                entity.Property(e => e.NombreIngrediente).IsUnicode(false);
            });

            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.HasKey(e => e.NumeroMatricula)
                    .HasName("PK_Matricula");

                entity.Property(e => e.NumeroMatricula).ValueGeneratedNever();

                entity.HasOne(d => d.IdNinnoNavigation)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.IdNinno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatriculaNinno");
            });

        }


    }
}