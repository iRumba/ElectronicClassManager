using System.Linq.Expressions;
using ElectronicClassManager.Dto.SchoolClass;
using ElectronicClassManager.Entities;
using ElectronicClassManager.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectronicClassManager.Controllers;
[Route("api/SchoolClasses")]
[ApiController]
public class SchoolClassesController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;
    private readonly IStudentService _studentService;

    public SchoolClassesController(ISchoolClassService schoolClassService, IStudentService studentService)
    {
        _schoolClassService = schoolClassService;
        _studentService = studentService;
    }
    // GET: api/<SchoolClassesController>
    [HttpGet("{pseudoName}")]
    public async Task<IActionResult> Get(string pseudoName)
    {
        var entity = await _schoolClassService.GetByPseudoNameAsync(pseudoName);
        
        return Ok(entity);
    }

    [HttpGet]
    public async Task<IActionResult> Find(int? number, string? letter, int? startYear, string? description)
    {
        var filters = new List<Expression<Func<SchoolClass, bool>>>();

        if (number is not null)
            filters.Add(x => x.Number == number);

        if (!string.IsNullOrWhiteSpace(letter))
            filters.Add(x => x.Letter == letter);

        if (startYear is not null)
            filters.Add(x => x.StartYear == startYear);

        if (!string.IsNullOrWhiteSpace(description))
            filters.Add(x => x.Description!.Contains(description));

        return Ok(await _schoolClassService.FindAsync(filters));
    }

    // POST api/<SchoolClassesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SchoolClassCreateDto dto)
    {
        var pseudoName = await GeneratePseudoNameAsync(dto);

        var entity = new SchoolClass
        {
            PseudoName = pseudoName,
            Letter = dto.Letter,
            StartYear = dto.StartYear,
            Description = dto.Description,
            Number = dto.Number
        };

        return Ok(await _schoolClassService.CreateAsync(entity));
    }

    // PUT api/<SchoolClassesController>/5
    [HttpPut("{pseudoName}")]
    public async Task<IActionResult> Put(string pseudoName, [FromBody] SchoolClassUpdateDto dto)
    {
        var entity = await _schoolClassService.GetByPseudoNameAsync(pseudoName);

        if (entity is null)
        {
            return NotFound();
        }

        entity.Description = dto.Description;

        return Ok(await _schoolClassService.UpdateAsync(entity));
    }

    // DELETE api/<SchoolClassesController>/5
    [HttpDelete("{pseudoName}")]
    public async Task<IActionResult> Delete(string pseudoName)
    {
        await _schoolClassService.DeleteByPseudoNameAsync(pseudoName);

        return Ok();
    }

    private async Task<string> GeneratePseudoNameAsync(SchoolClassCreateDto dto)
    {
        var pseudoNameStr = $"{dto.Number}{EngLetter(dto.Letter)}-{dto.StartYear}";

        var pseudoName = pseudoNameStr;

        var counter = 1;
        while (!await _schoolClassService.CanUsePseudoNameAsync(pseudoName))
        {
            pseudoName = $"{pseudoNameStr}-{counter}";
        }

        return pseudoName;
    }

    private string EngLetter(string rusLetter)
    {
        return rusLetter.ToLower() switch
        {
            "а" => "a",
            "б" => "b",
            "в" => "v",
            "г" => "g",
            "д" => "d",
            "е" => "e",
            "ж" => "j",
            "з" => "z",
            "и" => "i",
            "к" => "k",
            "л" => "l",
            "м" => "m",
            "н" => "n",
            "о" => "o",
            "п" => "p",
            "р" => "r",
            "с" => "s",
            "т" => "t",
            "у" => "u",
            "ф" => "f",
            "х" => "h",
            "ц" => "c",
            "ч" => "ch",
            "ш" => "sh",
            "ы" => "y",
            "э" => "ae",
            "ю" => "yu",
            "я" => "ya",
            _ => "-"
        };
    }
}
