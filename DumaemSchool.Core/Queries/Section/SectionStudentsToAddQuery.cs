using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Section;

public sealed record SectionStudentsToAddQuery(ListParam Param) : IRequest<ListDataResult<StudentToAddToSection>>;