using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograweb.Models.dbModels
{
    public partial class Match
    {
        [Key]
        public int IdMatch { get; set; }
        [StringLength(50)]
        public string MatchTitle { get; set; } = null!;
        public string MatchImage { get; set; } = null!;
        public string MatchDescription { get; set; } = null!;
        public int IdNewsCategory { get; set; }

        [ForeignKey("IdNewsCategory")]
        [InverseProperty("Matches")]
        public virtual NewsCategory IdNewsCategoryNavigation { get; set; } = null!;
    }
}
