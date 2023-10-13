using System.Reflection;
using Dapper;
using DumaemSchool.Core.Commands.Teacher;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using DumaemSchool.Database.ListGetters.Impl;
using DumaemSchool.Database.Mappers.EntityMapping;
using DumaemSchool.Database.Mappers.EntityMapping.Base;
using DumaemSchool.Database.PipelineBehaviors;
using DumaemSchool.Database.Repositories;
using DumaemSchool.Database.Repositories.Impl;
using DumaemSchool.Database.TypeHandlers;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;
using SectionType = DumaemSchool.Core.Models.SectionType;

namespace DumaemSchool.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        if (connectionString is null)
            throw new Exception("No database connection string");

        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions => { npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSnakeCaseNamingConvention();
        });

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());
        services.AddScoped<NpgsqlConnectionProvider>();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            options
                .AddBehavior<IPipelineBehavior<AddTeacherCommand, Result<Core.Models.Teacher>>,
                    TransactPipelineBehavior<AddTeacherCommand, Core.Models.Teacher>>();
        });

        services.AddScoped<ISectionTypeRepository, SectionTypeRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddSingleton<IListSqlGenerator<TeacherDto>, TeacherListSqlGenerator>();
        services.AddSingleton<IListSqlGenerator<SectionType>, SectionTypeListSqlGenerator>();
        services.AddSingleton<IListSqlGenerator<SectionInfo>, SectionInfoListSqlGenerator>();
        services.AddSingleton<IListSqlGenerator<SectionStudent>, SectionStudentListSqlGetter>();
        services.AddSingleton<IListSqlGenerator<SectionSchedule>, SectionScheduleListSqlGetter>();
        services.AddSingleton<IListSqlGenerator<StudentLessonStatistics>, StudentLessonStatisticsListSqlGetter>();
        services.AddSingleton<IListSqlGenerator<StudentToAddToSection>, SectionStudentsToAddListSqlGetter>();
        services.AddSingleton<IListSqlGenerator<SectionStudentStatistics>, SectionStudentStatisticsListSqlGetter>();
        services.AddSingleton<IEntityMapping<TeacherDto>, TeacherDtoEntityMapping>();
        services.AddSingleton<IEntityMapping<SectionType>, SectionTypeEntityMapping>();
        services.AddSingleton<IEntityMapping<SectionInfo>, SectionInfoEntityMapping>();
        services.AddSingleton<IEntityMapping<SectionStudent>, SectionStudentEntityMapping>();
        services.AddSingleton<IEntityMapping<SectionSchedule>, SectionScheduleEntityMapping>();
        services.AddSingleton<IEntityMapping<StudentLessonStatistics>, StudentLessonStatisticsEnityMapping>();
        services.AddSingleton<IEntityMapping<StudentToAddToSection>, StudentToAddToSectionEntityMapping>();
        services.AddSingleton<IEntityMapping<SectionStudentStatistics>, SectionStudentStatisticsEntityMapping>();

        return services;
    }
}