using ElectronicClassManager.Services;
using ElectronicClassManager.Services.Dto.SchoolClass;
using Microsoft.AspNetCore.Mvc;
using SchoolClassCreateDto = ElectronicClassManager.Dto.SchoolClassCreateDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectronicClassManager.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SchoolClassesController : ControllerBase
{
    private readonly ISchoolClassService _schoolClassService;

    public SchoolClassesController(ISchoolClassService schoolClassService)
    {
        _schoolClassService = schoolClassService;
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
        var sDto = new SchoolClassFindDto
        {
            Description = description,
            Number = number,
            Letter = letter,
            StartYear = startYear
        };

        return Ok(await _schoolClassService.FindAsync(sDto));
    }

    // POST api/<SchoolClassesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Dto.SchoolClassCreateDto dto)
    {
        var pseudoName = await GeneratePseudoNameAsync(dto);

        var sDto = new Services.Dto.SchoolClass.SchoolClassCreateDto
        {
            PseudoName = pseudoName,
            Description = dto.Description,
            Letter = dto.Letter,
            Number = dto.Number,
            StartYear = dto.StartYear
        };

        return Ok(await _schoolClassService.CreateAsync(sDto));
    }

    // PUT api/<SchoolClassesController>/5
    [HttpPut("{pseudoName}")]
    public async Task<IActionResult> Put(string pseudoName, [FromBody] Dto.SchoolClassUpdateDto dto)
    {
        var sDto = new SchoolClassUpdateDto
        {
            Description = dto.Description
        };

        return Ok(await _schoolClassService.UpdateByPseudoNameAsync(pseudoName, sDto));
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
