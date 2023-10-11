using MediatR;

namespace DumaemSchool.Core.Queries.Teacher;

public sealed record TeacherInfoQuery(int TeacherId) : IRequest<Models.Teacher?>;