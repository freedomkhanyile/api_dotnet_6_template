using AppServer.Core.Helpers.Constants;
using AppServer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Helpers.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedRoles();
            builder.SeedStatuses();
        }

        public static void SeedStatuses(this ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(
                new Status
                {
                    Id = (int)StatusEnum.ACTIVE,
                    Name = StatusConstants.ACTIVE,
                    Description = StatusConstants.ACTIVE,
                    IsActive = true,
                    StatusClass = "",
                },
                new Status
                {
                    Id = (int)StatusEnum.DISABLED,
                    Name = StatusConstants.DISABLED,
                    Description = StatusConstants.DISABLED,
                    IsActive = true,
                    StatusClass = "",
                },
                new Status
                {
                    Id = (int)StatusEnum.ARCHIVED,
                    Name = StatusConstants.ARCHIVED,
                    Description = StatusConstants.ARCHIVED,
                    IsActive = true,
                    StatusClass = "",
                },
                new Status
                {
                    Id = (int)StatusEnum.PENDING_ACTIVATION,
                    Name = StatusConstants.PENDING_ACTIVATION,
                    Description = StatusConstants.PENDING_ACTIVATION,
                    IsActive = true,
                    StatusClass = "",
                });
        }
        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("3f907975d4e24944b05bd54afc7b1539"),
                Name = RoleConstants.Admin,
                Description = "An administrator role normaly does everything.",
                CreateDate = new DateTime(2022, 6, 29, 21, 5, 58, 504, DateTimeKind.Local).AddTicks(7050),
                CreateUserId = "builder.seed",
                ModifyDate = new DateTime(2022, 6, 29, 21, 5, 58, 504, DateTimeKind.Local).AddTicks(7050),
                ModifyUserId = "builder.seed",
                StatusId = 1,
            },
            new Role
            {
                Id = new Guid("6d3caab6b94749ce8cd90479d1e8048d"),
                Name = RoleConstants.Member,
                Description = "Basic member of the system with limited scope.",
                CreateDate = new DateTime(2022, 6, 29, 21, 5, 58, 504, DateTimeKind.Local).AddTicks(7050),
                CreateUserId = "builder.seed",
                ModifyDate = new DateTime(2022, 6, 29, 21, 5, 58, 504, DateTimeKind.Local).AddTicks(7050),
                ModifyUserId = "builder.seed",
                StatusId = 1
            });
        }
    }
}
