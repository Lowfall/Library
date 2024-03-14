using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IGenericTokenService<T> where T : class
    {
        ClaimsIdentity GenerateClaims(T obj);
        string GenerateToken(ClaimsIdentity claimsIdentity, DateTime expiresTime);    
    }
}
