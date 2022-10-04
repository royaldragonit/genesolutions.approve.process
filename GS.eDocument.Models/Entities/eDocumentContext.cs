using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace GS.eDocument.Models.Entities
{
    public class eDocumentContext : DbContext
    {
        public eDocumentContext()
            : base("name=eDocumentContext")
        {
        }

        public virtual DbSet<ApproveProcess> ApproveProcesss { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Approve> Approves { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<ApproveProcess>();
            modelBuilder.Entity<Step>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Approve>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}