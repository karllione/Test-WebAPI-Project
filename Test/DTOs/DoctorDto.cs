namespace Test.DTOs
{
    // DTO для создания, редактирования записи, получения по ID
    public class DoctorDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int RoomId{ get; set; }
        public int SpecializationId { get; set; } 
        public int DistrictId { get; set; } 
    }
}
