using DTO.DTOs.AnnouncementDTOs;
using DTO.DTOs.CommentDTOs;
using DTO.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace RealEstateProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/User");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDTO>>(jsonData);
                var result = values.FirstOrDefault(x => x.UserName== username);
                if (result != null)
                {
                    return View(result);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserDTO updateUserDTO)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateUserDTO);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7132/api/User", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("Username", updateUserDTO.UserName);
                return RedirectToAction("Index","Default");
            }
            return View();
        }

        public async Task<IActionResult> MyAnnouncement()
        {
            var username = HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Announcement");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAnnocounmentListDTO>>(jsonData);
                var result = values.Where(x => x.UserName == username).ToList();
                return View(result);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAnnouncement(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Announcement/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAnnouncementDTO>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncementDTO updateAnnouncementDTO)
        {
            var username = HttpContext.Session.GetString("Username");
            updateAnnouncementDTO.UserName = username;
            updateAnnouncementDTO.ImageUrl2 = "bos";
            updateAnnouncementDTO.Date = DateTime.Now;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateAnnouncementDTO);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7132/api/Announcement", content);
            if (responseMessage.IsSuccessStatusCode)
            {            
                return RedirectToAction("MyAnnouncement", "User");
            }
            return View();
        }

        public async Task<IActionResult> MyComments()
        {
            var username = HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Comment");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
                var result = values.Where(x => x.UserName == username).ToList();
                return View(result);
            }
            return View();
        }

        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7132/api/Comment/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyComments", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7132/api/Comment/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateCommentDTO>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDTO updateCommentDTO)
        {
            var username = HttpContext.Session.GetString("Username");
            updateCommentDTO.UserName = username;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateCommentDTO);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7132/api/Comment", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyComments", "User");
            }
            return View();
        }
    }
}
