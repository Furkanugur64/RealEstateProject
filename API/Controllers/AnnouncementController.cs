using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public IActionResult AnnouncementList()
        {
            var values = _announcementService.TGetList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult AddAnnouncement(Announcement announcement)
        {
            _announcementService.TInsert(announcement);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnnouncement(string id)
        {
            var value = _announcementService.TGetByID(id);
            _announcementService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAnnouncement(string id)
        {
            var value = _announcementService.TGetByID(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateAnnouncement(Announcement announcement)
        {
            _announcementService.TUpdate(announcement);
            return Ok();
        }
    }
}
