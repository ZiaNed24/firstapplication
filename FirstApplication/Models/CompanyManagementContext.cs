using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

public partial class CompanyManagementContext : DbContext
{
    public CompanyManagementContext()
    {
    }

    public CompanyManagementContext(DbContextOptions<CompanyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=company_management;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__addresse__3213E83FBB30F773");

            entity.Property(e => e.IsPrimary).HasDefaultValue(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Addresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__addresses__emplo__4CA06362");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__countrie__7E8CD055FBF75234");

            entity.Property(e => e.CountryId).IsFixedLength();

            entity.HasOne(d => d.Region).WithMany(p => p.Countries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__countries__regio__38996AB5");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__C22324222F904449");

            entity.HasOne(d => d.Location).WithMany(p => p.Departments).HasConstraintName("FK__departmen__locat__3E52440B");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => e.DependentId).HasName("PK__dependen__F25E28CE04DC30EB");

            entity.HasOne(d => d.Employee).WithMany(p => p.Dependents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__dependent__emplo__48CFD27E");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA8830A5CF1");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasConstraintName("FK__employees__depar__45F365D3");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees).HasConstraintName("FK__employees__job_i__440B1D61");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager).HasConstraintName("FK__employees__manag__44FF419A");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__jobs__6E32B6A5CBB0391E");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__location__771831EAC3792594");

            entity.Property(e => e.CountryId).IsFixedLength();

            entity.HasOne(d => d.Country).WithMany(p => p.Locations).HasConstraintName("FK__locations__count__3B75D760");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__regions__01146BAE8FF9ABE5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
