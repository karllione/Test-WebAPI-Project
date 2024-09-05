using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Number { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}
