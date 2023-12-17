using System.ComponentModel.DataAnnotations;

namespace ServerApiBlog.Models.DTOs.RequestDTO.cs
{
    public class LoginRequestDTO
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string pw { get; set; }

    }
}
