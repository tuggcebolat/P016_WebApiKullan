namespace P016_WebApiKullan.Models
{
	public class Film
	{
		public int Id { get; set; }
		public string FilmAdi { get; set; }
		public string Yonetmen { get; set; }
		public string Basrol { get; set; }
		public int CikisYili { get; set; } = 1700;
	}
}
