using System.ComponentModel.DataAnnotations;

namespace MVC_BlogWebApp.Models.BlogDto
{
    public class BlogViewDto
    {
        public Guid bId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
