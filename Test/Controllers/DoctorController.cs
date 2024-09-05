using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.DTOs;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly TablesContext _dbContext;

        public DoctorController (TablesContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{id}")] // Получить запись по ID
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);
            return Ok(doctor);
        }

        [HttpPost] // Создать запись
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = new Doctor
            {
                FullName = dto.FullName,
                RoomId = dto.RoomId,
                SpecializationId = dto.SpecializationId,    
                DistrictId = dto.DistrictId
            };

            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")] // Обновить запись
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] DoctorDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FullName = dto.FullName;
            doctor.RoomId = dto.RoomId;
            doctor.SpecializationId = dto.SpecializationId;
            doctor.DistrictId = dto.DistrictId;

            _dbContext.Doctors.Update(doctor);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")] // Удалить запись 
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet] // Получить список докторов
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetDoctors(
        [FromQuery] string sortField = "FullName",
        [FromQuery] bool order = true,
        [FromQuery] int pageNum = 1,
        [FromQuery] int pageSize = 6)
        {
            var doctor = _dbContext.Doctors
                .Include(d => d.Room)
                .Include(d => d.Specialization)
                .Include(d => d.District)
                .AsQueryable();

            doctor = sortField switch
            {
                "FullName" => order ? doctor.OrderBy(d => d.FullName) : doctor.OrderByDescending(d => d.FullName),
                "RoomName" => order ? doctor.OrderBy(d => d.Room.Number) : doctor.OrderByDescending(d => d.Room.Number),
                "SpecializationName" => order ? doctor.OrderBy(d => d.Specialization.NameOfSpecialization) : doctor.OrderByDescending(d => d.Specialization.NameOfSpecialization),
                "DistrictName" => order ? doctor.OrderBy(d => d.District.Number) : doctor.OrderByDescending(d => d.District.Number),
                _ => doctor.OrderBy(d => d.FullName),
            };

            var doctors = await doctor.Skip((pageNum - 1) * pageSize).Take(pageSize).Select(d => new DoctorListDto
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    RoomId = d.Room.Id,
                    SpecializationName = d.Specialization.NameOfSpecialization,
                    DistrictName = d.District.Number
                })
                .ToListAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}/edit")] // Получить запись для редактирования по ID
        public async Task<IActionResult> GetDoctorByEdit(int id)
        {
            var doctor = await _dbContext.Doctors.Select(x => new DoctorDto
            {
                Id = x.Id,
                FullName = x.FullName,
                RoomId = x.Room.Id,
                SpecializationId = x.SpecializationId,
                DistrictId = x.DistrictId
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

    }
}
