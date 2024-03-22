using Library.Application.DTO;
using Library.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IAuthService
    {
        void RegisterUser(UserDTO model);
        string AuthenticateUser(AuthenticateModel model);
    }
}
