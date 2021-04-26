using Bussiness.Abstract;
using Bussiness.BusinessAspects.Autofac;
using Bussiness.CCS;
using Bussiness.Constants;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussiness.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result=BusinessRules.Run(CheckIfProductNameExist(product.ProductName)
                ,CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }
          
              _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
  
        }

       [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //İş kodları
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p => p.CategoryId == id));  //lamda veririz filtreleme yaptığı için expression
        }
        [CacheAspect]
        [PerformanceAspect(10)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 14)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>> (_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 7)
            {
                return new ErrorResult(Messages.ProductCountOfCategoriError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p=>p.ProductName==productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameErrorAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 7)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionTest(Product product)
        {
            Add(product);
                
            
                if (product.UnitPrice < 10)
                {
                   throw new Exception("");
                }
             return Add(product);
            
        }
    }
    
}
   

