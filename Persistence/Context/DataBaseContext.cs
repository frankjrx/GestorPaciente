using GestorPacientes.Core.Domain.Common;
using GestorPacientes.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPacientes.Infrastructure.Persistence.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prueb> Pruebas { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
        public DbSet<Cita> Citas { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken()) 
        {
            foreach (var entry in ChangeTracker.Entries<Auditable>()) 
            {
                switch (entry.State) 
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;

                    case EntityState.Modified: 
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            //Tablas
            modelBuilder.Entity<User>().ToTable("Usuarios");
            modelBuilder.Entity<Medico>().ToTable("Medicos");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Prueb>().ToTable("Pruebas");
            modelBuilder.Entity<Resultado>().ToTable("Resultados");
            modelBuilder.Entity<Cita>().ToTable("Citas");

            //Primary Keys
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Medico>().HasKey(x => x.Id);
            modelBuilder.Entity<Paciente>().HasKey(x => x.Id);
            modelBuilder.Entity<Prueb>().HasKey(x => x.Id);
            modelBuilder.Entity<Resultado>().HasKey(x => x.Id);
            modelBuilder.Entity<Cita>().HasKey(x => x.Id);

            //Relaciones
            modelBuilder.Entity<User>()
                .HasMany(u=>u.Medicos)
                .WithOne(m=>m.User)
                .HasForeignKey(m=>m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pacientes)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pruebas)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Resultados)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Citas)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Citas)
                .WithOne(c => c.Paciente)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Medico>()
                .HasMany(m => m.Citas)
                .WithOne(c => c.Medico)
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.resultados)
                .WithOne(r => r.Paciente)
                .HasForeignKey(r => r.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Prueb>()
                .HasMany(p => p.resultados)
                .WithOne(r => r.Prueba)
                .HasForeignKey(r => r.PruebaId)
                .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Cita>()
				.HasMany(c => c.resultados)
				.WithOne(r => r.Cita)
				.HasForeignKey(r => r.CitaId)
				.OnDelete(DeleteBehavior.Cascade);
		}
    }
}
