using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string? FullName { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        [ForeignKey("District")]
        public int DistrictId { get; set; } 

        public virtual Room? Room { get; set; }  
        public virtual Specialization? Specialization { get; set; }
        public virtual District? District { get; set; }
    }
}
