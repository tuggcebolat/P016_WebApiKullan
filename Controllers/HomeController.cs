using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P016_WebApiKullan.Models;
using System.Text;

namespace P016_WebApiKullan.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			//Api Uri ->https://localhost:7099/Api/Film
			/*
			 Api Çağırma/Kullanma işlemleri:
			 1. Sayfa yolu url si belirleme
			 2. "request" nesnesi hazırlanır.
			 3. "response" nesnesi hazırlanır.
			 4. response nesnesinin geri cevap türü belirlenir.(Json/Xml/Text)
			 5. Api çağırılır.
			 6. Api sonucuna göre işlem/çevrim metodu yazılabilir
			 6.1 View e veri gönderilir
			 6.2 Veritabanımıza gönderilir.

			 */
			return View();
		}
		public async Task<IActionResult> FilmList()
		{
			var request = new HttpRequestMessage
			{
				RequestUri = new Uri("https://localhost:7099/Api/Film"),
				Method = HttpMethod.Get,
			};

			var client = HttpClientFactory.Create();//pointer
			var response = await client.SendAsync(request);
			var responseStream = await response.Content.ReadAsStringAsync();

			if (responseStream != null)
			{
				var sonuc = JsonConvert.DeserializeObject<List<Film>>(responseStream);
			}

			//*** Önemli -> eğer arayüzde gösterilecekse standart komutlarımız başlıyor.

			return View();
		}

		public async Task<IActionResult> FilmEkle()
		{
			var gelenData = string.Empty;
			try
			{
				var gonderilecekData = new Film()
				{	Basrol = "Yılmaz Morgül", CikisYili = 2001, FilmAdi = "Behçevan", Id = 0, Yonetmen = "Bülent Ersoy"	};

				var request = new HttpRequestMessage
				{
					RequestUri = new Uri("https://localhost:7099/Api/Film"),
					Method = HttpMethod.Post,
					Content = new StringContent(JsonConvert.SerializeObject(gonderilecekData), Encoding.UTF8, "application/json"),
				};

				var client = HttpClientFactory.Create();//pointer
				var response = await client.SendAsync(request);
				var responseStream = await response.Content.ReadAsStringAsync();

				gelenData = responseStream;
				///-> sayfaya ya da popup a gödnerilecek data hazırlanır.
			}
			catch (Exception ex)
			{
			}
			return View(gelenData);
		}

		public async Task<IActionResult> FilmSil()
		{
			var gelenVeri = new List<Film>();
			try
			{
				var gonderilecekModel = new FilmDeleteViewMode()
				{
					Id = 1,
					softDelete = true,
				};

				var request = new HttpRequestMessage
				{
					RequestUri = new Uri("https://localhost:7099/Api/Film"),
					Method = HttpMethod.Delete,
					Content = new StringContent(JsonConvert.SerializeObject(gonderilecekModel), Encoding.UTF8, "application/json"),
				};

				var client = HttpClientFactory.Create();
				var response = await client.SendAsync(request);
				var responseStream = await response.Content.ReadAsStringAsync();

				gelenVeri = JsonConvert.DeserializeObject<List<Film>>(responseStream);
			}
			catch (Exception e)
			{

			}
			return View(gelenVeri);
		}

		public async Task<IActionResult> FilmDuzenle()
		{
			var gelenData = string.Empty;
			try
			{
				var gonderilecekData = new Film()
				{ Basrol = "", CikisYili = 1700, FilmAdi = "Hatice", Id = 3, Yonetmen = "" };

				var request = new HttpRequestMessage
				{
					RequestUri = new Uri("https://localhost:7099/Api/Film"),
					Method = HttpMethod.Put,
					Content = new StringContent(JsonConvert.SerializeObject(gonderilecekData), Encoding.UTF8, "application/json"),
				};

				var client = HttpClientFactory.Create();//pointer
				var response = await client.SendAsync(request);
				var responseStream = await response.Content.ReadAsStringAsync();

				gelenData = responseStream;
				///-> sayfaya ya da popup a gödnerilecek data hazırlanır.
			}
			catch (Exception ex)
			{
			}
			return View(gelenData);
		}
	}
}