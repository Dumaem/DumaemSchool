using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Mappers;
using MediatR;
using Teacher = DumaemSchool.Core.Models.Teacher;

namespace DumaemSchool.Database.Handlers;

public sealed class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Teacher?>
{
    private readonly ApplicationContext _context;

    public AddTeacherCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Teacher?> Handle(AddTeacherCommand request, 
        CancellationToken cancellationToken)
    {
        var mapper = new DatabaseMapper();
        var teacherDb = mapper.Map(request.Teacher);
        await _context.Teachers.AddAsync(teacherDb, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return mapper.Map(teacherDb);
    }
}