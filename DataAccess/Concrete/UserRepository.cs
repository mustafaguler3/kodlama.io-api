using Core.Abstract.EF;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserRepository : EntityRepository<User, VtContext>, IUserRepository
    {

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new VtContext())
            {
                var result = from i in context.OperationClaims
                             join c in context.UserOperationClaims on i.Id equals c.OperationClaimId
                             where c.UserId == user.Id
                             select new OperationClaim()
                             {
                                 Id = i.Id,
                                 Name = i.Name
                             };

                return result.ToList();
            }

            
        }
    }
}
