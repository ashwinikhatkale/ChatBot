using ChatBot.Data.Entities;
using ChatBot.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace ChatBot.Data.EntityFramework
{
    public class ChatBotInitializer : IDatabaseInitializer<ChatBotContext>
    {
        public void InitializeDatabase(ChatBotContext context)
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                if (context.Database.Exists())
                {
                    if (context.Database.CompatibleWithModel(false))
                        return;

                    context.Database.Delete();
                }
                context.Database.Create();

                //Calling the Seed method
                Seed(context);
            }

        }
        public void Seed(ChatBotContext _context)
        {
            //Add users
            var users = new List<Users>
            {
                new Users{FirstName = "Sachin", LastName = "Jadhav", Password="passw0rd", Address = "Sangola", Email = "sachinjadha12@gmail.com", ContactNumber = "8545231289", RoleId = 1 },
                new Users{ FirstName = "Anvi", LastName = "Shelke", Password="passw0rd", Address = "Wakad, Pune", Email = "anvishelke@gmail.com", ContactNumber = "854981289", RoleId = 2},
                new Users{FirstName = "Swara", LastName = "Shinde", Password="passw0rd", Address = "Sangola", Email = "swarashinde45@gmail.com",ContactNumber = "9423128912", RoleId = 2 },
            };
            _context.Users.AddRange(users);

            _context.SaveChanges();

            //Add notices
            var notices = new List<NoticeBoard>
            {
                new NoticeBoard{ Name = " Academic Calendar",Description="Academic Details", NoticeDateTime=System.DateTime.Now.AddDays(-50) },
                new NoticeBoard{ Name = "Examination",Description="Exam Details, CA1 Details, Mid Details, CA2 Details, Sem Exam Details, Marking Details", NoticeDateTime= System.DateTime.Now.AddDays(-20)},
                new NoticeBoard{ Name = "Holidays" , Description="Year Holidays", NoticeDateTime=System.DateTime.Now.AddDays(-50)},
                new NoticeBoard{ Name = "Admission", Description="Addmission Details, Fee Details, Branch Details, Exam Details", NoticeDateTime=System.DateTime.Now.AddDays(-40)},
                new NoticeBoard{ Name = "Scholarship Form", Description="Document Details", NoticeDateTime=System.DateTime.Now.AddDays(-30) },
                new NoticeBoard{ Name = "Fees" ,Description="Fees Details", NoticeDateTime=System.DateTime.Now.AddDays(-50)},
                new NoticeBoard{ Name = "Computer Department" ,Description="Computer Department Details", NoticeDateTime=System.DateTime.Now.AddDays(-50)},
                new NoticeBoard{ Name = "College Established Year" ,Description="College Established Year Details", NoticeDateTime=System.DateTime.Now.AddDays(-50)}
            };
            _context.NoticeBoards.AddRange(notices);

            _context.SaveChanges();
            // Add Questions
            var questions = new List<QuestionAnswer>
            {
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Examination"), QuestionHint1="Exam Details", QuestionHint2="Exam From", QuestionHint3="Exam Questions",Answer="Exam from check in Your Website and then Update Your Question" },
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Examination"), QuestionHint1="Exam Fee", QuestionHint2="Backlog Questions", QuestionHint3="Mcq Questions",Answer="All Question available in website"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Holidays"), QuestionHint1="Class Schedule", QuestionHint2="Degree Programs", QuestionHint3="Engineering requirments",Answer="Academic Calendar"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Admission"), QuestionHint1="Book Details", QuestionHint2="Bookstore", QuestionHint3="Material",Answer="Libray"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Scholarship Form"), QuestionHint1="Online Services", QuestionHint2="Events Calendar", QuestionHint3="Students Disipline",Answer="Academic Calendar"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Fees"), QuestionHint1="Fee Details", QuestionHint2="Fee slot", QuestionHint3="Total Fee",Answer="Account Session"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Computer Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer="Prof.Dounde P.P"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Mechanical Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer="Prof.Pawar S.A"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Civil Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer="Prof.Adlinge S.S."},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Electrical Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer="Prof. Mallreddy Chinnala."},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "E & TC Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer="Mrs.Raut D.M."},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "First Year Department"), QuestionHint1="Who is HOD of the Deaprtment?", QuestionHint2="Classes", QuestionHint3="Department Staff",Answer=" Prof.Gavade  R.A."},
                new QuestionAnswer{NoticeBoard=notices.FirstOrDefault(x => x.Name == "College"), QuestionHint1="College established year.", QuestionHint2 = "When was FABTECH TECHICAL CAMPUS Solapur established?", QuestionHint3= "College Name?", Answer = "FABTECH TECHICAL CAMPUS Solapur was established in the year 2011."},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Fees"), QuestionHint1="College ownership Type.", QuestionHint2="Type of College ownership.", QuestionHint3="Details College",Answer="Private"},
                new QuestionAnswer{ NoticeBoard=notices.FirstOrDefault(x => x.Name == "Fees"), QuestionHint1="College ownership Type.", QuestionHint2="Type of College ownership.", QuestionHint3="Details College",Answer="Private"},





            };
            _context.QuestionAnswers.AddRange(questions);

            _context.SaveChanges();
        }
       
}
}