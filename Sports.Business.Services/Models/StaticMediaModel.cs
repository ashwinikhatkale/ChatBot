using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Business.Services.Models
{
    public class StaticMediaModel
    {
        public string FileName { get; set; }

        public int ContentLength { get; set; }

        public string ContentType { get; set; }

        public byte[] InputStream { get; set; }
    }
}
