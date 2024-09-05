using Test.Enums;

namespace Test.DTOs
{
    // DTO для создания, редактирования записи, получения по ID
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }   
        public Sex Sex { get; set; }
        public int DistrictId { get; set; } 

    }
}
