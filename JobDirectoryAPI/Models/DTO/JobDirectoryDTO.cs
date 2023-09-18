using System.ComponentModel.DataAnnotations;

namespace JobDirectoryAPI.Models.DTO
{
    public class JobDirectoryDTO
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public EmailAddressAttribute Email { get; set; }
        public PhoneAttribute PhoneNumber { get; set; }
        public string SkillSets { get; set; }
        public string Hobby { get; set; }
    }
}
