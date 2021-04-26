using ChatBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ChatBot.Data.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;

namespace ChatBot.Data.EntityFramework
{
    [DbConfigurationType(typeof(DbContextConfiguration))]
    public class ChatBotContext : DbContext
    {
        public ChatBotContext() : base("ChatBotContext")
        {
            Database.SetInitializer(new ChatBotInitializer());
        }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<StudentQuestions> StudentQuestions { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<StaticMedia> StaticMedias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            foreach (var dbEntityEntry in ChangeTracker.Entries<Entity>())
            {
                if (dbEntityEntry.State == EntityState.Added)
                {
                    dbEntityEntry.Entity.CreatedOn = now;
                    dbEntityEntry.Entity.UpdatedOn = now;
                }

                if (dbEntityEntry.State == EntityState.Modified)
                {
                    dbEntityEntry.Entity.UpdatedOn = now;
                }
            }

            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                // Add the original exception as the innerException
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + ex.InnerException, ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.InnerException.ToString(), ex);
            }
        }
    }

    class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            this.SetDatabaseInitializer(new DropCreateDatabaseAlways<ChatBotContext>());
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}

