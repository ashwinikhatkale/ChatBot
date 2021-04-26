using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Data.Entities;
using ChatBot.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Services
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        private readonly ChatBotContext _context;
        public QuestionAnswerRepository(ChatBotContext context)
        {
            _context = context;
        }
        public async Task<QuestionAnswerModel> GetAnswer(long id)
        {
            var question = await _context.QuestionAnswers
                                    .Where(x => x.Id == id)
                                    .Select(x => new QuestionAnswerModel 
                                    { 
                                        Id = x.Id, 
                                        QuestionHint1 = x.QuestionHint1,
                                        QuestionHint2 = x.QuestionHint2,
                                        QuestionHint3 = x.QuestionHint3,
                                        Answer = x.Answer,
                                        FileName = x.FileId > 0 ? x.File.FileName : "",
                                        FileId = x.FileId,
                                    }).FirstOrDefaultAsync();

            return question;
        }

        public async Task<List<QuestionAnswerModel>> GetQuestionAnswers()
        {
            var questions = await _context.QuestionAnswers
                                    .Select(x => new QuestionAnswerModel
                                    {
                                        Id = x.Id,
                                        QuestionHint1 = x.QuestionHint1,
                                        QuestionHint2 = x.QuestionHint2,
                                        QuestionHint3 = x.QuestionHint3,
                                        Answer = x.Answer,
                                        FileName = x.FileId > 0 ? x.File.FileName : "",
                                        FileId = x.FileId,
                                        NoticeBoardId = x.NoticeBoardId,
                                        NoticeBoard = x.NoticeBoard.Name
                                    }).ToListAsync();

            return questions.OrderBy(x => x.NoticeBoard).ThenBy(x => x.QuestionHint1).ThenBy(x => x.QuestionHint2).ThenBy(x => x.QuestionHint3).ToList();
        }

        public async Task<List<QuestionAnswerModel>> GetQuestionAnswersByNoticeId(long noticeId)
        {
            var questions = await _context.QuestionAnswers
                                    .Where(x => x.NoticeBoardId == noticeId)
                                    .Select(x => new QuestionAnswerModel
                                    {
                                        Id = x.Id,
                                        QuestionHint1 = x.QuestionHint1,
                                        QuestionHint2 = x.QuestionHint2,
                                        QuestionHint3 = x.QuestionHint3,
                                        Answer = x.Answer,
                                        FileName = x.FileId > 0 ? x.File.FileName : "",
                                        FileId = x.FileId,
                                        NoticeBoardId = x.NoticeBoardId,
                                        NoticeBoard = x.NoticeBoard.Name
                                    }).ToListAsync();

            return questions.OrderBy(x => x.NoticeBoard).OrderBy(x => x.QuestionHint1).ThenBy(x => x.QuestionHint2).ThenBy(x => x.QuestionHint3).ToList();
        }

        public async Task<List<QuestionAnswerModel>> GetPendingQueries()
        {           
            var pendingQuestions = await _context.QuestionAnswers.Where(x => x.Answer == null)
                                    .Join(_context.StudentQuestions, q => q.Id, s => s.QuestionId, (q,s) => q)
                                    .Select(x => new QuestionAnswerModel
                                    {
                                        Id = x.Id,
                                        QuestionHint1 = x.QuestionHint1,
                                        QuestionHint2 = x.QuestionHint2,
                                        QuestionHint3 = x.QuestionHint3,
                                        Answer = x.Answer,
                                        FileName = x.FileId > 0 ? x.File.FileName : "",
                                        FileId = x.FileId,
                                        NoticeBoardId = x.NoticeBoardId,
                                        NoticeBoard = x.NoticeBoard.Name
                                    }).ToListAsync();

            return pendingQuestions.OrderBy(x => x.NoticeBoard).ThenBy(x => x.QuestionHint1).ThenBy(x => x.QuestionHint2).ThenBy(x => x.QuestionHint3).ToList();
        }
        public async Task<List<QuestionAnswerModel>> GetStudentsPendingQueries(long userID)
        {
            var pendingQuestions = await _context.QuestionAnswers
                                    .Join(_context.StudentQuestions.Where(x=> x.UserId == userID), q => q.Id, s => s.QuestionId, (q, s) => q)
                                    .Select(x => new QuestionAnswerModel
                                    {
                                        Id = x.Id,
                                        QuestionHint1 = x.QuestionHint1,
                                        QuestionHint2 = x.QuestionHint2,
                                        QuestionHint3 = x.QuestionHint3,
                                        Answer = x.Answer,
                                        FileName = x.FileId > 0 ? x.File.FileName : "",
                                        FileId = x.FileId,
                                        NoticeBoardId = x.NoticeBoardId,
                                        NoticeBoard = x.NoticeBoard.Name
                                    }).ToListAsync();

            return pendingQuestions.OrderBy(x => x.NoticeBoard).ThenBy(x => x.QuestionHint1).ThenBy(x => x.QuestionHint2).ThenBy(x => x.QuestionHint3).ToList();
        }
        public async Task<List<SelectListItem>> GetQuestionsSelectList(long noticeId = 0)
        {
            var questionAnswers = (noticeId > 0) ? await _context.QuestionAnswers.Where(x => x.NoticeBoardId == noticeId && x.Answer != null && x.Answer != "").ToListAsync() : await _context.QuestionAnswers.Where(x => x.Answer != null && x.Answer != "").ToListAsync();

            var hint1 = questionAnswers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.QuestionHint1 }).ToList();
            var hint2 = questionAnswers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.QuestionHint2 }).ToList();
            var hint3 = questionAnswers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.QuestionHint3 }).ToList();

            return (hint1.Union(hint2).Union(hint3)).OrderBy(x => x.Value).Distinct().ToList();
        }
        public async Task<bool> Insert(QuestionAnswerModel model)
        {
            var question = new Data.Entities.QuestionAnswer { NoticeBoardId = model.NoticeBoardId, QuestionHint1 = model.QuestionHint1,QuestionHint2 = model.QuestionHint2, QuestionHint3 = model.QuestionHint3, Answer = model.Answer };
            question.File = new StaticMedia {  ContentLength = model.File.ContentLength, ContentType = model.File.ContentType, FileName = model.File.FileName, InputStream = model.FileInputStream };
            _context.QuestionAnswers.Add(question);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> InsertStudentQuestions(QuestionAnswerModel model, long userId)
        {
            var question = new QuestionAnswer();

            if (model.Id > 0)
                question = await _context.QuestionAnswers.FindAsync(model.Id);

            question.NoticeBoardId = model.NoticeBoardId;
            question.QuestionHint1 = model.QuestionHint1;
            question.QuestionHint2 = model.QuestionHint2;
            question.QuestionHint3 = model.QuestionHint3;
            question.Answer = model.Answer;

            if (model.Id == 0)
            {
                _context.QuestionAnswers.Add(question);
                await _context.SaveChangesAsync();

                var student_question = new Data.Entities.StudentQuestions { QuestionId = question.Id, UserId = userId };
                _context.StudentQuestions.Add(student_question);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(QuestionAnswerModel model)
        {
            var question = await _context.QuestionAnswers
                                    .Where(x => x.Id == model.Id).FirstOrDefaultAsync();
           
            if (question.FileId > 0)
            {
                var file = await _context.StaticMedias.FindAsync(question.FileId);
                _context.StaticMedias.Remove(file);
            }

            if (question != null)
            {
                question.Id = model.Id;
                question.NoticeBoardId = model.NoticeBoardId;
                question.QuestionHint1 = model.QuestionHint1;
                question.QuestionHint2 = model.QuestionHint2;
                question.QuestionHint3 = model.QuestionHint3;
                question.Answer = model.Answer;
                question.File = new StaticMedia { ContentLength = model.File.ContentLength, ContentType = model.File.ContentType, FileName = model.File.FileName, InputStream = model.FileInputStream };
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                var studentQuestions = _context.StudentQuestions
                                        .Where(x => x.QuestionId == id);

                if(studentQuestions != null && studentQuestions.Any())
                { 
                _context.StudentQuestions.RemoveRange(studentQuestions);
                }

                await _context.SaveChangesAsync();

                var question = await _context.QuestionAnswers
                                        .Where(x => x.Id == id).FirstOrDefaultAsync();
                if (question != null)
                {
                    _context.QuestionAnswers.Remove(question);
                }

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return true;
        }

        public async Task<StaticMediaModel> GetFile(long fileId)
        {
            return await _context.StaticMedias.Where(x => x.Id == fileId).Select(x => new StaticMediaModel { FileName = x.FileName, ContentLength = x.ContentLength, ContentType = x.ContentType, InputStream = x.InputStream }).FirstOrDefaultAsync();
        }
    }
}
