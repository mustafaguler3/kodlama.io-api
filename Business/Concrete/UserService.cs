using Business.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.Get(i => i.Email == email);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userRepository.GetClaims(user);
        }
    }
}
