﻿using EntityLayer.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Announcement:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string Adress { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int Area { get; set; }
        public int RoomCount { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrl2 { get; set; }
        public string Status { get; set; }

        public Announcement()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
