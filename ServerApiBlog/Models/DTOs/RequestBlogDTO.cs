using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApiBlog.Models.DTOs
{
    public class RequestBlogDTO
    {
        public string? Title { get; set; }
        public string? Post { get; set; }


    }
}
