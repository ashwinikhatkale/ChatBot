using System;

namespace ChatBot.Data.Entities
{
    public class NoticeBoard : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime NoticeDateTime { get; set; }
       
    }
}
