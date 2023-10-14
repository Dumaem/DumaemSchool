using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record EditTeacherNameCommand(Models.Teacher Teacher) : IRequest<bool>;