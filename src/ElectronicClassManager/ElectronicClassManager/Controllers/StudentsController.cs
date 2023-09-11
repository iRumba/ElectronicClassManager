using ElectronicClassManager.Dto.Student;
using ElectronicClassManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicClassManager.Controllers;

[Route("api/Students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;
    private readonly IStudentService _studentService;

    public StudentsController(ISchoolClassService schoolClassService, IStudentService studentService)
    {
        _schoolClassService = schoolClassService;
        _studentService = studentService;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, StudentUpdateDto dto)
    {
        var entity = await _studentService.GetByIdAsync(id);

        if (entity is null)
            return NotFound();

        entity.BirthDate = dto.BirthDate;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Patronymic = dto.Patronymic;
        entity.Gender = dto.Gender;

        return Ok(await _studentService.UpdateAsync(entity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(await _studentService.DeleteByIdAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _studentService.GetByIdAsync(id));
    }
}
