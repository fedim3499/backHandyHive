using handyhive_backend.Dto;
using handyhive_backend.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace handyhive_backend.repositories
{
    public class WorkRequestRepository : IWorkRequestRepository
    {
        private readonly AppDbContext _context;

        public WorkRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateWorkRequestAsync(WorkRequest workRequest)
        {
            _context.WorkRequests.Add(workRequest);
            await _context.SaveChangesAsync();

            return workRequest.Id;
        }

        public async Task DeleteWorkRequestAsync(int id)
        {
            var workRequestToDelete = await _context.WorkRequests.FindAsync(id);
            if (workRequestToDelete != null)
            {
                _context.WorkRequests.Remove(workRequestToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WorkRequest>> GetAllWorkRequestsAsync()
        {
            return await _context.WorkRequests.ToListAsync();
        }

        public async Task<WorkRequest> GetWorkRequestByIdAsync(int id)
        {
            return await _context.WorkRequests.FindAsync(id);
        }

        public async Task UpdateWorkRequestAsync(WorkRequest workRequest)
        {
            _context.WorkRequests.Update(workRequest);
            await _context.SaveChangesAsync();
        }
    }
}
