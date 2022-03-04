using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PupilRegister.Models.Entities
{
    [Table("Pupil")]
    public class Pupil
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School School { get; set; } 
    }
}
