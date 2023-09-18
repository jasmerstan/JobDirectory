using System.ComponentModel.DataAnnotations;

namespace JobDirectoryWeb.Models.DTO
{
    public class JobDirectoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string SkillSets { get; set; }
        public string Hobby { get; set; }
    }
}
