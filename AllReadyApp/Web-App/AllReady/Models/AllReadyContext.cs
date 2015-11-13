﻿using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity.Metadata.Builders;

namespace AllReady.Models
{
    public class AllReadyContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Tenant> Tenants { get; set; }
        public DbSet<ActivitySignup> ActivitySignup { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignImpact> CampaignImpacts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivitySkill> ActivitySkills { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PostalCodeGeo> PostalCodes { get; set; }
        public DbSet<AllReadyTask> Tasks { get; set; }
        public DbSet<TaskSkill> TaskSkills { get; set; }
        public DbSet<TaskSignup> TaskSignups { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TenantContact> TenantContacts { get; set; }
        public DbSet<CampaignContact> CampaignContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseSqlServerIdentityColumns();

            Map(modelBuilder.Entity<Campaign>());
            Map(modelBuilder.Entity<CampaignSponsors>());
            Map(modelBuilder.Entity<Activity>());
            Map(modelBuilder.Entity<ActivitySkill>());
            Map(modelBuilder.Entity<ActivitySignup>());
            Map(modelBuilder.Entity<AllReadyTask>());
            Map(modelBuilder.Entity<TaskSkill>());
            Map(modelBuilder.Entity<TaskSignup>());
            Map(modelBuilder.Entity<Location>());
            Map(modelBuilder.Entity<PostalCodeGeo>());
            Map(modelBuilder.Entity<Skill>());
            Map(modelBuilder.Entity<UserSkill>());
            Map(modelBuilder.Entity<ApplicationUser>());
            Map(modelBuilder.Entity<Tenant>());
            Map(modelBuilder.Entity<TenantContact>());
            Map(modelBuilder.Entity<CampaignContact>());
            Map(modelBuilder.Entity<Contact>());

        }

        private void Map(EntityTypeBuilder<CampaignContact> builder)
        {
            builder.HasKey(tc => new { tc.CampaignId, tc.ContactId, tc.ContactType });
            builder.HasOne(tc => tc.Contact);
            builder.HasOne(tc => tc.Campaign);
        }

        private void Map(EntityTypeBuilder<Contact> builder)
        {
            builder.HasMany(c => c.TenantContacts);
            builder.HasMany(c => c.CampaignContacts);
        }

        private void Map(EntityTypeBuilder<TenantContact> builder)
        {
            builder.HasKey(tc => new { tc.TenantId, tc.ContactId, tc.ContactType});
            builder.HasOne(tc => tc.Contact);
            builder.HasOne(tc => tc.Tenant);
        }

        private void Map(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasOne(t => t.Location);
            builder.HasMany(t => t.TenantContacts);
            builder.Property(t => t.Name).IsRequired();
            


        }

        private void Map(EntityTypeBuilder<PostalCodeGeo> builder)
        {
            builder.HasKey(k => k.PostalCode);
        }

        private void Map(EntityTypeBuilder<Location> builder)
        {
            builder.HasOne(l => l.PostalCode);
        }

        private void Map(EntityTypeBuilder<TaskSignup> builder)
        {
            builder.HasOne(u => u.Task);
        }

        private void Map(EntityTypeBuilder<AllReadyTask> builder)
        {
            builder.HasOne(t => t.Activity);
            builder.HasOne(t => t.Tenant);
            builder.HasMany(t => t.AssignedVolunteers);
            builder.HasMany(t => t.RequiredSkills).WithOne(ts => ts.Task);
            builder.Property(p => p.Name).IsRequired();
            
        }

        private void Map(EntityTypeBuilder<TaskSkill> builder)
        {
            builder.HasKey(acsk => new { acsk.TaskId, acsk.SkillId });
        }

        private void Map(EntityTypeBuilder<ActivitySignup> builder)
        {
            builder.HasOne(t => t.User);
            builder.HasOne(t => t.Activity);
            builder.Property(t => t.SignupDateTime)
                   .IsRequired();
        }

        private void Map(EntityTypeBuilder<Activity> builder)
        {
            builder.HasOne(a => a.Tenant);
            builder.HasOne(a => a.Campaign);
            builder.HasOne(a => a.Location);
            builder.HasMany(a => a.Tasks);
            builder.HasMany(a => a.UsersSignedUp);
            builder.HasMany(a => a.RequiredSkills).WithOne(acsk => acsk.Activity);
            builder.Property(p => p.Name).IsRequired();
            
        }

        private void Map(EntityTypeBuilder<ActivitySkill> builder)
        {
            builder.HasKey(acsk => new { acsk.ActivityId, acsk.SkillId });
        }

        private void Map(EntityTypeBuilder<Skill> builder)
        {
            builder.HasOne(s => s.ParentSkill);             
        }

        private void Map(EntityTypeBuilder<CampaignSponsors> builder)
        {
            builder.HasOne(s => s.Campaign)
                   .WithMany(c => c.ParticipatingTenants);
            builder.HasOne(s => s.Tenant);
        }

        private void Map(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasOne(c => c.ManagingTenant);
            builder.HasOne(c => c.CampaignImpact);
            builder.HasMany(c => c.Activities);
            builder.HasOne(t => t.Location);
            builder.HasMany(t => t.CampaignContacts);
            builder.Property(a => a.Name).IsRequired();
            
        }

        private void Map(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.AssociatedSkills).WithOne(us => us.User);
        }

        private void Map(EntityTypeBuilder<UserSkill> builder)
        {
            builder.HasKey(us => new { us.UserId, us.SkillId });
        }
    }
}
