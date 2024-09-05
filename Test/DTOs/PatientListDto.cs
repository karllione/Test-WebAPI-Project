using Test.Enums;

namespace Test.DTOs
{
    public class PatientListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
        public string DistrictName { get; set; }
    }
}
