using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules;
using Core.Aspects;
using Core.Aspects.Transaction;
using Core.Aspects.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {            
            IResult result = CheckIfProductNameExists(product.ProductName);
            
            if (result != null)
            {
               return result;
            }
            _productRepository.Add(product);
            return new SuccessResult(Message.ProductAdded);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productRepository.Get(p => p.ProductName == productName) != null)
            {
                return new ErrorResult(Message.ProductNameAlreadyExists);
            }

            return null;
        }

        public IResult Delete(Product product)
        {
            _productRepository.Delete(product);
            return new SuccessResult(Message.ProductDeleted);
        }

        [SecuredOperation("Product.List,Admin")]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productRepository.GetAll());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productRepository.GetAll(i => i.CategoryId == categoryId).ToList());
        }

        [TransactionScopeAspect]
        public IResult TransactionOperation(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Add(product);
            //bir işlem başarılı bir işlem başarısız olsun diye yazdık öyle
            return new SuccessResult(Message.ProductUpdated);
        }

        public IResult Update(Product product)
        {
            _productRepository.Update(product);
            return new SuccessResult(Message.ProductUpdated);
        }
    }
}
