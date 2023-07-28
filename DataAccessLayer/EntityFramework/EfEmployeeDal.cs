using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfEmployeeDal : IEmployeeDal
    {
        private readonly IMongoCollection<Employee> _employee;

        public EfEmployeeDal()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RealEstateDB");
            _employee = database.GetCollection<Employee>("Employees");
        }

        public void Delete(Employee t)
        {
            _employee.DeleteOne(x => x.Id == t.Id);
        }

        public Employee GetByID(string id)
        {
            return _employee.Find(x => x.Id == id).FirstOrDefault();
        }

        public List<Employee> GetList()
        {
            return _employee.Find(x => true).ToList();
        }

        public void Insert(Employee t)
        {
            _employee.InsertOne(t);
        }

        public void Update(Employee t)
        {
            _employee.ReplaceOne(x => x.Id == t.Id, t);
        }
    }
}
