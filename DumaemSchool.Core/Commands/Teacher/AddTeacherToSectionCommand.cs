using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Core.Commands.Teacher
{
    public sealed record AddTeacherToSectionCommand(int TeacherId, int SectionId) : IRequest<bool>;
}
