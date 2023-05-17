using System.Windows;


namespace RestoranProjekat.Forme
{
	/// <summary>
	/// Interaction logic for DodavanjeStola.xaml
	/// </summary>
	public partial class DodavanjeStola : Window
	{
		//Prop koji cuva uneti broj u textBox
		public string BrojNovogStola
		{
			get { return dodajStoTB.Text; }
			set {; }
		}
		public DodavanjeStola()
		{
			InitializeComponent();
		}

		//Kada se klikne dugme dodaj ako je unos ispravan dodeljuje se vrednost broja unetog u textBoxu
		private void dodajButton_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(dodajStoTB.Text, out int brojS))
			{
				MessageBox.Show("Pogrešan unos!");

			}
			else
			{
				BrojNovogStola = brojS.ToString();
				this.Close();
			}
		}

	}
}
