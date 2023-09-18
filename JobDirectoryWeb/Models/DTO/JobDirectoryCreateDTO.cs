using System.ComponentModel.DataAnnotations;

namespace JobDirectoryWeb.Models.DTO
{
    public class JobDirectoryCreateDTO
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string SkillSets { get; set; }
        public string Hobby { get; set; }
    }
}
