using DumaemSchool.Core.Commands.Teacher;
using DumaemSchool.Core.Notifications;
using DumaemSchool.Database.Repositories;
using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Database.Handlers.Teacher;

public sealed class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Result<Core.Models.Teacher>>
{
    private readonly IPublisher _publisher;
    private readonly ITeacherRepository _repository;

    public AddTeacherCommandHandler(IPublisher publisher, 
        ITeacherRepository repository)
    {
        _publisher = publisher;
        _repository = repository;
    }

    public async Task<Result<Core.Models.Teacher>> Handle(AddTeacherCommand request, 
        CancellationToken cancellationToken)
    {
        var teacher = await _repository.AddTeacherAsync(request.Teacher); 
        var notification = new TeacherCreatedNotification(teacher, request.Email);
        await _publisher.Publish(notification, cancellationToken);

        return teacher;
    }
}