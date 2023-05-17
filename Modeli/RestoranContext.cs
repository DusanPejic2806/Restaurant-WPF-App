using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace RestoranProjekat.Modeli
{
	//Klasa za Kreiranje tabela u bazi i postavku baze
	public class RestoranContext : DbContext
	{
		//Tabele u bazi
		public DbSet<Artikli> Artikli { get; set; }
		public DbSet<Racuni> Racuni { get; set; }
		public DbSet<Zaposleni> Zaposleni { get; set; }
		public DbSet<Stolovi> Stolovi { get; set; }
		public DbSet<ArtikliRacuni> ArtikliRacuni { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Povezivanje tabele ArtikliRacuni sa Racunima i Artiklima
			modelBuilder.Entity<ArtikliRacuni>()
		.HasKey(ar => ar.Id);

			modelBuilder.Entity<ArtikliRacuni>()
				.HasOne(ar => ar.Artikli)
				.WithMany(a => a.ArtikliRacuni)
				.HasForeignKey(ar => ar.ArtikliNaRacunuSifraArtikla);

			modelBuilder.Entity<ArtikliRacuni>()
				.HasOne(ar => ar.Racuni)
				.WithMany(r => r.ArtikliRacuni)
				.HasForeignKey(ar => ar.RacuniSaArtiklomBrojRacuna);
		}


		//Veza sa bazom
		protected override void OnConfiguring(DbContextOptionsBuilder vezaBaza)
		{
			//Formiranje relativne putnja do baze podataka tako sto se kombinuju putanje do baze i izvrsnog fajla aplikacije
			string aplikacijaPutanja = AppDomain.CurrentDomain.BaseDirectory;
			string bazaRelativnaPutanja = @"..\..\..\Baza\RestoranProjekatBaza.mdf";
			string bazaPutanja = Path.GetFullPath(Path.Combine(aplikacijaPutanja, bazaRelativnaPutanja));
			string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{bazaPutanja}\";Integrated Security=True";
			/***CELA PUTANJA KAO KONEKCIONI STRING***/
			//string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Dusan RAD\\FAKULTET\\CS322\\PROJEKAT\\RestoranProjekat\\Baza\\RestoranProjekatBaza.mdf;Integrated Security=True";
			vezaBaza.UseSqlServer(connectionString);
		}
	}
}
