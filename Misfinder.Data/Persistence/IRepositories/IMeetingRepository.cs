using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        IQueryable<Meeting> GetAllMeetings();

        Task<Meeting> GetMeetingById(int? id);

        Task<Meeting> GetMeetingByLostItemId(int? id);

        Task<Meeting> GetMeetingByFoundItemId(int? id);

        Task<Meeting> GetMeetingByIdD(int? id);

        Task<Meeting> GetMeetingByIdL(int? id);
    }
}