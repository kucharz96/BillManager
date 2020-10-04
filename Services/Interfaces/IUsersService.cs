using BillManager.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Services.Interfaces
{
    public interface IUsersService
    {
        ResponseDTO EditUser(UserDTO userDTO);
        ResponseAfterAutDTO GetIdAndRoleForUserById(string email);
        UsersDTO GetAllUsers();

    }
}
