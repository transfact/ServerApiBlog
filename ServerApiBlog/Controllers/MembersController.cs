using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ServerApiBlog.Models;
using ServerApiBlog.Models.DTOs;
using ServerApiBlog.Models.DTOs.RequestDTO.cs;
using ServerApiBlog.Utils;

namespace ServerApiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MemberBlogContext _context;

        public MembersController(MemberBlogContext context)
        {
            _context = context;
            if (!context.Members.Any())
            {
                _context.Members.Add(new Member { Email = "member1@example.com", NickName = "Member1", Secret = "secret",CreatedDate = DateTime.Now });
                _context.Members.Add(new Member { Email = "member2@example.com", NickName = "Member2", Secret = "secret", CreatedDate = DateTime.Now });
                _context.Members.Add(new Member { Email = "gijin100@naver.com", NickName = "Lee", Secret = "secret" , CreatedDate = DateTime.Now });

                _context.Blogs.Add(new Blog { MemberId=1 , Post = "본문1" , Secret= "string", Title= "제목1" });
                _context.Blogs.Add(new Blog { MemberId = 1, Post = "본문2", Secret = "string", Title = "제목2" });
                _context.Blogs.Add(new Blog { MemberId = 1, Post = "본문3", Secret = "string", Title = "제목3" });

                _context.SaveChanges();
            }

        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers()
        {
            return await _context.Members.Select(x=> MemberDtoUtils.MemberToDTO(x) ).ToListAsync();
        }

        // Post: api/Members/Login
        [HttpPost("Login")]
        public async Task<ActionResult<MemberDTO>> PostMemberLogin(LoginRequestDTO loginRequestDTO)
        {
            var email = loginRequestDTO.email;
            var member2 = await _context.Members
                .Where(member => member.Email == email)
                .FirstOrDefaultAsync();
            if (member2 == null)
            {
                return NotFound();
            }

            // Add a cookie to the response
            var cookieOptions = new CookieOptions
            {
                // Set cookie properties such as expiration, domain, path, etc.
                Expires = DateTime.Now.AddHours(1),
                // More options can be set based on your requirements
                HttpOnly = false, // This prevents the cookie from being accessed through JavaScript

                SameSite = SameSiteMode.None,
                Secure = true 
            }; 
            Response.Cookies.Append("LoginCookie", member2.Email ?? "gijin100@naver.com", cookieOptions);

            return CreatedAtAction(nameof(GetMember), new { id = member2.MemberId }, MemberDtoUtils.MemberToDTO(member2));
        }
        //
        [HttpGet("Cookie")]
        public async Task<ActionResult<MemberDTO>> GetMembersWithCookie()
        {
            string email = Request.Cookies["LoginCookie"];
            if (email == null || email == "")
            {
                return NotFound();
            }
            var member = await _context.Members.Where(m => m.Email == email).FirstAsync();

            if (member==null)
            {
                return NotFound();
            }
            return MemberDtoUtils.MemberToDTO(member);
        }


        // GET: api/Members/Blog
        [HttpGet("Blog")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembersWithBlogs()
        {
            var membersWithBlogs = await _context.Members
                .Include(member => member.Blogs) //블로그 조인
                .ToListAsync();

            return membersWithBlogs;
        }


        // GET: api/valid
        [HttpGet("valid")]
        public async Task<ActionResult<MemberDTO>> GetMemberByCookie()
        {
            string? email = Request.Cookies["LoginCookie"];
            Console.WriteLine("이메일 쿠키" + email);
            if (email == null) {
                return NotFound();
            }
            var membersByCookie = await _context.Members.Where(x=>x.Email == email) .FirstAsync();

            if (membersByCookie == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetMemberByCookie), new { id = membersByCookie.MemberId }, MemberDtoUtils.MemberToDTO(membersByCookie));
        }


        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return MemberDtoUtils.MemberToDTO(member);
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, MemberDTO memberDTO)
        {
            Member member = MemberDtoUtils.DTO2PutMember(memberDTO);

            var member2 = await _context.Members.FindAsync(id);

            if (member2 == null)
            {
                return BadRequest();
            }

            member2.Email = memberDTO.Email;
            member2.NickName = memberDTO.NickName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MemberDTO>> PostMember(MemberDTO memberDTO)
        {
            var member = MemberDtoUtils.DTO2Member(memberDTO);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
