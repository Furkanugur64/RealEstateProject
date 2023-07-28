using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfAnnouncementDal : IAnnouncementDal
    {
        private readonly IMongoCollection<Announcement> _announcement;

        public EfAnnouncementDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RealEstateDB");
            _announcement = database.GetCollection<Announcement>("Announcements");
        }

        public void Delete(Announcement t)
        {
            _announcement.DeleteOne(x => x.Id == t.Id);
        }

        public Announcement GetByID(string id)
        {
            return _announcement.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Announcement> GetList()
        {
            return _announcement.Find(x => true).ToList();
        }

        public void Insert(Announcement t)
        {
            _announcement.InsertOne(t);            
        }

        public void Update(Announcement t)
        {
            _announcement.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
