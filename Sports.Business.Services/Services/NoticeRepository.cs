using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Data.Entities;
using ChatBot.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Services
{
    public class NoticesRepository : INoticeRepository
    {
        private readonly ChatBotContext _context;
        public NoticesRepository(ChatBotContext context)
        {
            _context = context;
        }
        public async Task<NoticeModel> GetNotice(long id)
        {
            var notice = await _context.NoticeBoards
                                     .Where(x => x.Id == id)
                                     .Select(x => new NoticeModel { Id = x.Id, Name = x.Name, NoticeDateTime=x.NoticeDateTime,Description=x.Description}).FirstOrDefaultAsync();

            return notice;
        }
        public async Task<List<NoticeModel>> GetNotices()
        {
            var notices = await _context.NoticeBoards
                                     .Select(x => new NoticeModel { Id = x.Id, Name = x.Name, NoticeDateTime = x.NoticeDateTime, Description = x.Description }).ToListAsync();

            return notices;
        }
        public async Task<List<SelectListItem>> GetNoticeSelectList()
        {
            var notices = await _context.NoticeBoards
                                   .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name,  }).ToListAsync();

            return notices;
        }
        public async Task<bool> Insert(NoticeModel model)
        {
            var notice = new NoticeBoard { Name = model.Name, NoticeDateTime = model.NoticeDateTime, Description = model.Description };
            _context.NoticeBoards.Add(notice);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Update(NoticeModel model)
        {
            var notice = await _context.NoticeBoards.FindAsync(model.Id);

            if (notice != null)
            {
                notice.Name = model.Name;

            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Delete(long Id)
        {
            var notice = await _context.NoticeBoards
                                    .Where(x => x.Id == Id).FirstOrDefaultAsync();

            _context.NoticeBoards.Remove(notice);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<NoticeModel>> GetNoticesWithinDate(DateTime fromDate, DateTime toDate)
        {
            var notice = await _context.NoticeBoards
                                     .Where(x => DbFunctions.TruncateTime(x.NoticeDateTime) >= DbFunctions.TruncateTime(fromDate) && DbFunctions.TruncateTime(x.NoticeDateTime) <= DbFunctions.TruncateTime(toDate))
                                     .Select(x => new NoticeModel { Id = x.Id, Name = x.Name, NoticeDateTime = x.NoticeDateTime, Description = x.Description }).ToListAsync();

            return notice.Distinct().OrderBy(x => x.Name).ToList();
        }
    }
}
