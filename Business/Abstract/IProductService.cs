using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetList();

        IDataResult<List<Product>> GetListByCategory(int categoryId);
        
        IResult Add(Product product);
        
        IResult Update(Product product);

        IResult Delete(Product product);

        IResult TransactionOperation(Product product);

    }
}
