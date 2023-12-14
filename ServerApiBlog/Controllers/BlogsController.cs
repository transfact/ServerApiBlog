using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApiBlog.Models;
using ServerApiBlog.Models.DTOs;
using ServerApiBlog.Utils;
namespace ServerApiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly MemberBlogContext _context;

        public BlogsController(MemberBlogContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            return await _context.Blogs.Select(x=>BlogDtoUtils.Blog2DTO(x)).ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            
            return BlogDtoUtils.Blog2DTO( blog);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(RequestBlogDTO blogDTO)
        {
            //id로 찾고
            var member = await _context.Members.FindAsync(blogDTO.MemberId);

            if (member == null)
            {
                // Handle the case where the associated member is not found
                return NotFound("Member not found");
            }

            // blogDTO를 blog로 바꾸고
            var blog = BlogDtoUtils.DTO2PostBlog(blogDTO);

            // Blog를 set
            blog.Member = member;
            // add
            _context.Blogs.Add(blog);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlog), new { id = blog.BlogId }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
