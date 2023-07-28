using DTO.DTOs.AnnouncementDTOs;
using DTO.DTOs.CommentDTOs;
using DTO.DTOs.UserDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RealEstateProject.Controllers
{
   
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string status,string adress,int price)
        {
            ViewBag.username= HttpContext.Session.GetString("Username");
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Comment");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
                ViewBag.Comments = values.ToList();
            }          
            var responseMessage2 = await client.GetAsync("https://localhost:7132/api/Announcement");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage2.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAnnouncementDTO>>(jsonData);
                if (!string.IsNullOrEmpty(adress) && !string.IsNullOrEmpty(status))
                {
                    values=values.Where(x => x.Status == status && status != "" && x.Adress.Contains(adress)).ToList();
                }
                else if (!string.IsNullOrEmpty(status))
                {
                   values= values.Where(x => x.Status == status).ToList();
                }
                if (!string.IsNullOrEmpty(adress))
                {
                    values=values.Where(x => x.Adress.Contains(adress)).ToList();
                }
                if (price > 0)
                {
                    values=values.Where(x => x.Price == price).ToList();
                }
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> AnnouncementList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Announcement");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAnnocounmentListDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> AnnouncementDetails(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7132/api/Announcement");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAnnocounmentListDTO>>(jsonData);
                var result = values.FirstOrDefault(x=>x.Id==id);              
                if (result != null)
                {                  
                    return View(result);
                }
            }
            return View();

        }

        public async void CommentList()
        {
            
        }

       
    }
}
