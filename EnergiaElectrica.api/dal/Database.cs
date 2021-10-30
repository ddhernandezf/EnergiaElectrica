using EnergiaElectrica.api.dal.context;
using Microsoft.EntityFrameworkCore;

namespace EnergiaElectrica.api.dal
{
    public sealed class Database : BaseDatos
    {
        private string stringConnection => "Server=DESKTOP-NDTELRU;Database=EnergiaElectrica;User Id=sa;Password=Letmein1.;";

        public Database(DbContextOptions<BaseDatos> options)
            : base(options)
        {
        }

        public Database()
        {
            Database.SetCommandTimeout(900000);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(this.stringConnection);
        }
    }
}
