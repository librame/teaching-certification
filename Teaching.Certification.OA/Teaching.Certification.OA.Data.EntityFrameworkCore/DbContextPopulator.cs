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
        /// 用户管理员角色。
        /// </summary>
        public const string UserAdministrator = "管理用户";

        /// <summary>
        /// 用户注册员角色。
        /// </summary>
        public const string UserRegister = "注册用户";

        /// <summary>
        /// 管理员角色名称。
        /// </summary>
        public const string RoleNameAdministrator = "Administrator";

        /// <summary>
        /// 注册员角色名称。
        /// </summary>
        public const string RoleNameRegister = "Register";

        /// <summary>
        /// 默认密码。
        /// </summary>
        public const string DefaultPassword = "123456";

        private static Role? _roleAdministrator;
        private static Role? _roleRegister;


        /// <summary>
        /// 异步填充。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回 <see cref="Task"/>。</returns>
        public static async Task PopulateAsync(DbContextAccessor accessor, IServiceProvider services,
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

            // 填充日程
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Schedules.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
                PopulateSchedules(accessor);
            }

            // 同步数据库
            await accessor.SaveChangesAsync(cancellationToken);
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
                Url = "/Global/Logs", // go to Logs
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
                Url = "/Schedule/My", // go to My
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
                Id = "102002",
                ParentId = "102",
                Name = "部门日程",
                Url = "/Schedule/Departments",
                Rank = 2.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "102003",
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
                Url = "/Personnel/Users", // go to Users
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
                Id = "101002",
                ParentId = "101",
                Name = "部门管理",
                Url = "/Personnel/Departments",
                Rank = 1.2F
            });
            accessor.Menus.Add(new Menu
            {
                Id = "101003",
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
                Id = StoreHelper.GenerateRoleId(),
                Name = RoleNameAdministrator,
                Descr = "管理员"
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。

            accessor.Roles.Add(new Role
            {
                Id = StoreHelper.GenerateRoleId(),
                Name = RoleNameRegister,
                Descr = "注册员"
            });

            _roleAdministrator = accessor.Roles.ReadyQuery().First(p => p.Name == RoleNameAdministrator);
            _roleRegister = accessor.Roles.ReadyQuery().First(p => p.Name == RoleNameRegister);

            // RoleMenu
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
#pragma warning restore CS8602 // 解引用可能出现空引用。
                {
                    RoleId = _roleAdministrator?.Id,
                    MenuId = id
                });
            });

            registMenuIds.ForEach(id =>
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                accessor.RoleMenus.Add(new RoleMenu
#pragma warning restore CS8602 // 解引用可能出现空引用。
                {
                    RoleId = _roleRegister?.Id,
                    MenuId = id
                });
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
                    AbbrName = "机构简称"
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

            var defaultPasswordHash = accessor.GetService<IPasswordHashService>().ComputeHash(DefaultPassword);

#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Users.Add(new User
            {
                Id = StoreHelper.GenerateUserId(),
                RoleId = _roleAdministrator?.Id,
                Name = UserAdministrator,
                PasswordHash = defaultPasswordHash
            });
#pragma warning restore CS8602 // 解引用可能出现空引用。
            accessor.Users.Add(new User
            {
                Id = StoreHelper.GenerateUserId(),
                RoleId = _roleRegister?.Id,
                DepartmentId = 1,
                Name = UserRegister,
                PasswordHash = defaultPasswordHash
            });
        }

        private static void PopulateSchedules(DbContextAccessor accessor)
        {
            var now = DateTime.Now;

            // Note
#pragma warning disable CS8604 // 可能的 null 引用参数。
            if (!accessor.Notes.IsPopulated())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
#pragma warning disable CS8604 // 可能的 null 引用参数。
                foreach (var user in accessor.Users.ReadyForEach())
#pragma warning restore CS8604 // 可能的 null 引用参数。
                {
                    accessor.Notes.Add(new Note
                    {
                        CreatorId = user?.Id,
                        Title = $"测试{user?.Id}个人便笺",
                        Descr = "便笺描述",
                        CreatedTime = now
                    });
                }
            }

#pragma warning disable CS8604 // 可能的 null 引用参数。
            foreach (var user in accessor.Users.ReadyForEach())
#pragma warning restore CS8604 // 可能的 null 引用参数。
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                accessor.Schedules.Add(new Schedule
#pragma warning restore CS8602 // 解引用可能出现空引用。
                {
                    DepartmentId = 0,
                    CreatorId = user?.Id,
                    Title = $"测试{user?.Id}个人日程",
                    Address = "日程地址",
                    Descr = "日程描述",
                    BeginTime = now.AddDays(10),
                    EndTime = now.AddDays(30),
                    CreatedTime = now,
                    Scope = ScheduleScope.Public
                });
            }

#pragma warning disable CS8602 // 解引用可能出现空引用。
            accessor.Schedules.Add(new Schedule
#pragma warning restore CS8602 // 解引用可能出现空引用。
            {
                DepartmentId = 1,
                CreatorId = accessor.Users.ReadyQuery().First()?.Id,
                Title = $"测试部门日程",
                Address = "日程地址",
                Descr = "日程描述",
                BeginTime = now.AddDays(10),
                EndTime = now.AddDays(30),
                CreatedTime = now,
                Scope = ScheduleScope.Public
            });
        }

    }
}
