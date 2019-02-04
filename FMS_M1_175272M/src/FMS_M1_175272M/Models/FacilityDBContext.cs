using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FMS_M1_175272M.Models
{
    public partial class FacilityDBContext : DbContext
    {
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<EndTime> EndTime { get; set; }
        public virtual DbSet<Facility> Facility { get; set; }
        public virtual DbSet<FacilityType> FacilityType { get; set; }
        public virtual DbSet<StartTime> StartTime { get; set; }

        public FacilityDBContext(DbContextOptions<FacilityDBContext> options) :
            base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FacilityDB;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.StaffName).IsRequired();

                entity.HasOne(d => d.EndTime)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.EndTimeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_EndTimeId_EndTime");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_FacilityId_Facility");

                entity.HasOne(d => d.StartTime)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.StartTimeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_StartTimeId_StartTime");
            });

            modelBuilder.Entity<EndTime>(entity =>
            {
                entity.Property(e => e.EndTime1)
                    .IsRequired()
                    .HasColumnName("EndTime")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.Abr)
                    .IsRequired()
                    .HasColumnType("nchar(5)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Details).IsRequired();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Facility)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Facility_ToFacilityType");
            });

            modelBuilder.Entity<FacilityType>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("nchar(2)");
            });

            modelBuilder.Entity<StartTime>(entity =>
            {
                entity.Property(e => e.StartTime1)
                    .IsRequired()
                    .HasColumnName("StartTime")
                    .HasMaxLength(5);
            });
        }
    }
}