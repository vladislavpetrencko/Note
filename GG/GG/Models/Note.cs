using System.ComponentModel.DataAnnotations;

namespace GG.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Нeader { get; set; }
        public string Body { get; set; }
        
    }
}
