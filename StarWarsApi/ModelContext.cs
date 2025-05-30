
using Microsoft.EntityFrameworkCore;
using StarWarsApi.Daos;
using StarWarsApi.Models.database;


namespace BDCADAO.BDModels
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
            /**/
        }

        public ModelContext(DbContextOptions<ModelContext> options) : base(options)
        {
            /*models*/
            Characters = new(characters);
            Films = new(films);
            Planets = new(planets);
            Species = new(species);
            Starships = new(starships);
            Vehicles = new(vehicles);

        }
        //daos
        public virtual CharacterDao Characters { get; set; }
        private DbSet<Character> characters { get; set; }
        public virtual FilmDao Films { get; set; }
        private DbSet<Films> films { get; set; }
        public virtual PlanetDao Planets { get; set; }
        private DbSet<Planet> planets { get; set; }
        public virtual SpecieDao Species { get; set; }
        private DbSet<Species> species { get; set; }
        public virtual StarshipDao Starships { get; set; }
        private DbSet<Starship> starships { get; set; }
        public virtual VehicleDao Vehicles { get; set; }
        private DbSet<Vehicle> vehicles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseOracle("USER ID=ceat;PASSWORD=ceat;DATA SOURCE=192.168.56.114:1521/xe;PERSIST SECURITY INFO=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            //modelos
            modelBuilder.Entity<Character>().HasKey(v=>v.Uid);
            modelBuilder.Entity<Films>().HasKey(v=>v.Uid);
            modelBuilder.Entity<Planet>().HasKey(v=>v.Uid);
            modelBuilder.Entity<Species>().HasKey(v=>v.Uid);
            modelBuilder.Entity<Starship>().HasKey(v=>v.Uid);
            modelBuilder.Entity<Vehicle>().HasKey(v=>v.Uid);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuild);

        
    }
}