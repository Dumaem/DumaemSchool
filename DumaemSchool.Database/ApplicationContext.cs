using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; } = null!;

    public virtual DbSet<Attendance> Attendances { get; set; } = null!;

    public virtual DbSet<Lesson> Lessons { get; set; } = null!;

    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<Schedule> Schedules { get; set; } = null!;

    public virtual DbSet<Section> Sections { get; set; } = null!;

    public virtual DbSet<SectionStudent> SectionStudents { get; set; } = null!;

    public virtual DbSet<SectionTeacher> SectionTeachers { get; set; } = null!;

    public virtual DbSet<SectionType> SectionTypes { get; set; } = null!;

    public virtual DbSet<Student> Students { get; set; } = null!;

    public virtual DbSet<Teacher> Teachers { get; set; } = null!;

    public virtual DbSet<TeacherLink> TeacherLinks { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("activity_pkey");

            entity.ToTable("activity", tb => tb.HasComment("Сущность Активность ученика"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.Mark)
                .HasComment("Оценка активности")
                .HasColumnName("mark");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Activities)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activity_lesson_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Activities)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("activity_student_id_fkey");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attendance_pkey");

            entity.ToTable("attendance", tb => tb.HasComment("Сущность Посещаемость ученика, наличие записи означает отсутствие ученика на занятии"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attendance_lesson_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("attendance_student_id_fkey");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lesson_pkey");

            entity.ToTable("lesson", tb => tb.HasComment("Сущность Занятие"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IsConducted)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasComment("Было или будет ли проведено занятие")
                .HasColumnName("is_conducted");
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.TeacherId)
                .HasComment("Учитель, фактически проводивший занятие (может расходиться со связкой к кружку)")
                .HasColumnName("teacher_id");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lesson_schedule_id_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lesson_teacher_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role", "auth", tb => tb.HasComment("Роль пользователя"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("schedule_pkey");

            entity.ToTable("schedule", tb => tb.HasComment("Сущность Расписание"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cabinet)
                .HasComment("Кабинет, в котором будет проводиться занятие")
                .HasColumnName("cabinet");
            entity.Property(e => e.DayOfWeek)
                .HasComment("День недели (0 - понедельник, 6 - воскресенье)")
                .HasColumnName("day_of_week");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.Time)
                .HasComment("Время проведения")
                .HasColumnName("time");

            entity.HasOne(d => d.Section).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedule_section_id_fkey");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("section_pkey");

            entity.ToTable("section", tb => tb.HasComment("Сущность Кружок"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDeleted).HasColumnName("date_deleted");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .HasComment("Название группы студентов")
                .HasColumnName("group_name");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.SectionTypeId).HasColumnName("section_type_id");

            entity.HasOne(d => d.SectionType).WithMany(p => p.Sections)
                .HasForeignKey(d => d.SectionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_section_type_id_fkey");
        });

        modelBuilder.Entity<SectionStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("section_student_pkey");

            entity.ToTable("section_student", tb => tb.HasComment("Связка кружка и ученика, который в нем занимается"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Section).WithMany(p => p.SectionStudents)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_student_section_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.SectionStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_student_student_id_fkey");
        });

        modelBuilder.Entity<SectionTeacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("section_teacher_pkey");

            entity.ToTable("section_teacher", tb => tb.HasComment("Связка кружка и учителя, который его проводит"));

            entity.HasIndex(e => e.IsActual, "section_teacher_is_actual_idx")
                .IsUnique()
                .HasFilter("(is_actual = true)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActual)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasComment("Метка актуальности записи")
                .HasColumnName("is_actual");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Section).WithMany(p => p.SectionTeachers)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_teacher_section_id_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.SectionTeachers)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_teacher_teacher_id_fkey");
        });

        modelBuilder.Entity<SectionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("section_type_pkey");

            entity.ToTable("section_type", tb => tb.HasComment("Сущность Вид кружка"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDeleted).HasColumnName("date_deleted");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComment("Название вида кружка")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("student_pkey");

            entity.ToTable("student", tb => tb.HasComment("Сущность Ученик"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComment("ФИО")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teacher_pkey");

            entity.ToTable("teacher", tb => tb.HasComment("Сущность Учитель"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDeleted).HasColumnName("date_deleted");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComment("Дата увольнения")
                .HasColumnName("name");
        });

        modelBuilder.Entity<TeacherLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("teacher_link_pkey");

            entity.ToTable("teacher_link", "auth", tb => tb.HasComment("Связка между пользователем учителя и бизнес-сущностью"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherLinks)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_link_teacher_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.TeacherLinks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_link_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", "auth", tb => tb.HasComment("Пользователь системы"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_id_fkey");
        });
    }
}
