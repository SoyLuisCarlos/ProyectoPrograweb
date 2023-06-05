using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograweb.Models.dbModels
{
    public partial class News
    {
        public News()
        {
            IdUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int IdNews { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsDescription { get; set; } = null!;
        public string NewsImage { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime NewsCreationDate { get; set; }
        public int IdUser { get; set; }
        public int IdNewsCategory { get; set; }

        [ForeignKey("IdNewsCategory")]
        [InverseProperty("News")]
        public virtual NewsCategory IdNewsCategoryNavigation { get; set; } = null!;
        [ForeignKey("IdUser")]
        [InverseProperty("News")]
        public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

        [ForeignKey("IdNews")]
        [InverseProperty("IdNews")]
        public virtual ICollection<ApplicationUser> IdUsers { get; set; }


    }
}
