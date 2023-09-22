using DumaemSchool.Core.Commands;
using DumaemSchool.Core.Notifications;
using DumaemSchool.Database.Mappers;
using MediatR;
using Teacher = DumaemSchool.Core.Models.Teacher;

namespace DumaemSchool.Database.Handlers;

public sealed class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Teacher?>
{
    private readonly ApplicationContext _context;
    private readonly IPublisher _publisher;

    public AddTeacherCommandHandler(ApplicationContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<Teacher?> Handle(AddTeacherCommand request, 
        CancellationToken cancellationToken)
    {
        var mapper = new DatabaseMapper();
        var teacherDb = mapper.Map(request.Teacher);
        await _context.Teachers.AddAsync(teacherDb, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var teacher = mapper.Map(teacherDb); 
        var notification = new TeacherCreatedNotification(teacher, request.Email);
        await _publisher.Publish(notification, cancellationToken);

        return teacher;
    }
}