
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatBot.Business.Services.Models
{
   public class NoticeModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime NoticeDateTime { get; set; }
    }
}
