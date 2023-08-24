using AppServer.Core.DTOs.Account;
using AppServer.Core.Features.AccountFeatures.Commands;
using AppServer.Core.Helpers.Constants;
using AppServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Core.Helpers.Extensions
{
    public static class AccountExtensions
    {
        public static AccountDTO ToModel(this Account model)
        {
            return new AccountDTO
            {
                Id = model.Id,
                Email = model.Email,
                Password = null,
                PhoneNumber = model.PhoneNumber,
                OTP = null,
                Token = model.Token,
                CreateDate = model.CreateDate,
                CreateUserId = model.CreateUserId,
                ModifyDate = model.ModifyDate,
                ModifyUserId = model.ModifyUserId,
                IsActive = model.IsActive,
                IsVerified = model.IsVerified,
                StatusId = model.StatusId
            };
        }

        public static Account ToEntity(this RegisterCommand model)
        {
            return new Account
            {
                Email = model.Email,
                Password = model.HashedPassword,
                PhoneNumber = model.PhoneNumber,
                OTP = model.Otp,
                CreateDate = DateTime.Now,
                CreateUserId = model.CreateUserId,
                ModifyDate = DateTime.Now,
                ModifyUserId = model.ModifyUserId,
                IsActive = model.IsActive,
                StatusId = model.StatusId
            };
        }
        public static AccountRole ToEntity(this Account model, Role role)
        {
            return new AccountRole
            {
                AccountId = model.Id,
                RoleId = role.Id,
                CreateDate = DateTime.Now,
                CreateUserId = model.CreateUserId,
                ModifyDate = DateTime.Now,
                ModifyUserId = model.ModifyUserId,
                IsActive = true,
                StatusId = (int)StatusEnum.ACTIVE // Active
            };
        }
        public static LoginCommand ToCommand(this LoginDTO model)
        {
            return new LoginCommand
            {
                Email = model.Email,
                Password = model.Password,
            };
        }

        public static RegisterCommand ToCommand(this RegisterDTO model)
        {
            return new RegisterCommand
            {
                Email = model.Email,
                Password = model.Password,
                RoleName = model.RoleName,
                PhoneNumber = model.PhoneNumber,
                CreateUserId = model.CreateUserId,                
                ModifyUserId = model.ModifyUserId,
                IsActive = model.IsActive,
                StatusId = model.StatusId
            };
        }
    }
}
