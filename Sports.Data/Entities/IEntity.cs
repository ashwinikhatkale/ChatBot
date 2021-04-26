using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBot.Data.Entities
{
   public interface IEntity
    {
        long Id { get; }

        DateTime CreatedOn { get; }

        DateTime UpdatedOn { get; }
    }
    public abstract class Entity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public virtual void OnCreated()
        {

        }

        protected Entity()
        {
            CreatedOn = DateTime.UtcNow;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}