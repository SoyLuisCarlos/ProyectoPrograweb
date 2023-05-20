using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograweb.Models
    {
        public class MatchHR
        {
            public int IdMatch { get; set; }
            public string MatchTitle { get; set; } = null!;
            public string MatchDescription { get; set; } = null!;
            public string MatchImage { get; set; } = null!;
            public int IdNewsCategory { get; set; }
        }
    }


