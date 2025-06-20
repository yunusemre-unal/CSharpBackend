using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidaiton;
using Core.AOP.Autofac.Caching;
using Core.AOP.Autofac.Logging;
using Core.AOP.Autofac.Performance;
using Core.AOP.Autofac.Transaction;
using Core.AOP.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;

        }

        [SecuredOperation("admin", Priority = 1)]
        [ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [TransactionScopeAspect(Priority = 3)]
        [PerformanceAspect(1,Priority = 4)]
        [CacheRemoveAspect("IProductService.Get", Priority =5)]
        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }


        [SecuredOperation("admin",Priority =1)]
        [CacheAspect(3,Priority = 2)]
        [LogAspect(typeof(DatabaseLogger))]

        public IDataResult<List<Product>> GetAll()
        {
            //Thread.Sleep(5000);
            var data = _productDal.GetAll();
            return new SuccessDataResult<List<Product>>(data);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            var data = _productDal.GetAll(x => x.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(data);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            var data = _productDal.GetAll(x => x.UnitPrice > min && x.UnitPrice < max);
            return new SuccessDataResult<List<Product>>(data);
        }


        public IDataResult<Product> GetProductById(int id)
        {
            var data = _productDal.Get(x => x.ProductId == id);
            return new SuccessDataResult<Product>(data);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

    }
}
