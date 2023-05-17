using RestoranProjekat.Modeli;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Magacin.xaml
	/// </summary>
	public partial class Magacin : Window
	{
		//Kontrolna promenljiva
		private bool jeAktivan = false;

		public Magacin()
		{

			InitializeComponent();

		}

		//Dodavanje artikla u tabelu i bazu iz polja za unos
		private void dodajArtikalButton_Click(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				var noviArtikal = new Artikli
				{
					NazivArtikla = nazivTB.Text,
					CenaArtikla = double.Parse(cenaTB.Text),
					DostupnaKolicina = int.Parse(kolicinaTB.Text)

				};
				if (noviArtikal.DostupnaKolicina < 0 || noviArtikal.CenaArtikla <= 0)
				{
					MessageBox.Show("Količina artikla mora da bude veća od nule i cena mora da bude veća od nule!");
				}
				else
				{
					restoran.Artikli.Add(noviArtikal);
					restoran.SaveChanges();
					var artikli = from a in restoran.Artikli
								  select a;
					artikliLista.ItemsSource = artikli.ToList();


					nazivTB.IsEnabled = false;
					cenaTB.IsEnabled = false;
					kolicinaTB.IsEnabled = false;
					dodajArtikalButton.IsEnabled = false;
					obrisiArtiklaButton.IsEnabled = false;
					izmeniArtikalButton.IsEnabled = false;
				}
			}

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
		//Postavlja selektovani zapis iz tabele u textBox
		private void artikliLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var odabraniArtikal = artikliLista.SelectedItem as Artikli;

			if (odabraniArtikal != null)
			{
				sifraTB.Text = odabraniArtikal.SifraArtikla.ToString();
				nazivTB.Text = odabraniArtikal.NazivArtikla;
				cenaTB.Text = odabraniArtikal.CenaArtikla.ToString();
				kolicinaTB.Text = odabraniArtikal.DostupnaKolicina.ToString();
			}
		}
		//Brisanje artikla iz liste i baze 
		private void obrisiArtikalButton_Click(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				var artikalZaBrisanje = restoran.Artikli.FirstOrDefault(a => a.SifraArtikla == int.Parse(sifraTB.Text));
				var artikli = from a in restoran.Artikli
							  select a;
				restoran.Remove(artikalZaBrisanje);
				restoran.SaveChanges();
				artikliLista.ItemsSource = artikli.ToList();

				nazivTB.IsEnabled = false;
				cenaTB.IsEnabled = false;
				kolicinaTB.IsEnabled = false;
				dodajArtikalButton.IsEnabled = false;
				obrisiArtiklaButton.IsEnabled = false;
				izmeniArtikalButton.IsEnabled = false;
			}
		}
		//Omogucava dodavanje novog artikla tako sto aktivira buttone za manipulaciju 
		private void noviArtikalButton_Click(object sender, RoutedEventArgs e)
		{
			if (jeAktivan == false)
			{
				nazivTB.IsEnabled = true;
				cenaTB.IsEnabled = true;
				kolicinaTB.IsEnabled = true;
				dodajArtikalButton.IsEnabled = true;
				obrisiArtiklaButton.IsEnabled = true;
				izmeniArtikalButton.IsEnabled = true;
			}



		}
		//Dozvoljava manipulisanje vec postojecim artiklima, promenu vrednosti promenljivih
		private void izmeniArtikalButton_Click(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				var artikalZaIzmenu = restoran.Artikli.FirstOrDefault(a => a.SifraArtikla == int.Parse(sifraTB.Text));
				artikalZaIzmenu.DostupnaKolicina = int.Parse(kolicinaTB.Text);
				artikalZaIzmenu.CenaArtikla = double.Parse(cenaTB.Text);
				if (artikalZaIzmenu.DostupnaKolicina < 0 || artikalZaIzmenu.CenaArtikla <= 0)
				{
					MessageBox.Show("Količina artikla mora da bude veća od nule i cena mora da bude veća od nule!");
				}
				else
				{
					restoran.SaveChanges();
					artikliLista.ItemsSource = restoran.Artikli.ToList();
					nazivTB.IsEnabled = false;
					cenaTB.IsEnabled = false;
					kolicinaTB.IsEnabled = false;
					dodajArtikalButton.IsEnabled = false;
					obrisiArtiklaButton.IsEnabled = false;
					izmeniArtikalButton.IsEnabled = false;
				}

			}
		}
	}
}
