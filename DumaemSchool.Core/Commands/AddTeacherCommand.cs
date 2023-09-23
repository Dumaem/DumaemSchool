using DumaemSchool.Core.Commands.Base;
using DumaemSchool.Core.Models;
using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record AddTeacherCommand(Teacher Teacher, string Email) : IRequest<Result<Teacher>>, ITransactRequest;