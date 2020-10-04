using AutoMapper;
using BillManager.Data;
using BillManager.Models;
using BillManager.ModelsDTO;
using BillManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        public UsersService(ApplicationDbContext context, ILogger<UsersService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        public ResponseDTO EditUser(UserDTO userDTO)
        {
            logger.LogInformation("EditUser");
            if (context.Users.Where(b => b.Id == userDTO.Id).Count() == 0)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = "User not exist",
                    Status = "Error"
                };
            }

            try
            {
                context.Users.Update(mapper.Map<ApplicationUser>(userDTO));
                context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Code = 400,
                    Message = e.Message,
                    Status = "Error"
                };
            }
            return new ResponseDTO()
            {
                Code = 200,
                Message = "Updated user",
                Status = "Success"
            };
        }

        public UsersDTO GetAllUsers()
        {
            var result = context.Users.ToList();
            UsersDTO UsersDTO = new UsersDTO();
            UsersDTO.UserList = new List<UserDTO>();
            foreach (var User in result)
            {
                UsersDTO.UserList.Add(mapper.Map<UserDTO>(User));
            }
            return UsersDTO;
        }

        public ResponseAfterAutDTO GetIdAndRoleForUserById(string email)
        {
            logger.LogInformation("GetIdAndRoleForUserById");
            var user = context.Users.Where(i => i.Email == email).SingleOrDefault();
            var roleId = context.UserRoles.Where(i => i.UserId == user.Id).FirstOrDefault().RoleId;
            var roleName = context.Roles.Where(i => i.Id == roleId).SingleOrDefault().Name;
            bool isAdmin = (roleName == "Admin") ? true : false;
            return new ResponseAfterAutDTO
            {
                Code = 200,
                Message = "User logged",
                Status = "Success",
                IdUser = user.Id,
                Mail = email,
                IsAdmin = isAdmin
            };
        }
    }
}
