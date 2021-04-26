
namespace ChatBot.Data.Entities
{
   public class Role : Entity
    {
        public string Name { get; set; }
    }

    public enum UserRole
    {
        Admin = 1,
        Student
    }
}
