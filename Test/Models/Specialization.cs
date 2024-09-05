using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string NameOfSpecialization { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
