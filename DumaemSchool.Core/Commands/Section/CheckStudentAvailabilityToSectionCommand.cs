using DumaemSchool.Core.OutputModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Core.Commands.Section
{
    public sealed record CheckStudentAvailabilityToSectionCommand(int StudentId, List<SectionSchedule> SectionSchedule) : IRequest<bool>
    {
    }
}
