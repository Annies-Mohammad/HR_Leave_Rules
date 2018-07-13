using LeaveProcess.Models.DTO;
using System;
using System.Collections.Generic; 
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LeaveProcess.Entity_DAL
{
    public class LeaveCOntext : DbContext
    {
        #region Public properties
        public System.Data.Entity.DbSet<Leave> Leaves { get; set; }
        #endregion

        #region Construction
        public LeaveCOntext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region Protected methods
      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Leave>()
                .ToTable("Leaves");

            modelBuilder.Entity<Leave>()
                .Property(s => s.Id)
                .IsRequired();

            modelBuilder.Entity<Leave>()
                .Property(s => s.FirstName)
                .HasDefaultValue("test");

            modelBuilder.Entity<Leave>()
                .Property(s => s.LastName)
                .HasDefaultValue("testLast");

            modelBuilder.Entity<Leave>()
                .Property(s => s.LeaveDate)
                .HasDefaultValue(DateTime.Now);            
        }*/
        #endregion
    }
}
