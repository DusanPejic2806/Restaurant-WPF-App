using RestoranProjekat.Modeli;
using System.Collections.Generic;
using System.Linq;
using System.Windows;



namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Izvestaji.xaml
	/// </summary>
	public partial class Izvestaji : Window
	{
		public Izvestaji()
		{
			InitializeComponent();
		}

		private void IzvestajiProzor_Loaded(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				var zaposleniIzvestaji = from z in restoran.Zaposleni
										 select z.Username;
				foreach (var z in zaposleniIzvestaji)
				{
					ZaposleniCB.Items.Add(z);
				}
				var stoloviIzvestaji = from s in restoran.Stolovi
									   select s.nazivStola;
				foreach (var s in stoloviIzvestaji)
				{
					StoloviCB.Items.Add(s);

				}
			}
		}

		private void ProdajaStoButton_Click(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				//Pronalazi cene svih artikala koji su izdati na jednom stolu preko racuna i veze artikala sa racunima
				var izvestaj = from r in restoran.Racuni
							   join ar in restoran.ArtikliRacuni on r.BrojRacuna equals ar.RacuniSaArtiklomBrojRacuna
							   join a in restoran.Artikli on ar.ArtikliNaRacunuSifraArtikla equals a.SifraArtikla
							   where r.IdStola.nazivStola.ToString() == StoloviCB.Text.ToString()
							   select a.CenaArtikla;
				//Ispisuje rezultat u ListBox
				IzvestajiListBox.Items.Add("Ovaj sto je zaradio " + izvestaj.Sum() + " dinara.");

			}
		}

		private void ProdajaZaposleni_Click(object sender, RoutedEventArgs e)
		{
			//Pronalazi cene svih artikala koji su prodati od strane jednog zaposlenog preko racuna koje je on izdao
			// i veze racuna sa artiklom
			using (RestoranContext restoran = new RestoranContext())
			{
				var izvestajZaposleni = from r in restoran.Racuni
										where r.IdZaposlenog.Username == ZaposleniCB.Text.ToString()
										from ar in r.ArtikliRacuni
										select ar.Artikli.CenaArtikla;
				//Ispisuje rezultat u ListBox
				IzvestajiListBox.Items.Add("Ovaj radnik je napravio promet od " + izvestajZaposleni.Sum() + " dinara.");
			}
		}
	}
}
