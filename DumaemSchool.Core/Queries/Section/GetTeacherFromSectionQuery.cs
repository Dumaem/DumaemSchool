using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Section
{
    public sealed record GetTeacherFromSectionQuery(int SectionId) : IRequest<TeacherDto>;
}
