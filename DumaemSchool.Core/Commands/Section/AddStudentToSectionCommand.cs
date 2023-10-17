using MediatR;

namespace DumaemSchool.Core.Commands.Section
{
    public sealed record AddStudentToSectionCommand(int StudentId, int SectionId): IRequest<bool>;
}
