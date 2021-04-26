
namespace ChatBot.Business.Services.Models
{
    public class TeamMemberModel
    {
        public long TeamId { get; set; }
        public string TeamName { get; set; }
        public long UserId { get; set; }
        public string TeamMemberName { get; set; }
        public bool IsCaption { get; set; }

    }
}
