using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.User;
using Microsoft.AspNetCore.Identity;
using newFace.Server.Data;
using newFace.Server.Services.Generic;
using Bill = newFace.Shared.Models.Financial.Bill;
using static newFace.Shared.Models.Resource.Enums;
using Microsoft.EntityFrameworkCore;

namespace newFace.Server.Services.Education
{
    public class CartRepository : ICartRepository
    {

        private readonly ICommissionRepository _commissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISuggestionRepository _suggestionRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public CartRepository(ICommissionRepository commissionRepository, IUserRepository userRepository, IGiftRepository giftRepository, ISkillRepository skillRepository, ISuggestionRepository suggestionRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _commissionRepository = commissionRepository;
            _userRepository = userRepository;
            _giftRepository = giftRepository;
            _skillRepository = skillRepository;
            _suggestionRepository = suggestionRepository;
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }

        public Result Create(string userid, int? productid, CartType cartType, string RecieverUserId, int? ShareholderPercent)
        {
            Result result = new Result();

            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا وارد شوید";
                return result;
            }

            if (productid == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه محصول را وارد نمایید";
                return result;
            }

            var product = _unitOfWork.ProductGR.GetById(productid.Value);

            if (product == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = " شناسه محصول ارسالی صحیح نیست";
                return result;
            }

            if (cartType != CartType.Shareholder)
            {
                bool IfBuoght = false;
                if (cartType == CartType.Gift)
                {
                    IfBuoght = _unitOfWork.FactorforsaleProductGR.FindBy(f => f.UserId == RecieverUserId && f.ProductId == productid).Any() || _unitOfWork.GiftGR.Any(p => p.UserResiv == RecieverUserId && p.PorductId == productid);
                }
                else
                {
                    IfBuoght = _unitOfWork.FactorforsaleProductGR.Any(f => f.UserId == userid && f.ProductId == productid && f.BuyType == BuyType.Normal) || _unitOfWork.GiftGR.Any(p=>p.UserResiv == userid && p.PorductId == productid);
                }

                if (IfBuoght)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = cartType == CartType.Normal ? "این محصول یا قبلا توسط شما خریداری یا به شما هدیه داده شده است." : "کاربر مورد نظر این محصول را قبلا خریداری کرده یا هدیه گرفته";
                    return result;
                }
            }



            var find_same_product_in_cart = IfExist(userid, productid, RecieverUserId, cartType);

            if (find_same_product_in_cart)
            {
                if (cartType == CartType.Gift)
                {
                    if (string.IsNullOrEmpty(RecieverUserId))
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "لطفا کاربر دریافت کننده هدیه را مشخص نمایید";
                        return result;
                    }

                    var gift = new Gift()
                    {
                        Date = DateTime.Now,
                        PorductId = productid.Value,
                        UserSend = userid,
                        UserResiv = RecieverUserId,
                        Status = false
                    };
                    _giftRepository.Create(gift);
                }


                Cart cart = new Cart
                {
                    ProductId = productid.Value,
                    CartType = cartType,
                    UserId = userid,
                    SubmitDate = DateTime.Now,
                    RecieverUserId = RecieverUserId,
                };

                if (ShareholderPercent != null)
                {
                    if (product.ShareholderPercentForSell - product.ShareholderPercentSold < ShareholderPercent.Value)
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "درصد سهام درخواستی بیش از ظرفیت موجود است";
                        return result;
                    }

                    cart.ShareholderPercent = ShareholderPercent.Value;
                }

                var savetocart = _unitOfWork.CartGR.Add(cart);


                string savetocartmsg = "";

                switch (cartType)
                {
                    case CartType.Normal:
                        savetocartmsg = "اضافه محصول به سبد خرید";
                        break;
                    case CartType.Gift:
                        savetocartmsg = "اضافه محصول به سبد هدیه";
                        break;
                    case CartType.Shareholder:
                        savetocartmsg = "اضافه سهام محصول به سبد خرید";
                        break;
                    default:
                        break;
                }

                result.Statue = savetocart.Statue;
                result.Message = savetocart.Message;
                return result;

            }
            else
            {
                result.Statue = Enums.Statue.Repetitive;
                if (cartType == CartType.Shareholder)
                {
                    result.Message = "سهام این محصول قبلا به سبد خرید شما اضافه شده است";
                }
                else
                {
                    result.Message = "این محصول قبلا به سبد خرید/هدیه شما اضافه شده است";
                }

                return result;
            }

        }

        public bool IfExist(string userid, int? productid, string RecieverUserId, CartType cartType)
        {
            if (string.IsNullOrEmpty(userid) || productid == null)
            {
                return false;
            }

            bool ifexist = false;
            if (cartType == CartType.Normal)
            {
                ifexist = _unitOfWork.CartGR.Any(f => f.UserId == userid && f.ProductId == productid && f.CartType == CartType.Normal);
            }
            if (cartType == CartType.Shareholder)
            {
                ifexist = _unitOfWork.CartGR.Any(f => f.UserId == userid && f.ProductId == productid && f.CartType == CartType.Shareholder);
            }

            var ifexistgift = _unitOfWork.CartGR.Any(f => f.UserId == userid && f.ProductId == productid && f.CartType == CartType.Gift && f.RecieverUserId == RecieverUserId);
            if (!ifexist && !ifexistgift)
            {
                return true;
            }

            return false;

        }

        public Result Delete(string userid, int? cartid)
        {
            Result result = new Result();


            if (cartid == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه سبد خرید ارسال نشده است";
                return result;
            }
            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "کاربر وارد نشده است";
                return result;
            }

            var delete = _unitOfWork.CartGR.FindBy(f => f.UserId == userid && f.Id == cartid).FirstOrDefault();
            if (delete == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "محصول مورد نظر در سبد شما یافت نشد";
                return result;
            }

            var deleteconfirm = _unitOfWork.CartGR.Delete(delete);

            if (deleteconfirm.Statue == Enums.Statue.Success)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "محصول با موفقیت حذف شد";
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "خطای حذف محصول از سبد خرید";
                return result;
            }


        }

        public Result EmptyCart(string userid)
        {
            Result result = new Result();



            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "کاربر وارد نشده است";
                return result;
            }

            var user = UserManager.Users.FirstOrDefault(u => u.Id == userid);

            if (user == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "کاربری با این مشخصات یافت نشد ";
                return result;
            }

            var delete = _unitOfWork.CartGR.GetAll().Where(f => f.UserId == userid).Include(c => c.Product).ThenInclude(c=>c.Courses).ThenInclude(c=>c.Videos).ToList();
            if (delete.Count == 0)
            {
                
                
                result.Statue = Enums.Statue.Failure;
                result.Message = "محصولی یافت نشد";
                return result;
            }


            foreach (var item in delete.Where(d => d.CartType == CartType.Shareholder))
            {
                var product = item.Product;

                //خرید سهام های همزمان
                if ((product.ShareholderPercentForSell - product.ShareholderPercentSold) < item.ShareholderPercent.Value)
                {
                    user.Point = user.Point + (item.ShareholderPercent.Value * product.ShareholderUnitPrice);
                }
                else
                {
                    product.ShareholderPercentSold += item.ShareholderPercent.Value;
                    _unitOfWork.ProductGR.Update(product);
                    var shareholderRepeat = _unitOfWork.ShareholderGR.FindBy(s => s.ProductId == item.ProductId && s.UserId == userid).FirstOrDefault();
                    if (shareholderRepeat != null)
                    {
                        shareholderRepeat.Percent += item.ShareholderPercent.Value;

                        _unitOfWork.ShareholderGR.Update(shareholderRepeat);
                    }
                    else
                    {
                        Shareholder shareholder = new Shareholder
                        {
                            ProductId = item.ProductId,
                            UserId = userid,
                            Percent = item.ShareholderPercent.Value
                        };

                        _unitOfWork.ShareholderGR.Add(shareholder);
                    }
                }

            }


            //if (!string.IsNullOrEmpty(user.ParentId))
            //{
            //    foreach (var item in delete.Where(d => d.CartType != CartType.Shareholder))
            //    {
            //        var product = item.Product;

            //        if (product != null)
            //        {
            //            Commission commission = new Commission()
            //            {
            //                ProductId = product.Id,
            //                Amount = product.Price,
            //                UserId = user.ParentId,
            //                SubsetId = userid,
            //                CommissionType = CommissionTypeEnum.SubsetId,
            //                Percent = GlobalParametrs.GetCommissionPercent
            //            };
            //            _commissionRepository.ReferralCommissionCalculator(commission);
            //        }

            //        var shareholders = _ShareholderService.FindBy(s => s.ProductId == product.Id).ToList();
            //        foreach (var shareholder in shareholders)
            //        {
            //            Commission commission = new Commission()
            //            {
            //                ProductId = product.Id,
            //                Amount = product.Price,
            //                UserId = shareholder.UserId,
            //                CommissionType = CommissionTypeEnum.Shareholder,
            //                Percent = shareholder.Percent
            //            };
            //            _commissionRepository.ReferralCommissionCalculator(commission);
            //        }

            //    }

            //}




            foreach (var item in delete)
            {
                if (item.CartType != CartType.Shareholder)
                {
                    string userId = "";
                    if (item.CartType == CartType.Normal)
                    {
                        userId = item.UserId;
                    }
                    if (item.CartType == CartType.Gift)
                    {
                        userId = item.RecieverUserId;
                    }
                    //افزودن در productseeninfo
                    var productSeenInfo = new ProductSeenInfo
                    {
                        ProductId = item.ProductId,
                        UserId = userId
                    };
                    _unitOfWork.ProductSeenInfoGR.Add(productSeenInfo);

                    //افزودن مهارت های شخص
                    var productScale = _unitOfWork.ProductScaleGR.GetAllIncluding(f => f.ProductId == item.ProductId, i => i.Category).ToList();
                    //حذف محصول خرید شده از محصولات پیشتهادی کاربر
                    var deletesuggestion = _suggestionRepository.DeleteProductFromSuggestion(userId, item.ProductId);

                    foreach (var itemProductScale in productScale)
                    {
                        if (itemProductScale.Category != null)
                        {
                            var skill = _unitOfWork.SkillGR.FirstOrDefault(a => a.CategoryId == itemProductScale.Category.Id && a.UserId == userId);
                            if (skill == null)
                            {
                                _skillRepository.AddSkill(new SkillViewModel()
                                {
                                    CategoryId = itemProductScale.Category.Id,
                                    CategoryName = itemProductScale.Category.Title,
                                    Credit = (long)itemProductScale.Credit,
                                    Percent = 0,
                                    IsUpdate = true,
                                    Lvl1 = true,
                                    //یافتن لول مقدماتی برای مهارت تازه وارد شده
                                    LevelId = _unitOfWork.LevelGR.FirstOrDefault(f => f.Number == 1).Id,
                                    SkillType = SkillType.Adventitious,
                                    UserId = userId
                                }, VisionType.Work);
                            }
                            else
                            {
                                if (skill.SkillType == SkillType.Assigned)
                                {
                                    //اگر از قبل وجود داشت نوع آن را =انتسابی کند و درصدش را =صفر
                                    skill.SkillType = SkillType.Adventitious;
                                    skill.Percent = 0;
                                    _unitOfWork.SkillGR.Update(skill);
                                }


                            }
                        }
                    }



                    if (item.Product.Type == ProductType.Course)
                    {
                        foreach (var video in item.Product?.Courses.FirstOrDefault()?.Videos)
                        {
                            var videoSeenInfo = new VideoSeenInfo
                            {
                                UserId = userId,
                                VideoId = video.Id
                            };

                            _unitOfWork.VideoSeenInfoGR.Add(videoSeenInfo);
                        }
                    }

                }
            }



            var deleteconfirm = _unitOfWork.CartGR.DeleteRange(delete);


            if (deleteconfirm.Statue == Enums.Statue.Success)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "محصولات از سبدخرید شما با موفقیت حذف شدند";
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "خطای حذف محصولات از سبد خرید";
                return result;
            }


        }

        public ResultCart GetAll(string userid)
        {
            ResultCart result = new ResultCart();
            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه کاربر وارد نشده است";
                result.Carts = new List<Cart>();
                return result;
            }

            var cartlist = _unitOfWork.CartGR.GetAllIncluding(w => w.UserId == userid,
                    i => i.Product, i => i.ApplicationUser).ToList()
                    .Select(

                        s =>
                            new Cart
                            {
                                Id = s.Id,
                                UserId = s.UserId,
                                Product = s.Product,
                                ProductId = s.ProductId,
                                Credit = s.Product.ProductScale?.Sum(w => w.Credit),
                                ApplicationUser = s.ApplicationUser,
                                CartType = s.CartType,
                                RecieverUserId = s.RecieverUserId,
                                ShareholderPercent = s.ShareholderPercent,
                                SubmitDate = s.SubmitDate
                            }
                        )
                ;


            //چک کردن سهام های تکمیل ظرفیت شده
            var invalidCartList = cartlist.Where(c => c.CartType == CartType.Shareholder).Where(c => (c.Product.ShareholderPercentForSell - c.Product.ShareholderPercentSold) < c.ShareholderPercent);
            if (invalidCartList != null && invalidCartList.Any())
            {
                _unitOfWork.CartGR.DeleteRange(invalidCartList);
            }



            CartViewModel cvm = new CartViewModel();

            cvm.Carts.AddRange(cartlist);
            cvm.CartCount = cartlist.Count();
            cvm.TotalAmount = cartlist.Any() ? cartlist.Where(c => c.CartType != CartType.Shareholder).Sum(s => s.Product.Price) : 0;
            cvm.TotalDiscount = cartlist.Any() ? cartlist.Where(c => c.CartType != CartType.Shareholder).Sum(s => (s.Product.Price * s.Product.Discount) / 100) : 0;
            cvm.TotalPriceToPay = cvm.TotalAmount - cvm.TotalDiscount;
            cvm.TotalPoint = cartlist.Any() ? cartlist.Where(c => c.CartType != CartType.Shareholder).Sum(s => s.Product.ProductScale != null? s.Product.ProductScale.Sum(ss => ss.Credit) : 0) : 0;
            cvm.User = UserManager.FindByIdAsync(userid).Result;

            //سهام
            cvm.TotalPriceToPay = cvm.TotalPriceToPay + cartlist.Where(c => c.CartType == CartType.Shareholder).Sum(s => s.ShareholderPercent.Value * s.Product.ShareholderUnitPrice);
            cvm.TotalAmount = cvm.TotalAmount + cartlist.Where(c => c.CartType == CartType.Shareholder).Sum(s => s.ShareholderPercent.Value * s.Product.ShareholderUnitPrice);

            //ایا کیف پول اعتبار کافی را دارد
            cvm.PayFromCredit = cvm.User.WalletCredit >= cvm.TotalPriceToPay ? true : false;

            if (cartlist.Any())
            {


                result.CartViewModel = cvm;
                result.Statue = Enums.Statue.Success;
                result.Message = "لیست باموفقیت ارسال شد.";
                // result.Carts = cartlist;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "موردی یافت نشد";
                result.Carts = new List<Cart>();
                result.CartViewModel = cvm;
                return result;
            }
        }

        public ResultCart CreateCredit(string userid, double Amount, TransactionTypeEnum transactionType)
        {
            ResultCart result = new ResultCart();

            if (string.IsNullOrEmpty(userid))
            {
                result.Message = "شناسه کاربر وارد نشده است";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            if (Amount == 0.00)
            {
                result.Message = "لطفا مقدار شارژ را وارد نمایید";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            var user = UserManager.Users.FirstOrDefault(f => f.Id == userid);

            Wallet W = new Wallet
            {
                Statue = Enums.Statue.Failure,
                Amount = Amount,
                CDate = DateTime.Now,
                CUserId = userid,
                RefId = null,
                TransactionType = transactionType,
                UserId = userid
            };



            var save = _unitOfWork.WalletGR.Add(W);
            if (save.Statue == Enums.Statue.Success)
            {
                user.WalletCredit += (long)Amount;
                UserManager.UpdateAsync(user).Wait();
                result.Statue = Enums.Statue.Success;
                result.Wallet = W;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
            }
            result.Message = save.Message;
            return result;
        }


        public Result EditCredit(ApplicationUser user, int WalletId, string referenceCode)
        {

            Result result = new Result();


            if (user == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "‍کاربر یافت نشد";
                return result;
            }

            

            var findwallet = _unitOfWork.WalletGR.FirstOrDefault(f => f.UserId == user.Id && f.Id == WalletId);
            if (findwallet == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "کیف پول مورد نظر یافت نشد";
                return result;
            }

            findwallet.Statue = Enums.Statue.Success;
            findwallet.EDate = DateTime.Now;
            findwallet.RefId = referenceCode;

            var stat = findwallet.TransactionType == TransactionTypeEnum.Increase
                ? user.WalletCredit += (long)findwallet.Amount
                : user.WalletCredit -= (long)findwallet.Amount;
            _unitOfWork.WalletGR.Update(findwallet);

            var increasecredit = UserManager.UpdateAsync(user).Result;

            result.Statue = increasecredit.Succeeded ? Enums.Statue.Success : Enums.Statue.Failure;
            result.Message = findwallet.TransactionType == TransactionTypeEnum.Increase ? "افزایش اعتبار کیف پول" : "برداشت از اعتبار کیف پول";
            return result;

        }

        public ResultCart GetCreditById(int? id)
        {
            ResultCart result = new ResultCart();

            if (id == null)
            {
                result.Message = "لطفا شناسه کیف پول را ارسال کنید";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            var wallet = _unitOfWork.WalletGR.GetById(id.Value);

            if (wallet != null)
            {
                result.Message = "کیف پول ارسال شد";
                result.Statue = Enums.Statue.Success;
                result.Wallet = wallet;
                return result;
            }
            result.Message = "کیف پول یافت نشد";
            result.Statue = Enums.Statue.Failure;
            return result;
        }

        //public int CartCount(string userid)
        //{
        //    var count = _unitOfWork.CartGR.Count(c => c.UserId == userid);

        //    return count;
        //}

        public ResultCart Payment(string userid,BillType BillType)
        {
            ResultCart result = new ResultCart();
            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه کاربر اشتباه است";
                result.Carts = new List<Cart>();
                return result;
            }



            var cartlist = _unitOfWork.CartGR.GetAllIncluding(w => w.UserId == userid,
                    i => i.Product.ProductScale, i => i.Product).ToList();

            var bill = new Bill
            {
                CDate = DateTime.Now,
                Status = 0,
                TotalUnitPrice = cartlist.Where(c => c.CartType != CartType.Shareholder).Sum(s => s.Product.Price),
                TotalDiscount = cartlist.Where(c => c.CartType != CartType.Shareholder).Sum(s => (s.Product.Price * s.Product.Discount) / 100),
                UserId = userid,
                BillType = BillType
            };
            bill.TotalPrice = bill.TotalUnitPrice - bill.TotalDiscount;

            bill.TotalPrice = bill.TotalPrice + cartlist.Where(c => c.CartType == CartType.Shareholder).Sum(cc => cc.ShareholderPercent.Value * cc.Product.ShareholderUnitPrice);

            if (BillType == BillType.Credit)
            {
                var user = UserManager.Users.FirstOrDefault(f => f.Id == userid);
                if (user.WalletCredit < bill.TotalPrice)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "اعتبار کیف پول شما کافی نیست";
                    return result;
                }
            }
            //---------------------//
            var savestatus = _unitOfWork.BillGR.Add(bill);


            if (savestatus.Statue == Enums.Statue.Success)
            {
                List<FactorforsaleProduct> FFP = new List<FactorforsaleProduct>();

                foreach (var item in cartlist)
                {
                    var factordetail = new FactorforsaleProduct
                    {
                        BillId = bill.Id,
                        Count = 1,
                        Discount = (item.Product.Price * item.Product.Discount) / 100,
                        ProductId = item.ProductId,
                        UnitPrice = item.Product.Price,
                        TotalPrice = item.Product.Price - ((item.Product.Price * item.Product.Discount) / 100),
                        UserId = userid,
                        BuyType = item.CartType == CartType.Normal ? BuyType.Normal : item.CartType == CartType.Gift ? BuyType.Gift: BuyType.Shareholder
                    };

                    //سهام
                    if (item.CartType == CartType.Shareholder)
                    {
                        factordetail.UnitPrice = item.Product.ShareholderUnitPrice;
                        factordetail.TotalPrice = item.ShareholderPercent.Value * factordetail.UnitPrice;
                        factordetail.Discount = 0;
                        factordetail.Count = item.ShareholderPercent.Value;
                        factordetail.BuyType = BuyType.Shareholder;
                    }


                    FFP.Add(factordetail);
                    if (_unitOfWork.GiftGR.FindBy(p => p.PorductId == item.ProductId && p.UserSend == userid && p.Status == false).Any())
                    {
                        var gift = _unitOfWork.GiftGR.FindBy(p => p.PorductId == item.ProductId && p.UserSend == userid && p.Status == false).FirstOrDefault();
                        gift.Status = true;
                        gift.BillId = bill.Id;
                        _giftRepository.Edit(gift);

                    }
                }

                var savedetails = _unitOfWork.FactorforsaleProductGR.AddRange(FFP);
                if (savedetails.Statue == Enums.Statue.Success)
                {
                    result.Bill = bill;
                    result.Statue = Enums.Statue.Success;
                    result.Message = "صورتحساب با موفقیت ایجاد گردید";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ثبت جزییات صورتحساب با خطا مواجه شده است.لطفا دوباره تلاش کنید";
                    return result;
                }

            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "خطای ایجاد صورتحساب.لطفا دوباره تلاش کنید.";
                return result;
            }

        }


        public ResultCart CompeletePayment(int billId, string referenceCode)
        {
            ResultCart result = new ResultCart();

            var bill = _unitOfWork.BillGR.GetById(billId);
            bill.PaymentDate = DateTime.Now;
            bill.RefId = referenceCode;
            bill.Status = 1;
            _unitOfWork.BillGR.Update(bill);

            result.Statue = Enums.Statue.Success;
            result.Message = "صورتحساب با موفقیت پرداخت شد";
            return result;
        }



    }
}