using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectFinance.Domain.Entities;

namespace ProjectFinance.Infrastructure.DBContext;

public partial class ProjectFinanceContext : DbContext
{
    public ProjectFinanceContext()
    {
    }

    public ProjectFinanceContext(DbContextOptions<ProjectFinanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contractor> Contractors { get; set; }

    public virtual DbSet<CostCategory> CostCategories { get; set; }

    public virtual DbSet<CostDetail> CostDetails { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<FinanceOption> FinanceOptions { get; set; }

    public virtual DbSet<FinanceOptionSchedule> FinanceOptionSchedules { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MonitoringEvaluation> MonitoringEvaluations { get; set; }

    public virtual DbSet<PODetail> PODetails { get; set; }

    public virtual DbSet<PODetailReceive> PODetailReceives { get; set; }

    public virtual DbSet<POPaySchedule> POPaySchedules { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectActivity> ProjectActivities { get; set; }

    public virtual DbSet<ProjectActivityCost> ProjectActivityCosts { get; set; }

    public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }

    public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }

    public virtual DbSet<ProjectSchedule> ProjectSchedules { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StakeHolder> StakeHolders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();
        });

        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.Property(e => e.code).IsFixedLength();
            entity.Property(e => e.phone).IsFixedLength();
        });

        modelBuilder.Entity<CostCategory>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<CostDetail>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();

            entity.HasOne(d => d.CostCategory).WithMany(p => p.CostDetails).HasConstraintName("FK_CostDetails_CostCategory");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<FinanceOption>(entity =>
        {
            entity.HasOne(d => d.Bank).WithMany(p => p.FinanceOptions).HasConstraintName("FK_FinanceOption_Bank");
        });

        modelBuilder.Entity<FinanceOptionSchedule>(entity =>
        {
            entity.HasOne(d => d.FinanceOption).WithMany(p => p.FinanceOptionSchedules).HasConstraintName("FK_FinanceOptionSchedule_FinanceOption");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.Invoices).HasConstraintName("FK_Invoice_Project");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.Invoices).HasConstraintName("FK_Invoice_PurchaseOrder");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Invoices).HasConstraintName("FK_Invoice_Supplier");
        });

        modelBuilder.Entity<MonitoringEvaluation>(entity =>
        {
            entity.Property(e => e.workDone).IsFixedLength();

            entity.HasOne(d => d.Activity).WithMany(p => p.MonitoringEvaluations).HasConstraintName("FK_MonitoringEvaluation_MonitoringEvaluation");
        });

        modelBuilder.Entity<POPaySchedule>(entity =>
        {
            entity.HasOne(d => d.PO).WithMany(p => p.POPaySchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_POPaySchedule_PurchaseOrder");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();

            entity.HasOne(d => d.Client).WithMany(p => p.Projects).HasConstraintName("FK_Project_Client");

            entity.HasOne(d => d.Contractor).WithMany(p => p.Projects).HasConstraintName("FK_Project_Contractor");

            entity.HasOne(d => d.Currency).WithMany(p => p.Projects).HasConstraintName("FK_Project_Currency");

            entity.HasOne(d => d.ProjectCategory).WithMany(p => p.Projects).HasConstraintName("FK_Project_ProjectCategory");

            entity.HasOne(d => d.ProjectType).WithMany(p => p.Projects).HasConstraintName("FK_Project_ProjectType");
        });

        modelBuilder.Entity<ProjectActivity>(entity =>
        {
            entity.HasOne(d => d.Activity).WithMany(p => p.ProjectActivities).HasConstraintName("FK_ProjectActivity_Activity");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectActivities).HasConstraintName("FK_ProjectActivity_Project");
        });

        modelBuilder.Entity<ProjectActivityCost>(entity =>
        {
            entity.HasOne(d => d.CostDetail).WithMany(p => p.ProjectActivityCosts).HasConstraintName("FK_ProjectActivityCost_CostDetails");

            entity.HasOne(d => d.ProjectActivity).WithMany(p => p.ProjectActivityCosts).HasConstraintName("FK_ProjectActivityCost_ProjectActivity");
        });

        modelBuilder.Entity<ProjectCategory>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<ProjectSchedule>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.ProjectSchedules).HasConstraintName("FK_ProjectSchedule_Project");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.Gender).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
