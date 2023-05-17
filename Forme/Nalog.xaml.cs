using System;
using System.Windows;
using RestoranProjekat.Modeli;
using System.Linq;


namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Nalog.xaml
	/// </summary>
	public partial class Nalog : Window
	{
		public Nalog()
		{
			InitializeComponent();
		}

		private void kreirajButton_Click(object sender, RoutedEventArgs e)
		{
			using (RestoranContext restoran = new RestoranContext())
			{
				//Dodaje novog radnika u listu Zaposlenih
				Zaposleni radnik = new Zaposleni();
				var radnici = from z in restoran.Zaposleni
							  select z.Username;
				radnik.Ime = imeTB.Text;
				radnik.Prezime = prezimeTB.Text;
				radnik.Username = usernameCreateTB.Text;
				//Provera da li se lozinka poklapa i da li zaposleni sa istim korisnickim imenom postoji u bazi
				if (passwordCreateTB.Text.Equals(passwordConfirmTB.Text) && !radnici.Contains(radnik.Username))
				{
					radnik.Password = passwordCreateTB.Text;
					restoran.Zaposleni.Add(radnik);
					restoran.SaveChanges();
					MessageBox.Show("Radnik je dodat, u bazu!");

				}
				else if (radnici.Contains(radnik.Username))
				{
					MessageBox.Show("Radnik sa ovim korisničkim imenom postoji u bazi!");
				}
				else
				{
					MessageBox.Show("Password se ne poklapa, pokušajte ponovo!");
				}
			}

		}
		//Metode koje obezbedjuju da tekst iz polja nestane kada treba da se unesu vrednosti
		//Treba obezbediti da se ovo desava samo ako je tekst defaultni!
		private void imeTB_Focus(object sender, RoutedEventArgs e)
		{
			imeTB.Text = null;
		}
		//Brisanje radnika iz baze
		private void ObrisiNalogButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (RestoranContext restoran = new RestoranContext())
				{
					var radnikZaBrisanje = restoran.Zaposleni.Where(r => r.Username == usernameCreateTB.Text).First();
					restoran.Zaposleni.Remove(radnikZaBrisanje);
					restoran.SaveChanges();
					MessageBox.Show("Radnik " + radnikZaBrisanje.Ime + " " + radnikZaBrisanje.Prezime + " je obrisan iz baze!");

				}
			}
			catch (Exception)
			{
				MessageBox.Show("Došlo je do greške prilikom brisanja radnika iz baze! Unesite username zaposlenog" +
					" i pokušajte ponovo!");
			}
		}

		private void prezimeTB_Focus(object sender, RoutedEventArgs e)
		{
			prezimeTB.Text = null;
		}

		private void usernameCreateTB_Focus(object sender, RoutedEventArgs e)
		{
			usernameCreateTB.Text = null;
		}

		private void passwordCreateTB_Focus(object sender, RoutedEventArgs e)
		{
			passwordCreateTB.Text = null;
		}

		private void passwordConfirmTB_Focus(object sender, RoutedEventArgs e)
		{
			passwordConfirmTB.Text = null;
		}
	}
}
