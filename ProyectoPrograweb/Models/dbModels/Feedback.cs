using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograweb.Models.dbModels
{
    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        public int IdFeedback { get; set; }
        public string FeedbackComment { get; set; } = null!;
        public string FeedbackName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime FeedbackDate { get; set; }
        public int FeedbackPhone { get; set; }
        public string FeedbackEmail { get; set; } = null!;
    }
}
