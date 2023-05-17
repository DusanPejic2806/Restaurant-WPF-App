using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestoranProjekat.Modeli
{
	//Klasa predstavlja tabelu stolova u bazi podataka
	public class Stolovi
	{

		//ID tabele
		[Key]
		[Column("ID")]
		public int BrojStola { get; set; }
		[Required]
		[Column("BrojStola")]
		public int nazivStola { get; set; }



	}
}
