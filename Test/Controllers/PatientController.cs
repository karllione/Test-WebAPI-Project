using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.DTOs;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Globalization;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly TablesContext _dbContext;
        public PatientController(TablesContext dbContext) 
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{id}")] // Получить запись по ID
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            return Ok(patient);
        }

        [HttpPost] // Создать запись
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto dto) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = new Patient
            {
                FisrtName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
                Sex = dto.Sex,
                DistrictId = dto.DistrictId
            };

            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")] // Обновить запись
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _dbContext.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            patient.FisrtName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.Address = dto.Address;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Sex = dto.Sex;
            patient.DistrictId = dto.DistrictId;

            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")] // Удалить запись 
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            _dbContext.Patients.Remove(patient);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet] // Получить список пациентов
        public async Task<IActionResult> GetPatients(
            [FromQuery] string sortString = "LastName",
            [FromQuery] int pageNum = 1,
            [FromQuery] int pageSize = 6,
            [FromQuery] bool order = true // порядок сортировки
            )
        {
            var patient = _dbContext.Patients.Select(x => new PatientListDto
            {
                Id = x.Id,  
                FirstName = x.FisrtName,
                LastName = x.LastName,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                Sex = x.Sex,
                DistrictName = x.District.Number
            });

            patient = sortString switch
            {
                "FirstName" => order ? patient.OrderBy(x => x.FirstName) : patient.OrderByDescending(x => x.FirstName),
                "DateOfBirth" => order ? patient.OrderBy(x => x.DateOfBirth) : patient.OrderByDescending(x => x.DateOfBirth),
                "LastName" => order ? patient.OrderBy(x => x.LastName) : patient.OrderByDescending(x => x.LastName)
            };

            var patients = await patient.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(patients);
        }

        [HttpGet("{id}/edit")] // Получить запись для редактирования по ID
        public async Task<IActionResult> GetPatientByEdit(int id)
        {
            var patient = await _dbContext.Patients.Select(x => new PatientDto
            {
                Id = x.Id,
                FirstName = x.FisrtName,
                LastName = x.LastName,  
                Address = x.Address,   
                DateOfBirth = x.DateOfBirth,
                Sex = x.Sex,
                DistrictId = x.DistrictId
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
    }
}
