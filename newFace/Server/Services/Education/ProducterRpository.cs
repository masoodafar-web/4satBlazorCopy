using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;

namespace newFace.Server.Services.Education
{
    public class ProducterRpository : IProducterRepository
    {
        private IUnitOfWork _unitOfWork;

        public ProducterRpository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ResultProducter AddProducter(ProducterViewModel producterViewModel)
        {
            if (producterViewModel != null)
            {
                var producter = producterViewModel.ToProducter();
                if (_unitOfWork.ProducterGR.Update(producter).Statue == Enums.Statue.Success)
                {
                    var IsExist = _unitOfWork.ProducterTypeGR.Any(a =>
                        a.ProducterId == producter.Id && a.Type == producterViewModel.EnumType);
                    if (!IsExist)
                    {
                        var resultType = _unitOfWork.ProducterTypeGR.Add(new ProducterType()
                        {
                            ProducterId = producter.Id,
                            Type = producterViewModel.EnumType
                        });
                        if (resultType.Statue == Enums.Statue.Success)
                        {
                            return new ResultProducter()
                            {
                                Statue = Enums.Statue.Success,
                                Message = resultType.Message,
                                ProducterViewModel = producterViewModel
                            };
                        }
                        else
                        {
                            _unitOfWork.ProducterGR.Delete(producter);
                            return new ResultProducter()
                            {
                                Statue = Enums.Statue.Failure,
                                Message = "خطا در ثبت کامل داده"
                            };
                        }
                    }
                    else
                    {
                        return new ResultProducter()
                        {
                            Statue = Enums.Statue.Success,
                            Message = "این رکورد قبلا ثبت شده است",
                            ProducterViewModel = producterViewModel
                        };
                    }

                }
                else
                {
                    return new ResultProducter()
                    {
                        Statue = Enums.Statue.Failure
                    };
                }
            }
            else
            {
                return new ResultProducter()
                {
                    Statue = Enums.Statue.Failure
                };
            }
        }

        public ResultProducter DeleteProducter(ProducterViewModel producterViewModel)
        {
            if (producterViewModel != null)
            {
                if (_unitOfWork.ProducterTypeGR.Delete(new ProducterType()
                {
                    ProducterId = producterViewModel.Id,
                    Type = producterViewModel.EnumType
                }).Statue == Enums.Statue.Success)
                {
                    var resultType = _unitOfWork.ProducterGR.Delete(producterViewModel.ToProducter());
                    if (resultType.Statue == Enums.Statue.Success)
                    {
                        return new ResultProducter()
                        {
                            Statue = Enums.Statue.Success,
                            Message = resultType.Message,
                            ProducterViewModel = producterViewModel
                        };
                    }
                    else
                    {
                        _unitOfWork.ProducterGR.Delete(producterViewModel.ToProducter());
                        return new ResultProducter()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "خطا در ثبت کامل داده"
                        };
                    }
                }
                else
                {
                    return new ResultProducter()
                    {
                        Statue = Enums.Statue.Failure
                    };
                }
            }
            else
            {
                return new ResultProducter()
                {
                    Statue = Enums.Statue.Failure
                };
            }
        }

        public ResultProducter UpdateProducter(ProducterViewModel producterViewModel)
        {
            if (producterViewModel != null)
            {
                var producter = producterViewModel.ToProducter();
                if (_unitOfWork.ProducterGR.Update(producter).Statue == Enums.Statue.Success)
                {
                    var resultType = _unitOfWork.ProducterTypeGR.Update(new ProducterType()
                    {
                        ProducterId = producter.Id,
                        Type = producterViewModel.EnumType
                    });
                    if (resultType.Statue == Enums.Statue.Success)
                    {
                        return new ResultProducter()
                        {
                            Statue = Enums.Statue.Success,
                            Message = resultType.Message,
                            ProducterViewModel = producterViewModel
                        };
                    }
                    else
                    {
                        _unitOfWork.ProducterGR.Delete(producter);
                        return new ResultProducter()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "خطا در ثبت کامل داده"
                        };
                    }
                }
                else
                {
                    return new ResultProducter()
                    {
                        Statue = Enums.Statue.Failure
                    };
                }
            }
            else
            {
                return new ResultProducter()
                {
                    Statue = Enums.Statue.Failure
                };
            }

        }
    }
}