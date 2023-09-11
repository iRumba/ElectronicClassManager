using ElectronicClassManager.Entities;
using ElectronicClassManager.Entities.Storage;

namespace ElectronicClassManager.Services.Implementations;

public class StudentService : ServiceBase<Student>, IStudentService
{
    public StudentService(EntitiesDbContext dbContext) : base(dbContext)
    {
        
    }

    ///// <inheritdoc />
    //protected override Expression<Func<Student, StudentOutDto>> EntityToOutDtoMappingExpression { get; } = x =>
    //    new StudentOutDto
    //    {
    //        Id = x.Id,
    //        FirstName = x.FirstName,
    //        LastName = x.LastName,
    //        Patronymic = x.Patronymic,
    //        Gender = x.Gender,
    //        BirthDate = x.BirthDate,
    //        ClassPseudoName = x.SchoolClass.PseudoName
    //    };

    ///// <inheritdoc />
    //protected override Student MapCreationDtoToEntity(StudentCreateDto dto)
    //{
    //    return new Student
    //    {
    //        FirstName = dto.FirstName,
    //        LastName = dto.LastName,
    //        Patronymic = dto.Patronymic,
    //        Gender = dto.Gender,
    //        BirthDate = dto.BirthDate,
    //        SchoolClass = DbContext.Find<SchoolClass>(dto.SchoolClassId)
    //    };
    //}

    ///// <inheritdoc />
    //protected override void UpdateEntityFromDto(Student entity, StudentUpdateDto dto)
    //{
    //    entity.FirstName = dto.FirstName;
    //    entity.LastName = dto.LastName;
    //    entity.Patronymic = dto.Patronymic;
    //    entity.Gender = dto.Gender;
    //    entity.BirthDate = dto.BirthDate;
    //    entity.SchoolClass = dto.SchoolClass;
    //}

    ///// <inheritdoc />
    //protected override IQueryable<Student> ApplyFindFilter(IQueryable<Student> query, StudentFindDto dto)
    //{
    //    query = query.Where(x => x.SchoolClass.PseudoName == dto.ClassPseudoName);

    //    if (!string.IsNullOrEmpty(dto.FirstName))
    //    {
    //        query = query.Where(x => x.FirstName == dto.FirstName);
    //    }

    //    if (!string.IsNullOrEmpty(dto.LastName))
    //    {
    //        query = query.Where(x => x.LastName == dto.LastName);
    //    }

    //    if (!string.IsNullOrWhiteSpace(dto.Patronymic))
    //    {
    //        query = query.Where(x => x.Patronymic == dto.Patronymic);
    //    }

    //    if (dto.Gender is not null)
    //    {
    //        query = query.Where(x => x.Gender == dto.Gender);
    //    }

    //    if (dto.BirthDate is not null)
    //    {
    //        query = query.Where(x => x.BirthDate == dto.BirthDate);
    //    }

    //    return query;
    //}
}