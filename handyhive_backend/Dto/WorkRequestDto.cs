using Microsoft.AspNetCore.Http;
using System;

namespace handyhive_backend.Dto
{
    public class WorkRequestDto
    {
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public String Time { get; set; }
        public string Photo { get; set; } // Added property for photo upload
        // Add other properties from your Flutter code
    }
}
