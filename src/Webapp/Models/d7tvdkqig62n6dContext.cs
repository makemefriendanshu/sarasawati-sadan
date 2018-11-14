using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webapp.Models
{
    public partial class d7tvdkqig62n6dContext : DbContext
    {
        public d7tvdkqig62n6dContext()
        {
        }

        public d7tvdkqig62n6dContext(DbContextOptions<d7tvdkqig62n6dContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EventInvocationLogs> EventInvocationLogs { get; set; }
        public virtual DbSet<EventLog> EventLog { get; set; }
        public virtual DbSet<EventTriggers> EventTriggers { get; set; }
        public virtual DbSet<HdbPermission> HdbPermission { get; set; }
        public virtual DbSet<HdbQueryTemplate> HdbQueryTemplate { get; set; }
        public virtual DbSet<HdbRelationship> HdbRelationship { get; set; }
        public virtual DbSet<HdbTable> HdbTable { get; set; }

        // Unable to generate entity type for table 'hdb_catalog.hdb_version'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=ec2-54-225-115-234.compute-1.amazonaws.com;Database=d7tvdkqig62n6d;Username=lxrljtzxdnogvg;Password=84e8873531e63a38804c9119ba3d97e89df3372e6dbbd4a0b0430e6a9369fdfd;sslmode=Require;Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Gid);

                entity.ToTable("employee");

                entity.Property(e => e.Gid)
                    .HasColumnName("gid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Costcentre)
                    .IsRequired()
                    .HasColumnName("costcentre");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasColumnName("employee_email");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name");

                entity.Property(e => e.EntityStatusId).HasColumnName("entity_status_id");

                entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasColumnName("grade");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.ManagerGid)
                    .IsRequired()
                    .HasColumnName("manager_gid");

                entity.Property(e => e.ManagerName)
                    .IsRequired()
                    .HasColumnName("manager_name");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");
            });

            modelBuilder.Entity<EventInvocationLogs>(entity =>
            {
                entity.ToTable("event_invocation_logs", "hdb_catalog");

                entity.HasIndex(e => e.EventId)
                    .HasName("event_invocation_logs_event_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Request)
                    .HasColumnName("request")
                    .HasColumnType("json");

                entity.Property(e => e.Response)
                    .HasColumnName("response")
                    .HasColumnType("json");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.EventInvocationLogs)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("event_invocation_logs_event_id_fkey");
            });

            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.ToTable("event_log", "hdb_catalog");

                entity.HasIndex(e => e.TriggerId)
                    .HasName("event_log_trigger_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Delivered).HasColumnName("delivered");

                entity.Property(e => e.Error).HasColumnName("error");

                entity.Property(e => e.Locked).HasColumnName("locked");

                entity.Property(e => e.NextRetryAt).HasColumnName("next_retry_at");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnName("payload")
                    .HasColumnType("jsonb");

                entity.Property(e => e.SchemaName)
                    .IsRequired()
                    .HasColumnName("schema_name");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnName("table_name");

                entity.Property(e => e.Tries).HasColumnName("tries");

                entity.Property(e => e.TriggerId)
                    .IsRequired()
                    .HasColumnName("trigger_id");

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasColumnName("trigger_name");
            });

            modelBuilder.Entity<EventTriggers>(entity =>
            {
                entity.ToTable("event_triggers", "hdb_catalog");

                entity.HasIndex(e => e.Name)
                    .HasName("event_triggers_name_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Definition)
                    .HasColumnName("definition")
                    .HasColumnType("json");

                entity.Property(e => e.Headers)
                    .HasColumnName("headers")
                    .HasColumnType("json");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NumRetries)
                    .HasColumnName("num_retries")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.RetryInterval)
                    .HasColumnName("retry_interval")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.SchemaName)
                    .IsRequired()
                    .HasColumnName("schema_name");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnName("table_name");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type");

                entity.Property(e => e.Webhook)
                    .IsRequired()
                    .HasColumnName("webhook");
            });

            modelBuilder.Entity<HdbPermission>(entity =>
            {
                entity.HasKey(e => new { e.TableSchema, e.TableName, e.RoleName, e.PermType });

                entity.ToTable("hdb_permission", "hdb_catalog");

                entity.Property(e => e.TableSchema).HasColumnName("table_schema");

                entity.Property(e => e.TableName).HasColumnName("table_name");

                entity.Property(e => e.RoleName).HasColumnName("role_name");

                entity.Property(e => e.PermType).HasColumnName("perm_type");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("is_system_defined")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.PermDef)
                    .IsRequired()
                    .HasColumnName("perm_def")
                    .HasColumnType("jsonb");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.HdbPermission)
                    .HasForeignKey(d => new { d.TableSchema, d.TableName })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hdb_permission_table_schema_fkey");
            });

            modelBuilder.Entity<HdbQueryTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateName);

                entity.ToTable("hdb_query_template", "hdb_catalog");

                entity.Property(e => e.TemplateName)
                    .HasColumnName("template_name")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("is_system_defined")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.TemplateDefn)
                    .IsRequired()
                    .HasColumnName("template_defn")
                    .HasColumnType("jsonb");
            });

            modelBuilder.Entity<HdbRelationship>(entity =>
            {
                entity.HasKey(e => new { e.TableSchema, e.TableName, e.RelName });

                entity.ToTable("hdb_relationship", "hdb_catalog");

                entity.Property(e => e.TableSchema).HasColumnName("table_schema");

                entity.Property(e => e.TableName).HasColumnName("table_name");

                entity.Property(e => e.RelName).HasColumnName("rel_name");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("is_system_defined")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.RelDef)
                    .IsRequired()
                    .HasColumnName("rel_def")
                    .HasColumnType("jsonb");

                entity.Property(e => e.RelType).HasColumnName("rel_type");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.HdbRelationship)
                    .HasForeignKey(d => new { d.TableSchema, d.TableName })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hdb_relationship_table_schema_fkey");
            });

            modelBuilder.Entity<HdbTable>(entity =>
            {
                entity.HasKey(e => new { e.TableSchema, e.TableName });

                entity.ToTable("hdb_table", "hdb_catalog");

                entity.Property(e => e.TableSchema).HasColumnName("table_schema");

                entity.Property(e => e.TableName).HasColumnName("table_name");

                entity.Property(e => e.IsSystemDefined)
                    .HasColumnName("is_system_defined")
                    .HasDefaultValueSql("false");
            });
        }
    }
}
