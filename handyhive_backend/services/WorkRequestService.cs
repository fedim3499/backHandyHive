using Azure.Core;
using handyhive_backend.Dto;
using handyhive_backend.models;
using handyhive_backend.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace handyhive_backend.services
{
    public class WorkRequestService : IWorkRequestService
    {
        private readonly IWorkRequestRepository _repository;

        public WorkRequestService(IWorkRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateWorkRequestAsync(WorkRequestDto workRequestDto)


        {

            var workrequest = new WorkRequest
            {
               Description= workRequestDto.Description,
               Date = workRequestDto.Date,
               Time=    workRequestDto.Time,
               Photo=workRequestDto.Photo,
            };
            return await _repository.CreateWorkRequestAsync(workrequest);
        }

        public async Task DeleteWorkRequestAsync(int id)
        {
            await _repository.DeleteWorkRequestAsync(id);
        }

        public async Task<IEnumerable<WorkRequest>> GetAllWorkRequestsAsync()
        {
            return await _repository.GetAllWorkRequestsAsync();
        }

        public async Task<WorkRequest> GetWorkRequestByIdAsync(int id)
        {
            return await _repository.GetWorkRequestByIdAsync(id);
        }

        public async Task UpdateWorkRequestAsync(int id, WorkRequest workRequestDto)
        {
            var existingWorkRequest = await _repository.GetWorkRequestByIdAsync(id);
            if (existingWorkRequest != null)
            {
                // Update properties of existingWorkRequest with values from workRequestDto
                existingWorkRequest.Description = workRequestDto.Description;
                existingWorkRequest.Date = workRequestDto.Date;
                existingWorkRequest.Time = workRequestDto.Time;
                // Update other properties as needed

                await _repository.UpdateWorkRequestAsync(existingWorkRequest);
            }
        }
    }
}
