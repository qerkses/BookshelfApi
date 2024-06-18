using BookshelfWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookshelfWeb.Controllers
{
    public class BooksController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7238/api");
        private readonly HttpClient _httpClient;

        public BooksController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BookViewModel> booksList = new List<BookViewModel>();

            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Books");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                booksList = JsonConvert.DeserializeObject<List<BookViewModel>>(content);
            }
            catch (Exception)
            {
           
            }

            return View(booksList);
        }
    }
}
