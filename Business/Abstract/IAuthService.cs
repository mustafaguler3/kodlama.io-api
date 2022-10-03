using Core.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(CreateUserDto user, string password);
        IDataResult<User> Login(UserLoginDto user);

        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
