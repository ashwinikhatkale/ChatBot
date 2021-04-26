using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Models
{
    public class QuestionAnswerModel
    {
        public long Id { get; set; }
        [Required]
        public string QuestionHint1 { get; set; }
        public string QuestionHint2 { get; set; }
        public string QuestionHint3 { get; set; }
        [Required]
        public string Answer { get; set; }
        public long? FileId { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public byte[] FileInputStream { get; set; }
        public long NoticeBoardId { get; set; }
        public string NoticeBoard { get; set; }
        public List<SelectListItem> NoticeBoards { get; set; }
    }
}
