using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace ProyectoPrograweb.Models.dbModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            News = new HashSet<News>();
            IdNews = new HashSet<News>();
        }

        [Key]
        public string UserImage { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime UserCreationDate { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<News> IdNews { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}

