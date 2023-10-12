using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Core.Commands.Section
{
    public sealed record AddStudentToSectionCommand(int StudentId, int SectionId): IRequest<bool>;
}
