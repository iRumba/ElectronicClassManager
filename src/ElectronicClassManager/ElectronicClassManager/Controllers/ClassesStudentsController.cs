using System.Security.Cryptography.X509Certificates;
using ElectronicClassManager.Dto.Student;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicClassManager.Controllers;


[Route("api/SchoolClasses/{classPseudoName}")]
[ApiController]
public class ClassesStudentsController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;
    private readonly IStudentService _studentService;

    public ClassesStudentsController(ISchoolClassService schoolClassService, IStudentService studentService)
    {
        _schoolClassService = schoolClassService;
        _studentService = studentService;
    }

    [HttpGet("Students")]
    public async Task<IActionResult> GetStudentsAsync(string classPseudoName)
    {
        var items = await _studentService.FindAsync(x => x.SchoolClass.PseudoName == classPseudoName);

        return Ok(items.Select(res => new StudentOutDto
        {
            Id = res.Id,
            Patronymic = res.Patronymic,
            Gender = res.Gender,
            FirstName = res.FirstName,
            LastName = res.LastName,
            BirthDate = res.BirthDate,
            ClassPseudoName = res.SchoolClass.PseudoName
        }));
    }

    [HttpPost("Students")]
    public async Task<IActionResult> CreateStudent(string classPseudoName, StudentCreateDto dto)
    {
        var schoolClass = await _schoolClassService.GetByPseudoNameAsync(classPseudoName);

        if (schoolClass is null)
        {
            return NotFound();
        }

        var res = await _studentService.CreateAsync(new Student
        {
            BirthDate = dto.BirthDate,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            SchoolClass = schoolClass
        });

        return Ok(new StudentOutDto
        {
            Id = res.Id,
            Patronymic = res.Patronymic,
            Gender = res.Gender,
            FirstName = res.FirstName,
            LastName = res.LastName,
            BirthDate = res.BirthDate,
            ClassPseudoName = schoolClass.PseudoName
        });
    }
}