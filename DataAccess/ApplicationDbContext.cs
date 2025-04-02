using Microsoft.EntityFrameworkCore;
using SoilReportApp.Models;

namespace SoilReportApp.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } 
    public DbSet<Reading> Readings { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Crop> Crops { get; set; }
    public DbSet<CropStage> CropStages { get; set; }
    public DbSet<SoilType> SoilTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reading>()
            .HasOne(reading => reading.Request)
            .WithMany(request => request.Readings)
            .HasForeignKey(reading => reading.RequestId);
        
        modelBuilder.Entity<Request>()
            .HasOne(request => request.Crop)
            .WithMany(crop => crop.Requests)
            .HasForeignKey(request => request.CropId)
            .OnDelete(DeleteBehavior.SetNull); ;
        
        modelBuilder.Entity<Request>()
            .HasOne(request => request.CropStage)
            .WithMany(cropStage => cropStage.Requests)
            .HasForeignKey(request => request.CropStageId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Request>()
            .HasOne(request => request.SoilType)
            .WithMany(soilType => soilType.Requests)
            .HasForeignKey(request => request.SoilTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Request>()
            .HasOne(r => r.Farmer) // Request has one Farmer 
            .WithMany(u => u.RequestsAsFarmer) // Farmer can have multiple Requests
            .HasForeignKey(r => r.FarmerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Request>()
            .HasOne(r => r.Expert)  // Request has one Expert
            .WithMany(u => u.RequestsAsExpert) // Expert can have multiple Requests
            .HasForeignKey(r => r.ExpertId) // Foreign Key
            .OnDelete(DeleteBehavior.Restrict);
            
        base.OnModelCreating(modelBuilder);
    }
}