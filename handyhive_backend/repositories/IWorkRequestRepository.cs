using handyhive_backend.Dto;
using handyhive_backend.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace handyhive_backend.repositories
{
    public interface IWorkRequestRepository
    {
        Task<IEnumerable<WorkRequest>> GetAllWorkRequestsAsync();
        Task<WorkRequest> GetWorkRequestByIdAsync(int id);
        Task<int> CreateWorkRequestAsync(WorkRequest workRequest);
        Task UpdateWorkRequestAsync(WorkRequest workRequest);
        Task DeleteWorkRequestAsync(int id);
    }
}
