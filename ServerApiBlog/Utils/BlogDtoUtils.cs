using ServerApiBlog.Models.DTOs;
using ServerApiBlog.Models;

namespace ServerApiBlog.Utils
{
    public class BlogDtoUtils
    {
        public static Blog DTO2Blog(BlogDTO b) =>
           new Blog
           {
               Post = b.Post,
               Title= b.Title,
               Secret = "Secret"
           };

        public static Blog DTO2PostBlog(RequestBlogDTO b) =>
           new Blog
           {
               Post = b.Post,
               Title = b.Title,
               Secret = "Secret"
           };

        public static BlogDTO Blog2DTO(Blog b) =>
           new BlogDTO
           {
               Post = b.Post,
               Title = b.Title,
               BlogId = b.BlogId,
           };
    }
}
