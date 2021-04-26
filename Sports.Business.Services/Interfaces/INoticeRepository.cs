using ChatBot.Business.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Interfaces
{
    public interface INoticeRepository
    {
        Task<NoticeModel> GetNotice(long Id);
        Task<List<NoticeModel>> GetNotices();
        Task<List<SelectListItem>> GetNoticeSelectList();
        Task<bool> Insert(NoticeModel model);
        Task<bool> Update(NoticeModel model);
        Task<bool> Delete(long id);
        Task<List<NoticeModel>> GetNoticesWithinDate(DateTime fromDate, DateTime toDate);
    }
}
