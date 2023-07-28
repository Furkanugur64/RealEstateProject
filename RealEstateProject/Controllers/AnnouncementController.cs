using DTO.DTOs.AnnouncementDTOs;
using DTO.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace RealEstateProject.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnnouncementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateAnnouncementDTO createAnnouncementDTO)
        {
            var username= HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            createAnnouncementDTO.Date=DateTime.Now;
            createAnnouncementDTO.UserName = username;
            createAnnouncementDTO.ImageUrl2 = "bos";
            var JsonData = JsonConvert.SerializeObject(createAnnouncementDTO);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7132/api/Announcement", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }    

        public async Task<IActionResult> DeleteAnnouncement(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7132/api/Announcement/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Default");
            }
            return View();
        }
    }
}
