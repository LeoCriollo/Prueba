namespace DoleEcIntranet.Data.DataAdam
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityAdam : DbContext
    {
        public EntityAdam()
            : base("name=EntityAdam")
        {
        }

        public virtual DbSet<Rol_usuarios_ADAM> Rol_usuarios_ADAM { get; set; }
        public virtual DbSet<Rol_EstCta_ADAM> Rol_EstCta_ADAM { get; set; }
        public virtual DbSet<Rol_ImpRenta_ADAM> Rol_ImpRenta_ADAM { get; set; }
        public virtual DbSet<Rol_slip_cab_ADAM> Rol_slip_cab_ADAM { get; set; }
        public virtual DbSet<Rol_slip_det_ADAM> Rol_slip_det_ADAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.nom_cia)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.desc_cl_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.num_personal)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.nombre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.ced_identidad)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_cargo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.nom_cargo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_grpnomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.des_grpnomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_departamento)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.des_departamento)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_ccosto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.des_ccosto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_zonageo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.des_zonageo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.num_jefe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.nombre_jefe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cod_cargojefe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.des_cargojefe)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.sistema_origen)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.clave)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.desc_banco)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.cuenta_banco)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_usuarios_ADAM>()
                .Property(e => e.tipo_cta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.compania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.nombre_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.nombre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.desc_concepto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.no_prestamo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.monto_credito)
                .HasPrecision(19, 6);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.tasa)
                .HasPrecision(15, 6);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.saldo)
                .HasPrecision(20, 6);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.cuota)
                .HasPrecision(21, 6);

            modelBuilder.Entity<Rol_EstCta_ADAM>()
                .Property(e => e.sistema)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.compania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.nombre_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.nombre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.cargo_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.desc_carg_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.centro_costo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.desc_ccosto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.grupo_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.desc_gnomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.desc_clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.desc_concepto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.tipo_movimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.tiempo)
                .HasPrecision(8, 3);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.importe)
                .HasPrecision(13, 2);

            modelBuilder.Entity<Rol_ImpRenta_ADAM>()
                .Property(e => e.sistema)
                .IsUnicode(false);

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
                .Property(e => e.sistema)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.compania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.nombre_cia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.nombre)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.cargo_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_carg_trabajador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.centro_costo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_ccosto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.grupo_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_gnomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_clase_nomina)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_concepto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.cod_tarifa)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_tarifa)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.desc_unidad)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.cantidad_unidades)
                .HasPrecision(15, 6);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.valor_tarifa)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.tipo_movimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.tiempo)
                .HasPrecision(8, 3);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.importe)
                .HasPrecision(13, 2);

            modelBuilder.Entity<Rol_slip_det_ADAM>()
                .Property(e => e.sistema)
                .IsUnicode(false);
        }

    }
}
