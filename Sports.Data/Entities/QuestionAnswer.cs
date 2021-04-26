
namespace ChatBot.Data.Entities
{
    public class QuestionAnswer : Entity
    {
        public string QuestionHint1 { get; set; }
        public string QuestionHint2 { get; set; }
        public string QuestionHint3 { get; set; }
        public string Answer { get; set; }
        public long? FileId { get; set; }
        public virtual StaticMedia File { get; set; }
        public long NoticeBoardId { get; set; }
        public virtual NoticeBoard NoticeBoard { get; set; }
    }
}
