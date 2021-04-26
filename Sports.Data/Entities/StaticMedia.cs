

namespace ChatBot.Data.Entities
{
    public class StaticMedia : Entity
    {
        public string FileName { get; set; }

        public int ContentLength { get; set; }

        public string ContentType { get; set; }

        public byte[] InputStream { get; set; }
    }
}
