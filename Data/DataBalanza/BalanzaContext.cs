using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoleEcIntranet.Data.DataBalanza
{
    public partial class BalanzaContext : DbContext
    {
        public BalanzaContext()
            : base("name=BalanzaContext")
        {
        }

        public virtual DbSet<CapturaPesaje> CapturaPesaje { get; set; }
        public virtual DbSet<CupoDiario> CupoDiario { get; set; }
        public virtual DbSet<TiposEmpaques> TiposEmpaques { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TiposEmpaques>()
                .Property(e => e.PesoLibraIni)
                .HasPrecision(8, 2);

            modelBuilder.Entity<TiposEmpaques>()
                .Property(e => e.PesoLibraFin)
                .HasPrecision(8, 2);

            modelBuilder.Entity<TiposEmpaques>()
                .Property(e => e.PesoCaja)
                .HasPrecision(8, 2);
        }
    }
}
