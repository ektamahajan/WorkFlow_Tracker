﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkflowTracker.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class emahajanDataBaseEntities5 : DbContext
    {
        public emahajanDataBaseEntities5()
            : base("name=emahajanDataBaseEntities5")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Justification> Justifications { get; set; }
        public virtual DbSet<MasterStep> MasterSteps { get; set; }
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }
        public virtual DbSet<StepDetail> StepDetails { get; set; }
        public virtual DbSet<UserProject> UserProjects { get; set; }
        public virtual DbSet<HISTORY_REPORT> HISTORY_REPORT { get; set; }
        public virtual DbSet<HISTORY_REPORT1> HISTORY_REPORT1 { get; set; }
    }
}