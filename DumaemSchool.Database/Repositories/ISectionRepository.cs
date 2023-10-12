﻿using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories;

public interface ISectionRepository
{
    public Task<ListDataResult<SectionInfo>> ListSectionInfo(ListParam param);
    public Task<ListDataResult<SectionStudent>> ListSectionStudents(ListParam param);
    public Task<ListDataResult<SectionSchedule>> ListSectionSchedule(ListParam param);
    public Task<ListDataResult<StudentToAddToSection>> ListStudentsToAdd(ListParam param);
}