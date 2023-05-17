using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RestoranProjekat.Modeli
{
	//Klasa se odnosi na tabelu u bazi koja predstavlja artikle
	public class Artikli
	{
		//ID tabele
		[Key]
		public int SifraArtikla { get; set; }
		public string NazivArtikla { get; set; }
		public double CenaArtikla { get; set; }
		public int DostupnaKolicina { get; set; }
		//Veza sa racunima
		public List<ArtikliRacuni> ArtikliRacuni { get; set; }
		public override string ToString()
		{
			return SifraArtikla + " " + NazivArtikla.ToString() + " " + CenaArtikla.ToString() + " " + DostupnaKolicina.ToString();
		}
	}
}
