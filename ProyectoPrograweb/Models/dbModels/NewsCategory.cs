using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograweb.Models.dbModels
{
    [Table("NewsCategory")]
    public partial class NewsCategory
    {
        public NewsCategory()
        {
            Matches = new HashSet<Match>();
            News = new HashSet<News>();
        }

        [Key]
        public int IdNewsCategory { get; set; }
        public string NewsCategoryDescription { get; set; } = null!;

        [InverseProperty("IdNewsCategoryNavigation")]
        public virtual ICollection<Match> Matches { get; set; }
        [InverseProperty("IdNewsCategoryNavigation")]
        public virtual ICollection<News> News { get; set; }
    }
}
