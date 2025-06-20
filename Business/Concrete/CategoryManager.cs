using Business.Abstract;
using Business.BusinessAspect.Autofac;
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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


        [SecuredOperation("admin", Priority = 1)]
        [ValidationAspect(typeof(CategoryValidator), Priority = 2)]
        [TransactionScopeAspect(Priority = 3)]
        [PerformanceAspect(1, Priority = 4)]
        [CacheRemoveAspect("ICategoryService.Get", Priority = 5)]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult();
        }

        [SecuredOperation("admin", Priority = 1)]
        [CacheAspect(3, Priority = 2)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Category>> GetAll()
        {
            var data = _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(data);
        }

        public IDataResult<Category> GetCategoryById(int id)
        {
            var data = _categoryDal.Get(x => x.CategoryId == id);
            return new SuccessDataResult<Category>(data);
        }
    }
}
