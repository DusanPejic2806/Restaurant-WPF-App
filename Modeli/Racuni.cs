using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestoranProjekat.Modeli
{
	public class Racuni
	{
		//ID tabele
		[Key]
		public int BrojRacuna { get; set; }
		public DateTime datumIzdavanja { get; set; }

		//Veza za Zaposlenima
		public Zaposleni IdZaposlenog { get; set; }
		//Veza sa Stolovima
		public Stolovi IdStola { get; set; }
		//Veza sa artiklima
		public List<ArtikliRacuni> ArtikliRacuni { get; set; }

	}
}
