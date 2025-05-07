using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class JobiverseContext : DbContext
{
    public JobiverseContext()
    {
    }

    public JobiverseContext(DbContextOptions<JobiverseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<CvFile> CvFiles { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentApply> StudentApplies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=mysql-ed145c2-hochitrung08012004-6685.j.aivencloud.com;port=22012;database=Jobiverse;user=avnadmin;password=AVNS_23e2BqV9YnTCreBLUIT;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "PhoneNumber").IsUnique();

            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountType).HasColumnType("enum('admin','employer','student')");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.Cvid).HasName("PRIMARY");

            entity.ToTable("CVs");

            entity.HasIndex(e => e.StudentId, "StudentID");

            entity.Property(e => e.Cvid)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("CVID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Student).WithMany(p => p.Cvs)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("CVs_ibfk_1");
        });

        modelBuilder.Entity<CvFile>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PRIMARY");

            entity.ToTable("CV_Files");

            entity.HasIndex(e => e.Cvid, "CVID");

            entity.Property(e => e.FileId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("FileID");
            entity.Property(e => e.Cvid).HasColumnName("CVID");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .HasColumnName("FileURL");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'")
                .HasColumnType("datetime(3)");

            entity.HasOne(d => d.Cv).WithMany(p => p.CvFiles)
                .HasForeignKey(d => d.Cvid)
                .HasConstraintName("CV_Files_ibfk_1");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.DisciplineId).HasName("PRIMARY");

            entity.HasIndex(e => e.MajorId, "MajorID");

            entity.Property(e => e.DisciplineId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("DisciplineID");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.DisciplineName).HasMaxLength(255);
            entity.Property(e => e.MajorId).HasColumnName("MajorID");

            entity.HasOne(d => d.Major).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Disciplines_ibfk_1");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployerId).HasName("PRIMARY");

            entity.HasIndex(e => e.AccountId, "AccountID").IsUnique();

            entity.Property(e => e.EmployerId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("EmployerID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AddressEmployers).HasMaxLength(255);
            entity.Property(e => e.BusinessScale).HasColumnType("enum('Private individuals','companies')");
            entity.Property(e => e.CompanyInfo).HasMaxLength(100);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.Job).HasMaxLength(100);
            entity.Property(e => e.Prove).HasMaxLength(100);
            entity.Property(e => e.RepresentativeName).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Employer)
                .HasForeignKey<Employer>(d => d.AccountId)
                .HasConstraintName("Employers_ibfk_1");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.MajorId).HasName("PRIMARY");

            entity.Property(e => e.MajorId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("MajorID");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.MajorName).HasMaxLength(255);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

            entity.HasIndex(e => e.AccountId, "AccountID");

            entity.Property(e => e.NotificationId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("NotificationID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.IsRead).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Notifications_ibfk_1");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PRIMARY");

            entity.HasIndex(e => e.AccountId, "AccountID").IsUnique();

            entity.HasIndex(e => e.MajorId, "MajorID");

            entity.Property(e => e.ProjectId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("ProjectID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'open'")
                .HasColumnType("enum('open','closed','archived')");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.WorkingTime).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Project)
                .HasForeignKey<Project>(d => d.AccountId)
                .HasConstraintName("Projects_ibfk_1");

            entity.HasOne(d => d.Major).WithMany(p => p.Projects)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Projects_ibfk_2");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PRIMARY");

            entity.HasIndex(e => e.AccountId, "AccountID").IsUnique();

            entity.HasIndex(e => e.MajorId, "MajorID");

            entity.Property(e => e.StudentId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("StudentID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.MajorId).HasColumnName("MajorID");
            entity.Property(e => e.Mssv)
                .HasMaxLength(20)
                .HasColumnName("MSSV");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.University).HasMaxLength(100);

            entity.HasOne(d => d.Account).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.AccountId)
                .HasConstraintName("Students_ibfk_1");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Students_ibfk_2");
        });

        modelBuilder.Entity<StudentApply>(entity =>
        {
            entity.HasKey(e => e.StudentApplyId).HasName("PRIMARY");

            entity.ToTable("StudentApply");

            entity.HasIndex(e => e.ProjectId, "ProjectID");

            entity.HasIndex(e => e.StudentId, "StudentID");

            entity.Property(e => e.StudentApplyId)
                .HasDefaultValueSql("'uuid()'")
                .HasColumnName("StudentApplyID");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'")
                .HasColumnType("datetime(3)");
            entity.Property(e => e.Deleted).HasDefaultValueSql("'0'");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','accepted','rejected')");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Project).WithMany(p => p.StudentApplies)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("StudentApply_ibfk_2");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentApplies)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("StudentApply_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
