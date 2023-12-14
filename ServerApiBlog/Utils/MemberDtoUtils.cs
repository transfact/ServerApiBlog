using ServerApiBlog.Models.DTOs;
using ServerApiBlog.Models;

namespace ServerApiBlog.Utils
{
    public class MemberDtoUtils
    {
        public static MemberDTO MemberToDTO(Member m) =>
         new MemberDTO
         {
             MemberId = m.MemberId,
             Email = m.Email,
             NickName = m.NickName
         };

        public static Member DTO2Member(MemberDTO m) =>
           new Member
           {
               MemberId = m.MemberId,
               Email = m.Email,
               NickName = m.NickName,
               CreatedDate = DateTime.Now,
               Secret = "false"
           };

        public static Member DTO2PutMember(MemberDTO m) =>
           new Member
           {
               MemberId = m.MemberId,
               Email = m.Email,
               NickName = m.NickName,
               Secret = "false"
           };
    }
}
