using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestoranProjekat.Modeli
{
	//Klasa koja se odnosi na tabelu zaposlenih u Restoran_Kopijau
	public class Zaposleni
	{
		//ID tabele
		[Key]
		public int ZaposleniId { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		[NotNull]
		public string Username { get; set; }
		[NotNull]
		public string Password { get; set; }

	}
}
