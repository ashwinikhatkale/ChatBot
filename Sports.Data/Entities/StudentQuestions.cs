using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Data.Entities
{
    public class StudentQuestions :Entity
    {
        public long QuestionId { get; set; }
        public long UserId { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }
        public virtual Users User { get; set; }
    }
}
