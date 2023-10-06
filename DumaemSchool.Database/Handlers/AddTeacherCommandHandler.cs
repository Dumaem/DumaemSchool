using DumaemSchool.Core.Commands;
using DumaemSchool.Core.Notifications;
using DumaemSchool.Database.Repositories;
using LanguageExt.Common;
using MediatR;
using Teacher = DumaemSchool.Core.Models.Teacher;

namespace DumaemSchool.Database.Handlers;

public sealed class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Result<Teacher>>
{
    private readonly IPublisher _publisher;
    private readonly ITeacherRepository _repository;

    public AddTeacherCommandHandler(IPublisher publisher, 
        ITeacherRepository repository)
    {
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<Result<Teacher>> Handle(AddTeacherCommand request, 
        CancellationToken cancellationToken)
    {
        var teacher = await _repository.AddTeacherAsync(request.Teacher); 
        var notification = new TeacherCreatedNotification(teacher, request.Email);
        await _publisher.Publish(notification, cancellationToken);

        return teacher;
    }
}