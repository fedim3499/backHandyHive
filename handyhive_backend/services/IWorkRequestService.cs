using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using handyhive_backend.Dto;
using handyhive_backend.models;

namespace handyhive_backend.services
{
    public interface IWorkRequestService
    {
        Task<IEnumerable<WorkRequest>> GetAllWorkRequestsAsync();
        Task<WorkRequest> GetWorkRequestByIdAsync(int id);
        Task<int> CreateWorkRequestAsync(WorkRequestDto workRequestDto);
        Task UpdateWorkRequestAsync(int id, WorkRequest workRequestDto);
        Task DeleteWorkRequestAsync(int id);
    }
}
