using DumaemSchool.Core.Models;
using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record EditTeacherNameCommand(Teacher Teacher) : IRequest<bool>;