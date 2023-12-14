using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ServerApiBlog.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Post { get; set; }

        [ForeignKey(name: "MemberId")]
        public int MemberId { get; set; } // Foreign key property
       [JsonIgnore]
        public  Member Member { get; set; }

        public string? Secret { get; set; }
    }
}
