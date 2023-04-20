using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoleEcIntranet.Models.DBAdam
{
    public partial class AdamContext : DbContext
    {
        public AdamContext()
            : base("name=AdamContext")
        {
        }

        public virtual DbSet<Rol_slip_cab_ADAM> Rol_slip_cab_ADAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.compania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.nombre_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.nombre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.cargo_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.desc_carg_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.centro_costo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.desc_ccosto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.grupo_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.desc_gnomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.desc_clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.telefono_particular)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_cab_ADAM>()
                .Property(e => e.sistema)
                .IsUnicode(false);
        }
    }
}
