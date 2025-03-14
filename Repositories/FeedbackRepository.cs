using EventManagementWithAuthentication.Data;
using EventManagementWithAuthentication.Interfaces;
using EventManagementWithAuthentication.Models;

namespace EventManagementWithAuthentication.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return _context.Feedbacks.ToList();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
