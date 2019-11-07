using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MisFinderDbContext context;

        public MeetingRepository(MisFinderDbContext context)
        {
            this.context = context;
        }

        public void Create(Meeting entity)
        {
            context.Meetings.Add(entity);
        }

        public void Delete(Meeting entity)
        {
            context.Remove(entity);
        }

        public async Task Delete(int id)
        {
            var meeting = await context.Meetings.SingleOrDefaultAsync(m => m.Id == id);
            context.Meetings.Remove(meeting);
        }

        public IQueryable<Meeting> GetAllMeetings()
        {
            IQueryable<Meeting> meetings = context.Meetings.Include(c => c.FoundItem).
                Include(c => c.LostItem);
            return meetings;
        }

        public async Task<Meeting> GetMeetingByFoundItemId(int? id)
        {
            return await context.Meetings.
                 Include(c => c.FoundItem)
                 .Include(c => c.LostItem).FirstOrDefaultAsync(c => c.FoundItem.Id == id);
        }

        public async Task<Meeting> GetMeetingByLostItemId(int? id)
        {
            return await context.Meetings.
                Include(c => c.LostItem).FirstOrDefaultAsync(c => c.FoundItem.Id == id);
        }

        public async Task<Meeting> GetMeetingById(int? id)
        {
            return await context.Meetings.Include(c => c.FoundItem)
                .Include(c => c.LostItem).SingleOrDefaultAsync(c => c.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Meeting entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}