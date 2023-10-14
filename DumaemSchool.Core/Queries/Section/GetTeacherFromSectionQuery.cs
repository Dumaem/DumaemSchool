using DumaemSchool.Core.OutputModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Core.Queries.Section
{
    public sealed record GetTeacherFromSectionQuery(int SectionId) : IRequest<TeacherDto>;
}
