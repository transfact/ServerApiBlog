namespace ServerApiBlog.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string? NickName { get; set; }

        public string? Email { get; set; }
        public  DateTime CreatedDate { set;  get; } 
        public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>(); // Add this line

        public string? Secret { get; set; }
    }
}
