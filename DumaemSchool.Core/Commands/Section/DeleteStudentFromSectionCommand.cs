using MediatR;

namespace DumaemSchool.Core.Commands.Section
{
    public sealed record DeleteStudentFromSectionCommand(int StudentId, int SectionId) : IRequest<bool>;
}
