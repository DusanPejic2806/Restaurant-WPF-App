using System;
using System.Windows;
using RestoranProjekat.Modeli;
using System.Linq;


namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
		}
		//Logovanje korisnika u aplikaciju
		private void loginButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				using (RestoranContext restoran = new RestoranContext())
				{
					var zaposleni = (from z in restoran.Zaposleni
									 where z.Username == $"{usernameTB.Text}"
									 && z.Password == $"{passwordTB.Text}"
									 select z).First();

					Restoran radniProzor = new Restoran(zaposleni.Username);
					radniProzor.Show();
					this.Close();
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Zaposleni ne postoji!");
			}


		}
		//Otvaranje prozora za dodavanje novog radnika u bazu
		private void nalogButton_Click(object sender, RoutedEventArgs e)
		{
			Nalog nalog = new Nalog();
			nalog.Show();
		}
	}
}

