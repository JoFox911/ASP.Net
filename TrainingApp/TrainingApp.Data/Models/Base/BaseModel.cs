using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingApp.Data.Enums;

namespace TrainingApp.Data.Base.Models
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public RecordState RecordState { get; set; } = RecordState.N;
    }
}
