using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApiBlog.Models.DTOs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Post { get; set; }

        public int MemberId { get; set; }

    }
}
