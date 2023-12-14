
using System.Text.Json.Serialization;
namespace ServerApiBlog.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string? NickName { get; set; }

        public string? Email { get; set; }
        public  DateTime CreatedDate { set;  get; } 
        [JsonIgnore]
        public  ICollection<Blog> Blogs { get; set; }

        public string? Secret { get; set; }
    }
}
