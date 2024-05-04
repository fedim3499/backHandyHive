using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace handyhive_backend.models
{
    public class WorkRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public string Time { get; set; }
        public string? Photo { get; set; }
    }
}
