using Test.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string FisrtName { get; set; }
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {  get; set; }
        [Required]
        public Sex Sex { get; set; }
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        
    }
}
