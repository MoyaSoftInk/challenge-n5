namespace ChallengeN5.Command.Persistance.Application.Data;

using ChallengeN5.Command.Domain.Application.Model;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// N5 db context
/// </summary>
public class N5Context : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    public N5Context(DbContextOptions<N5Context> options) : base(options)
    {
    }

    /// <summary>
    /// On model creating
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Position).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.EmployeeId).IsRequired();
            entity.Property(x => x.PermissionTypeId).IsRequired();
            entity.Property(x => x.StartDate).IsRequired();
            entity.Property(x => x.EndDate).IsRequired();

            entity.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.PermissionType)
                .WithMany()
                .HasForeignKey(x => x.PermissionTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(N5Context).Assembly);
    }
}
