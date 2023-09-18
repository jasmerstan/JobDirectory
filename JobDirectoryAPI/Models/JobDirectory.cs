using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace JobDirectoryAPI.Models
{
    public class JobDirectory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public EmailAddressAttribute Email { get; set; }
        public PhoneAttribute PhoneNumber { get; set; }
        public string SkillSets { get; set; }
        public string Hobby { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
