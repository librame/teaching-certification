﻿#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="DbContext"/> 访问器。
    /// </summary>
    public class DbContextAccessor : DbContext, IAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="DbContextAccessor"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public DbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Attachment>? Attachments { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Branch>? Branches { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Department>? Departments { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Document>? Documents { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<DocumentCategory>? DocumentCategories { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Log>? Logs { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Menu>? Menus { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Note>? Notes { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Role>? Roles { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<RoleMenu>? RoleMenus { get; set; }

        /// <summary>
        /// 附件数据集。
        /// </summary>
        public DbSet<Schedule>? Schedules { get; set; }

        /// <summary>
        /// 用户数据集。
        /// </summary>
        public DbSet<User>? Users { get; set; }

        /// <summary>
        /// 用户登录数据集。
        /// </summary>
        public DbSet<UserLogin>? UserLogins { get; set; }

        /// <summary>
        /// 用户状态数据集。
        /// </summary>
        public DbSet<UserState>? UserStates { get; set; }


        /// <summary>
        /// 重载保存更改。
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">指示是否在更改已成功发送到数据库之后调用。</param>
        /// <returns>返回受影响的行数。</returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var aspects = this.GetService<IEnumerable<ISaveChangesAspect>>();
            var hasAspects = aspects.IsNotEmpty();

            if (hasAspects)
                aspects.ForEach(a => a?.PreInject(this));

            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            if (hasAspects)
                aspects.ForEach(a => a?.PostInject(this));

            return result;
        }

        /// <summary>
        /// 重载保存更改。
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">指示是否在更改已成功发送到数据库之后调用。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含受影响行数的异步操作。</returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var aspects = this.GetService<IEnumerable<ISaveChangesAspect>>();
            var hasAspects = aspects.IsNotEmpty();

            if (hasAspects)
                aspects.ForEach(a => a?.PreInject(this));

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            if (hasAspects)
                aspects.ForEach(a => a?.PostInject(this));

            return result;
        }


        /// <summary>
        /// 配置模型构建器核心。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(i => new { i.Name, i.DepartmentId })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                b.Property(p => p.RoleId)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.StateId)
                    .IsRequired();

                b.Property(p => p.DepartmentId)
                    .IsRequired();

                b.Property(p => p.Gender)
                    .IsRequired();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.PasswordHash)
                    .HasMaxLength(200)
                    .IsRequired();

                b.Property(p => p.Descr)
                    .HasMaxLength(200);

                // Relationship
                //b.HasMany<UserLogin>()
                //    .WithOne()
                //    .HasForeignKey(fk => fk.UserId)
                //    .IsRequired();
            });

            modelBuilder.Entity<UserState>(b =>
            {
                b.HasIndex(i => i.Name)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.HasIndex(i => i.UserId)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.Status)
                    .IsRequired();

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.UserId)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.UserIp)
                    .HasMaxLength(100)
                    .IsRequired();

                b.Property(p => p.Descr)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasIndex(i => i.Name)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedNever()
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Descr)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoleMenu>(b =>
            {
                b.HasIndex(i => new { i.RoleId, i.MenuId })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.RoleId)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.MenuId)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            modelBuilder.Entity<Menu>(b =>
            {
                b.HasIndex(i => new { i.ParentId, i.Name })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                b.Property(p => p.ParentId)
                    .HasMaxLength(50);

                b.Property(p => p.Rank)
                    .IsRequired();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Url)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Branch>(b =>
            {
                b.HasIndex(i => i.Name)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.AbbrName)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Department>(b =>
            {
                b.HasIndex(i => new { i.BranchId, i.Name })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.BranchId);

                b.Property(p => p.PrincipalId)
                    .HasMaxLength(50);

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Phone)
                    .HasMaxLength(100);

                b.Property(p => p.Mobile)
                    .HasMaxLength(100);

                b.Property(p => p.Fax)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Document>(b =>
            {
                b.HasIndex(i => new { i.CategoryId, i.Name })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.CategoryId)
                    .IsRequired();

                b.Property(p => p.ParentId)
                    .IsRequired();

                b.Property(p => p.Status)
                    .IsRequired();

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.OwnerId)
                    .HasMaxLength(50);

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Descr)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DocumentCategory>(b =>
            {
                b.HasIndex(i => i.Name)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Icon)
                    .HasMaxLength(50);

                b.Property(p => p.Extension)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Attachment>(b =>
            {
                b.HasIndex(i => i.Name)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.DocumentId)
                    .IsRequired();

                b.Property(p => p.DocumentCategoryId)
                    .IsRequired();

                b.Property(p => p.Length)
                    .IsRequired();

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Path)
                    .HasMaxLength(200)
                    .IsRequired();
            });

            modelBuilder.Entity<Schedule>(b =>
            {
                b.HasIndex(i => new { i.BeginTime, i.EndTime, i.Title })
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.DepartmentId)
                    .IsRequired();

                b.Property(p => p.CreatorId)
                    .HasMaxLength(50);
                    //.IsRequired();

                b.Property(p => p.Scope)
                    .IsRequired();

                b.Property(p => p.BeginTime)
                    .IsRequired();

                b.Property(p => p.EndTime)
                    .IsRequired();

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.Title)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Address)
                    .HasMaxLength(500);

                b.Property(p => p.Descr)
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Note>(b =>
            {
                b.HasIndex(i => i.Title)
                    .IsUnique();

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.CreatorId)
                    .HasMaxLength(50);
                    //.IsRequired();

                b.Property(p => p.Title)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Descr)
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<Log>(b =>
            {
                b.HasIndex(i => i.UserId);

                b.HasKey(k => k.Id);

                b.Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                b.Property(p => p.UserId)
                    .HasMaxLength(50);

                b.Property(p => p.AssocId)
                    .HasMaxLength(50);

                b.Property(p => p.CreatedTime)
                    .IsRequired();

                b.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(p => p.Descr);
            });
        }

    }
}
