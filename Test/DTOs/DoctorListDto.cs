namespace Test.DTOs
{
    // DTO для получения списка записей
    public class DoctorListDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int RoomId { get; set; }
        public string? SpecializationName { get; set; } 
        public string? DistrictName { get; set; } 
    }
}
