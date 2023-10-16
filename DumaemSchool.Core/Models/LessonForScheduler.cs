using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Core.Models
{
    public class LessonForScheduler
    {
        public DateTime LessonStart { get; set; }

        public TimeSpan Duration { get; set; }

        public string? SectionGroupName { get; set; }

        public DateTime LessonEnd => LessonStart.Add(Duration);

        public bool IsReplacement { get; set; }

        public bool IsCanceled { get; set; }
    }
}
