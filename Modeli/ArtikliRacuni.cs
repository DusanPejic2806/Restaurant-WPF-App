namespace RestoranProjekat.Modeli
{
	public class ArtikliRacuni
	{
		public int Id { get; set; }
		public int ArtikliNaRacunuSifraArtikla { get; set; }
		public int RacuniSaArtiklomBrojRacuna { get; set; }

		public Artikli Artikli { get; set; }
		public Racuni Racuni { get; set; }

	}
}
