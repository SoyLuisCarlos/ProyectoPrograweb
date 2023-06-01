using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoPrograweb.Models.dbModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPrograweb.Models
{
    public class NewsCategoryHR
    {
        public int IdNewsCategory { get; set; }
        public string NewsCategoryDescription { get; set; } = null!;
    }
}
