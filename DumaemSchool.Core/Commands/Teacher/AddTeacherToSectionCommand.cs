using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record AddTeacherToSectionCommand(int TeacherId, int SectionId) : IRequest<bool>;