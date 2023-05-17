using RestoranProjekat.Modeli;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Racun.xaml
	/// </summary>
	public partial class Racun : Window
	{
		public string korisnik;
		public int brojStola;
		public Racun(string korisnikTrenutni, int sto)
		{
			InitializeComponent();
			korisnik = korisnikTrenutni;
			brojStola = sto;
		}
		//Ucitavanje tabele sa artiklima
		private void artikliLista_Loaded(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				var artikli = from a in restoran.Artikli
							  select a;

				artikliLista.ItemsSource = artikli.ToList();
			}

		}
		//Pretrazivanje artikla po nazivu
		private void PretragaTB_TextChanged(object sender, TextChangedEventArgs e)
		{
			string tekstPretrage = PretragaTB.Text;
			using (RestoranContext restoran = new RestoranContext())
			{
				var rezultatPretrage = from a in restoran.Artikli
									   where a.NazivArtikla.Contains(tekstPretrage)
									   select a;
				artikliLista.ItemsSource = rezultatPretrage.ToList();
			}
		}
		//Dodavanje Artikla na racun
		private void DodajURacunButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{ 
				using (RestoranContext restoran = new RestoranContext())
				{

					var artikalRacuna = restoran.Artikli.Where(a => a.Equals(artikliLista.SelectedItem)).First();
					if ((artikalRacuna.DostupnaKolicina - 1) < 0)
					{
						MessageBox.Show("Ovaj artikal se ne može dodati na račun jer nije na stanju!");
					}
					else
					{
						cenaRacuna.Text = (double.Parse(cenaRacuna.Text) + artikalRacuna.CenaArtikla).ToString();
						RacunListBox.Items.Add(artikalRacuna);
						artikalRacuna.DostupnaKolicina--;
						var artikli = from a in restoran.Artikli
									  select a;
						artikliLista.ItemsSource = artikli.ToList<Artikli>();
						restoran.SaveChanges();
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Odaberite artikal koji želite da dodate na račun!");
			}

		}
		//Brise artikal sa racuna
		private void ObrisiSaRacunaButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				using (RestoranContext restoran = new RestoranContext())
				{
					var artikalRacunaBrisanje = restoran.Artikli.Where(a => a.Equals(RacunListBox.SelectedItem)).First();
					RacunListBox.Items.Remove(RacunListBox.SelectedItem);
					cenaRacuna.Text = (int.Parse(cenaRacuna.Text) - artikalRacunaBrisanje.CenaArtikla).ToString();
					artikalRacunaBrisanje.DostupnaKolicina++;
					var artikli = from a in restoran.Artikli
								  select a;
					artikliLista.ItemsSource = artikli.ToList<Artikli>();
					restoran.SaveChanges();
				}

			}
			catch (Exception)
			{
				MessageBox.Show("Odaberite artikal za brisanje!");
			}
		}

		private void SacuvajRacunButton_Click(object sender, RoutedEventArgs e)
		{
			//Kreira racun u bazi i povezuje racune i artikle na njemu
			using (RestoranContext restoran = new RestoranContext())
			{
				Racuni racun = new Racuni();
				racun.datumIzdavanja = DateTime.Now;
				var zaposelniRacun = restoran.Zaposleni.Where(z => z.Username.Equals(korisnik)).FirstOrDefault();
				var stoRacun = restoran.Stolovi.Where(s => s.nazivStola == brojStola).FirstOrDefault();
				racun.IdZaposlenog = zaposelniRacun;
				racun.IdStola = stoRacun;
				foreach (var artikal in RacunListBox.Items)
				{
					int sifra = ((Artikli)artikal).SifraArtikla;
					Artikli artikalIzBaze = restoran.Artikli.Where(a => a.SifraArtikla == sifra).FirstOrDefault();

					ArtikliRacuni artikliRacuniVeza = new ArtikliRacuni();
					artikliRacuniVeza.Racuni = racun;
					artikliRacuniVeza.Artikli = artikalIzBaze;
					restoran.ArtikliRacuni.Add(artikliRacuniVeza);
				}
				restoran.Racuni.Add(racun);
				restoran.SaveChanges();
				MessageBox.Show("Račun je izdat!");
			}
		}
	}
}
