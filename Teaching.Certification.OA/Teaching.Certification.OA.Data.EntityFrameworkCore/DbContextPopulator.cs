#region License

/* **************************************************************************************
 * Librame Pong for Teaching Certification.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Teaching.Certification.OA.Data
{
    /// <summary>
    /// <see cref="DbContext"/> 填充器。
    /// </summary>
    public static class DbContextPopulator
    {
        /// <summary>
        /// 开始填充。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public static async Task OnPopulateAsync(DbContextAccessor accessor, IServiceProvider services,
            CancellationToken cancellationToken = default)
        {
            accessor.NotNull(nameof(accessor));
            services.NotNull(nameof(services));

            // 尝试创建数据库
            await accessor.Database.EnsureCreatedAsync(cancellationToken);

            // 填充菜单
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Menus.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                PopulateMenus(accessor);
            }

            // 填充角色
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Roles.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                PopulateRoles(accessor);
            }

            // 填充部门
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Departments.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                PopulateDepartments(accessor);
            }

            // 填充用户
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Users.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                PopulateUsers(accessor);
            }
        }


        private static void PopulateMenus(DbContextAccessor accessor)
        {
            // Global
#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Menus.Add(new Menu
            {
                Id = "105",
                ParentId = "1",
                Name = "系统管理",
                Url = "/Global",
                Rank = 1.0F
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。
            accessor.Menus.Add(new Menu
            {
                Id = "105001",
                ParentId = "105",
                Name = "角色管理",
                Url = "/Global/Roles",
                Rank = 1.1F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "105002",
                ParentId = "105",
                Name = "登录日志",
                Url = "/Global/UserLogins",
                Rank = 1.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "105003",
                ParentId = "105",
                Name = "操作日志",
                Url = "/Global/Logs",
                Rank = 1.3F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "105004",
                ParentId = "105",
                Name = "菜单管理",
                Url = "/Global/Menus",
                Rank = 1.4F
            });

            // Document
            accessor.Menus.Add(new Menu
            {
                Id = "103",
                ParentId = "1",
                Name = "文档管理",
                Url = "/Document",
                Rank = 2.0F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "103001",
                ParentId = "103",
                Name = "文档管理",
                Url = "/Document/Index",
                Rank = 2.1F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "103002",
                ParentId = "103",
                Name = "回收站",
                Url = "/Document/Recycle",
                Rank = 2.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "103003",
                ParentId = "103",
                Name = "文件搜索",
                Url = "/Document/Search",
                Rank = 2.3F
            });

            // Schedule
            accessor.Menus.Add(new Menu
            {
                Id = "102",
                ParentId = "1",
                Name = "日程管理",
                Url = "/Schedule",
                Rank = 2.0F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "102001",
                ParentId = "102",
                Name = "我的日程",
                Url = "/Schedule/My",
                Rank = 2.1F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "102001",
                ParentId = "102",
                Name = "部门日程",
                Url = "/Schedule/Departments",
                Rank = 2.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "102001",
                ParentId = "102",
                Name = "我的便签",
                Url = "/Schedule/Notes",
                Rank = 2.3F
            });

            // Personnel
            accessor.Menus.Add(new Menu
            {
                Id = "101",
                ParentId = "1",
                Name = "人事管理",
                Url = "/Personnel",
                Rank = 1.0F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "101001",
                ParentId = "101",
                Name = "机构管理",
                Url = "/Personnel/Branches",
                Rank = 1.1F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "101001",
                ParentId = "101",
                Name = "部门管理",
                Url = "/Personnel/Departments",
                Rank = 1.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "101001",
                ParentId = "101",
                Name = "员工管理",
                Url = "/Personnel/Users",
                Rank = 1.3F
            });
        }

        private static void PopulateRoles(DbContextAccessor accessor)
        {
            // Role
#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Roles.Add(new Role
            {
                Name = "Administrator",
                Descr = "管理员"
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。

            accessor.Roles.Add(new Role
            {
                Name = "Register",
                Descr = "注册人员"
            });

            // Right
#pragma warning disable CS8604 // 可能的 null 引用参数。
            var adminMenuIds = accessor.Menus.ReadyQuery().Select(s => s.Id).ToList();
#pragma warning restore CS8604 // 可能的 null 引用参数。
#pragma warning disable CS8602 // 解引用可能出现空引用。
            var registMenuIds = adminMenuIds.Where(p => p.StartsWith("102") || p.StartsWith("103"));
#pragma warning restore CS8602 // 解引用可能出现空引用。

            adminMenuIds.ForEach(id =>
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                accessor.RoleMenus.Add(new RoleMenu
                {
                    RoleId = 1, // Administrator
                    MenuId = id
                });
#pragma warning restore CS8602 // 解引用可能出现空引用。
            });

            registMenuIds.ForEach(id =>
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                accessor.RoleMenus.Add(new RoleMenu
                {
                    RoleId = 2, // Register
                    MenuId = id
                });
#pragma warning restore CS8602 // 解引用可能出现空引用。
            });
        }

        private static void PopulateDepartments(DbContextAccessor accessor)
        {
            // Branch
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Branches.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                accessor.Branches.Add(new Branch
                {
                    Name = "测试机构",
                    ShortName = "机构简称"
                });
            }

#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Departments.Add(new Department
            {
                Name = "测试部门",
                BranchId = 1,
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。
        }

        private static void PopulateUsers(DbContextAccessor accessor)
        {
            // State
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.UserStates.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                accessor.UserStates.Add(new UserState
                {
                    Name = "正常"
                });
                accessor.UserStates.Add(new UserState
                {
                    Name = "已屏蔽"
                });
            }

            var defaultPasswordHash = accessor.GetService<IPasswordHashService>().ComputeHash("123456");

#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Users.Add(new User
            {
                RoleId = 1,
                UserName = "默认管理用户",
                PasswordHash = defaultPasswordHash
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。
            accessor.Users.Add(new User
            {
                RoleId = 2,
                DepartmentId = 1,
                UserName = "默认注册用户",
                PasswordHash = defaultPasswordHash
            });
        }

    }
}
