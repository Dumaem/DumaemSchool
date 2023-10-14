using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Teacher;

public sealed record TeachersQuery(ListParam Params) : IRequest<ListDataResult<TeacherDto>>;