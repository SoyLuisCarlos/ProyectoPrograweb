using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograweb.Models
{
    public class NewsHR
    {
        public int IdNews { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsDescription { get; set; } = null!;
        public string NewsImage { get; set; } = null!;
        public DateTime NewsCreationDate { get; set; }
        public int IdUser { get; set; }
        public int IdNewsCategory { get; set; }
    }
}
