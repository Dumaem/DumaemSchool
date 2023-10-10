using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record SectionsInfoQuery(ListParam Param) : IRequest<ListDataResult<SectionInfo>>;