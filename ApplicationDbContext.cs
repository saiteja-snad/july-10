using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SMS;Username=postgres;Password=Saiteja@1919;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.AttendanceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("attendance_id");
            entity.Property(e => e.AttendanceDate).HasColumnName("attendance_date");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.MarkedBy).HasColumnName("marked_by");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PRESENT'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_attendance_courses");

            entity.HasOne(d => d.MarkedByNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.MarkedBy)
                .HasConstraintName("fk_attendance_users");

            entity.HasOne(d => d.Student).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_attendance_students");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("classes_pkey");

            entity.ToTable("classes");

            entity.Property(e => e.ClassId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("class_id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(100)
                .HasColumnName("class_name");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.SectionName)
                .HasMaxLength(20)
                .HasColumnName("section_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Classes)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_classes_departments");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.HasIndex(e => e.CourseCode, "courses_course_code_key").IsUnique();

            entity.Property(e => e.CourseId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("course_id");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("course_code");
            entity.Property(e => e.CourseName)
                .HasMaxLength(150)
                .HasColumnName("course_name");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("fk_courses_departments");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.HasIndex(e => e.DepartmentName, "departments_department_name_key").IsUnique();

            entity.Property(e => e.DepartmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("department_name");
            entity.Property(e => e.HodName)
                .HasMaxLength(100)
                .HasColumnName("hod_name");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("enrollments_pkey");

            entity.ToTable("enrollments");

            entity.Property(e => e.EnrollmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("enrollment_id");
            entity.Property(e => e.AcademicYear)
                .HasMaxLength(20)
                .HasColumnName("academic_year");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("enrollment_date");
            entity.Property(e => e.Semester)
                .HasMaxLength(20)
                .HasColumnName("semester");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ENROLLED'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_enrollments_courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_enrollments_students");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("exams_pkey");

            entity.ToTable("exams");

            entity.Property(e => e.ExamId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("exam_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.ExamDate).HasColumnName("exam_date");
            entity.Property(e => e.ExamName)
                .HasMaxLength(100)
                .HasColumnName("exam_name");
            entity.Property(e => e.TotalMarks).HasColumnName("total_marks");

            entity.HasOne(d => d.Course).WithMany(p => p.Exams)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_exams_courses");
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.FeeId).HasName("fees_pkey");

            entity.ToTable("fees");

            entity.Property(e => e.FeeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("fee_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.FeeType)
                .HasMaxLength(100)
                .HasColumnName("fee_type");
            entity.Property(e => e.PaidAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("paid_amount");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDING'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Fees)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fees_students");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("results_pkey");

            entity.ToTable("results");

            entity.Property(e => e.ResultId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("result_id");
            entity.Property(e => e.ExamId).HasColumnName("exam_id");
            entity.Property(e => e.Grade)
                .HasMaxLength(20)
                .HasColumnName("grade");
            entity.Property(e => e.MarksObtained)
                .HasPrecision(5, 2)
                .HasColumnName("marks_obtained");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .HasColumnName("remarks");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Exam).WithMany(p => p.Results)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_results_exams");

            entity.HasOne(d => d.Student).WithMany(p => p.Results)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_results_students");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("students_pkey");

            entity.ToTable("students");

            entity.HasIndex(e => e.Email, "students_email_key").IsUnique();

            entity.HasIndex(e => e.StudentCode, "students_student_code_key").IsUnique();

            entity.HasIndex(e => e.UserId, "students_user_id_key").IsUnique();

            entity.Property(e => e.StudentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("student_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.AdmissionDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("admission_date");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVE'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(50)
                .HasColumnName("student_code");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("fk_students_classes");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.UserId)
                .HasConstraintName("fk_students_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.UserId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVE'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
