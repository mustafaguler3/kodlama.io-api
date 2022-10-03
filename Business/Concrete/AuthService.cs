using Business.Abstract;
using Business.Constants;
using Core.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results;
using Core.Utilities.Security.jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken,Message.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserLoginDto user)
        {
            var userCheck = _userService.GetByEmail(user.Email);

            if (userCheck == null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(user.Password,userCheck.PasswordHash,userCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Message.PasswordError);
            }

            return new SuccessDataResult<User>(userCheck, Message.SuccessLogin);
        }

        public IDataResult<User> Register(CreateUserDto user, string password)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);

            var u = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.Add(u);

            return new SuccessDataResult<User>(u);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByEmail(email) != null)
            {
                return new ErrorDataResult<User>(Message.UserAlreadyTaken);
            }

            return new SuccessResult();
        }
    }
}
