using RestoranProjekat.Modeli;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;



namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Restoran.xaml
	/// </summary>
	public partial class Restoran : Window
	{

		//Veza sa bazom 
		RestoranContext restoran = new RestoranContext();
		//Promenljive koje se koriste kod pomeranja stolova
		private Point pocetnaPozicija;
		private bool sePomera;
		//Promenljiva koja cuva trenutnog zaposlenog
		public string korisnik;
		public Restoran(string korisnikTrenutni)
		{
			InitializeComponent();
			korisnik = korisnikTrenutni;
		}
		//Metoda koja se poziva prilikom ucitavanja prozora
		private void RestoranProzor_Loaded(object sender, EventArgs e)
		{

			//Zapamceni stolovi se ucitavaju tako sto se najpre formira niz stringova odvojenih zarezima
			string pozicija = Properties.Settings.Default.PozicijaStolova;
			string[] pozicijaStolova = pozicija.Split(';');
			//prva dva elementa se smestaju u koordinate dok se treci element smesta u content dugmeta
			//Ovaj postupak se ponavlja za svako dugme
			foreach (string p in pozicijaStolova)
			{
				if (!string.IsNullOrEmpty(p))
				{
					string[] delovi = p.Split(',');
					double left = double.Parse(delovi[0]);
					double top = double.Parse(delovi[1]);
					Button sto = new Button();
					sto.Content = $"{delovi[2]}";
					sto.Width = 100;
					sto.Height = 30;

					Canvas.SetLeft(sto, left);
					Canvas.SetTop(sto, top);

					sto.FontFamily = new FontFamily("Arial");
					sto.FontSize = 12;
					sto.BorderBrush = Brushes.Red;
					sto.BorderThickness = new Thickness(2);
					sto.Background = Brushes.Yellow;
					RestoranPrikaz.Children.Add(sto);
					//dodavanje metoda svakom ucitanom dugmetu
					sto.PreviewMouseDown += Sto_PreviewLeftMouseDown;
					sto.PreviewMouseMove += Sto_PreviewMouseMove;
					sto.PreviewMouseUp += Sto_PreviewMouseUp;


				}
				// prikaz trenutnog korisnika
				korsinikLabel.Content = korisnik;
			}

		}
		//Dodavanje stolova
		private void DodajSto_Click(object sender, RoutedEventArgs e)
		{
			//Otvara se novi prozor u kom se unosi broj stola koji dodajemo
			//odnoso sta ce pisati na dugmetu koje predstavalja sto
			DodavanjeStola novoDodavanje = new DodavanjeStola();
			novoDodavanje.ShowDialog();
			if (!string.IsNullOrEmpty(novoDodavanje.BrojNovogStola))
			{
				try
				{
					//Provera da li sto vec postoji u bazi
					int nazivStola = int.Parse(novoDodavanje.BrojNovogStola);
					bool stoPostoji = restoran.Stolovi.Any(s => s.nazivStola == nazivStola);
					if (stoPostoji)
					{
						MessageBox.Show("Sto vec postoji u bazi");
						return;
					}

					// Pravi sto u bazi
					Stolovi stoBaza = new Stolovi();
					stoBaza.nazivStola = nazivStola;
					restoran.Stolovi.Add(stoBaza);


					// Pravi sto u vidu dugmeta na formi
					Button Sto = new Button();
					Sto.Name = $"stoButton{stoBaza.BrojStola}";
					Sto.Content = stoBaza.nazivStola.ToString();
					Sto.Height = 30;
					Sto.Width = 100;
					Canvas.SetLeft(Sto, 0);
					Canvas.SetTop(Sto, 0);
					Sto.FontFamily = new FontFamily("Arial");
					Sto.FontSize = 12;
					Sto.BorderBrush = Brushes.Red;
					Sto.BorderThickness = new Thickness(2);
					Sto.Background = Brushes.Yellow;
					RestoranPrikaz.Children.Add(Sto);
					//Metode koje se dodaju dugmetu sto
					Sto.PreviewMouseDown += Sto_PreviewLeftMouseDown;
					Sto.PreviewMouseMove += Sto_PreviewMouseMove;
					Sto.PreviewMouseUp += Sto_PreviewMouseUp;
					SacuvajButton.IsEnabled = true;
					//Cuva izmene u bazi kako ne bi mogli da dodamo dva ista stola pre nego sto se sacuvaju izmene
					restoran.SaveChanges();

				}
				catch (FormatException)
				{
					MessageBox.Show("Niste uneli broj");
				}
			}
		}

		//***************************************************Stolovi***************************************
		//Event kada se pritisne taster misa
		private void Sto_PreviewLeftMouseDown(object sender, MouseButtonEventArgs e)
		{
			//Ako je pritisnut levi klik dugme moze da se pomera i pocetna pozicija misa dobija vrednost u odnosu na platno
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				sePomera = true;
				pocetnaPozicija = e.GetPosition(RestoranPrikaz);
				//Ovo omogucava dugmetu da prati pokrete misa
				(sender as Button).CaptureMouse();
			}
			//Ako je pritisnut desni klik misa pojavljuje se ContextMenu sa opcijom za izdavanja racuna i brisanja dugmeta
			else if (e.RightButton == MouseButtonState.Pressed)
			{
				Button button = sender as Button;

				if (button != null)
				{
					//Kreiranje ContextMenu-a sa metodama za izdavanje racuna i brisanje racuna
					ContextMenu meni = new ContextMenu();
					MenuItem obrisiDugme = new MenuItem();
					MenuItem izdajRacun = new MenuItem();
					izdajRacun.Header = "Izdaj Račun";
					obrisiDugme.Header = "Obriši Sto";
					obrisiDugme.Click += (sender, e) =>
					{
						//Brise dugme i sto iz baze
						var stoBrisanje = restoran.Stolovi.FirstOrDefault(s => s.nazivStola == int.Parse(button.Content.ToString()));
						restoran.Stolovi.Remove(stoBrisanje);
						RestoranPrikaz.Children.Remove(button);
						SacuvajButton.IsEnabled = true;
						restoran.SaveChanges();



					};
					izdajRacun.Click += (sender, e) =>
					{

						//Otvara prozor za dodavanje artikala na racun i prosledjuje mu vrednosti trenutnog korisnika i stola
						//na kome se izdaje racun
						Racun racun = new Racun(korisnik, int.Parse(button.Content.ToString()));
						racun.Show();
					};
					meni.Items.Add(izdajRacun);
					meni.Items.Add(obrisiDugme);
					meni.PlacementTarget = button;
					meni.IsOpen = true;
				}
			}
		}

		//Event koji se poziva kada se mis pomera preko dugmeta
		private void Sto_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			//Ako je dugme moguce pomerati odnosno ako je pritisnut levi taster misa
			if (sePomera)
			{
				//Postavlja trenutnu poziciju misa u odnosu na canvas element i racuna pomeraj 
				Point trenutnaPozicijaMisa = e.GetPosition(RestoranPrikaz);
				double pomerajX = trenutnaPozicijaMisa.X - pocetnaPozicija.X;
				double pomerajY = trenutnaPozicijaMisa.Y - pocetnaPozicija.Y;
				//Postavlja se nova pozicija dugmeta 
				Button button = sender as Button;
				double newLeft = Canvas.GetLeft(button) + pomerajX;
				double newTop = Canvas.GetTop(button) + pomerajY;

				// Proverava da li je nova pozicija dugmeta u granicama Canvas elementa i ako jeste postavalja novu poziciju dugmeta
				if (newLeft >= 0 && newTop >= 0 && newLeft + button.ActualWidth <= RestoranPrikaz.ActualWidth
					&& newTop + button.ActualHeight <= RestoranPrikaz.ActualHeight)
				{
					Canvas.SetLeft(button, newLeft);
					Canvas.SetTop(button, newTop);
					pocetnaPozicija = trenutnaPozicijaMisa;
				}
			}
		}
		//Event kada se pusti klik misa
		private void Sto_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			//Dugme ne moze da se pomera i poziva se metoda suprotna CaptureMouse() kako dugme vise ne bi pratilo misa 
			sePomera = false;
			(sender as Button).ReleaseMouseCapture();
		}

		//Event koji cuva raspored stolova
		private void SacuvajButton_Click(object sender, RoutedEventArgs e)
		{
			//dugme sto se cuva u vidu stringa, gde su koordinate dugmeta zapisane kao prvi i drugi element stringa
			//dok je treci element zapravo broj stola odnostno tekst koji pise u buttonu
			try
			{
				string karakteristikeStola = "";

				for (int i = 0; i < RestoranPrikaz.Children.Count; i++)
				{
					if (RestoranPrikaz.Children[i] is Button button)
					{
						double left = Canvas.GetLeft(button);
						double top = Canvas.GetTop(button);
						int number = int.Parse(button.Content.ToString());
						karakteristikeStola += $"{left},{top},{number};";
					}
				}
				Properties.Settings.Default.PozicijaStolova = karakteristikeStola;
				Properties.Settings.Default.Save();
				//Cuva stolove u bazi!!!
				restoran.SaveChanges();
				SacuvajButton.IsEnabled = false;
				MessageBox.Show("Raspored stolova je sačuvan!");
			}
			catch (Exception)
			{
				MessageBox.Show("Došlo je do greške, niste uspeli da sačuvate raspored stolova!");
			}

		}
		//Otvara prozor za rad sa artiklima
		private void artikliButton_Click(object sender, RoutedEventArgs e)
		{
			Magacin magacin = new Magacin();
			magacin.Show();

		}
		//Otvara prozor za izvestaje
		private void IzvestajiButton_Click(object sender, RoutedEventArgs e)
		{
			Izvestaji izvestaj = new Izvestaji();
			izvestaj.Show();
		}
		//Log out metoda 
		private void IzlogujButton_Click(object sender, RoutedEventArgs e)
		{
			if (!SacuvajButton.IsEnabled)
			{
				Login login = new Login();
				login.Show();
				this.Close();
			}
			else
			{
				MessageBox.Show("Ne možete da se izlogujete dok ne sačuvate raspored stolova!");
			}
		}
		//Sprecava da se sto zatvori ako je neki sto dodat a nisu sacuvane izmene!
		private void RestoranProzor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (SacuvajButton.IsEnabled)
			{
				e.Cancel = true;
				MessageBox.Show("Da biste zatvorili aplikaciju morate sacuvati izmene!");
			}
		}
	}
}
