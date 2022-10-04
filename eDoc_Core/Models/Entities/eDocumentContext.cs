using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Xml.Linq;

namespace eDoc_Core.Models.Entities
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
        public virtual DbSet<TypeDocument> TypeDocuments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentFile> DocumentFiles { get; set; }
        public virtual DbSet<ApproveDocument> ApproveDocuments { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<ParamEmailTemplate> ParamEmailTemplates { get; set; }
        public virtual DbSet<LogException> LogExceptions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<ApproveProcess>();
            modelBuilder.Entity<Step>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Approve>();
            modelBuilder.Entity<TypeDocument>();
            modelBuilder.Entity<Document>();
            modelBuilder.Entity<DocumentFile>();
            modelBuilder.Entity<ApproveDocument>();
            modelBuilder.Entity<EmailTemplate>();
            modelBuilder.Entity<ParamEmailTemplate>();
            modelBuilder.Entity<LogException>();
        }
    }

    //public static class StoredProcedure
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}