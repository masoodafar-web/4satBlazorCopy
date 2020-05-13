using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newFace.Server;
using newFace.Shared;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Shop;
using static newFace.Shared.Models.Resource.Enums;
using static newFace.Server.GlobalParametrs;
using System.Threading;

namespace newFace.Controllers.Api.Shop
{
    [ApiController]
    public class CartController : Controller
    {


        private readonly IProductRepository _ProductRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IBookRepository _bookRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommissionRepository _commissionRepository;
        private UserManager<ApplicationUser> UserManager;

        public CartController(IProductRepository productRepository, ICartRepository cartRepository, IQuestionRepository questionRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository, ITransactionRepository transactionRepository, IUserRepository userRepository, ICommissionRepository commissionRepository, UserManager<ApplicationUser> userManager)
        {
            _ProductRepository = productRepository;
            _cartRepository = cartRepository;
            _questionRepository = questionRepository;
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _commissionRepository = commissionRepository;
            UserManager = userManager;
        }


        [HttpPost, Route("api/GetCart")]
        public ResultCart getcart([FromBody] Request model)
        {
            ResultCart result = new ResultCart();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }

            result = _cartRepository.GetAll(model.UserId);

            return result;
        }

        [HttpPost, Route("api/AddToCart")]
        public Result AddCart([FromBody] RequestCart model)
        {
            Result result = new Result();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            result = _cartRepository.Create(model.UserId, model.Id, model.CartType, model.ReciverUserId, model.ShareholderPercent);
            return result;
        }

        [HttpPost, Route("api/RemoveFromCart")]
        public Result removecart([FromBody] Request model)
        {
            Result result = new Result();
            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            result = _cartRepository.Delete(model.UserId, model.Id);
            return result;
        }

        [HttpPost, Route("api/PayResult")]

        public ResultPayResult PayResult([FromBody] RequestPayResult model)
        {
            var result = new ResultPayResult();
            if (!string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }

            var findbillbyid = _transactionRepository.GetBillById(int.Parse(model.FactorId));


            var userid = findbillbyid.Bill.UserId;

            var billstatus = _transactionRepository.ShowBill(int.Parse(model.FactorId), userid);

            if (billstatus.Statue == Enums.Statue.Success && billstatus.Bill.Status == 1)
            {
                result.Bill = billstatus.Bill;
                result.Message = "پرداخت قبلا انجام شده";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            var apicode = GlobalParametrs.Zarin;

            var paymentstatus =
                _paymentRepository.PaymentVerification(apicode, model.Authority, int.Parse(model.TotalPrice));

            var EmptyCart = _cartRepository.EmptyCart(userid);

            if (paymentstatus.Statue == Enums.Statue.Success)
            {
                var paymentcomplete = _cartRepository.CompeletePayment(int.Parse(model.FactorId), paymentstatus.RefrenceCode);



                //محاسبه کارمزد سهام و پورسانت
                var findBill = _transactionRepository.ShowBill(int.Parse(model.FactorId), userid);

                var factorForSaleList = _unitOfWork.FactorforsaleProductGR.GetAllIncluding(f => f.BillId == findBill.Bill.Id, i => i.Products).ToList();
                foreach (var item in factorForSaleList.Where(p => p.Products.ReferralRight > 0 || (p.Products.Shareholders != null ? p.Products.Shareholders.Any() : false)).ToList())
                {
                    if (item.Products.ReferralRight > 0)
                    {
                        var month = DateTime.Now.MiladiToJalaliMonth();
                        var year = DateTime.Now.MiladiToJalaliYear();
                        var DividendAmountHistory = new DividendAmountHistory
                        {
                            CDate = DateTime.Now,
                            Month = month,
                            Year = year,
                            OriginalAmount = item.Products.ReferralRight,
                            UserId = item.UserId
                        };

                        //ثبت مقادیر ورودی
                        _unitOfWork.DividendAmountHistoryGR.Add(DividendAmountHistory);


                        //کوئری لیست کاربرانی که در یوز جنولوژی آی دی یوزرآیدی ورودی رو دارن و جنولوژی تایپ آنها مربوط  به معرف است
                        var UserGenList = _unitOfWork.UserGeneologyGR.FindBy(p => p.UserId == item.UserId && (p.Geneologytype.SystemType == SystemType.ForsatReagent)).ToList();

                        //ذخیره فروش شخصی و گروهی کاربر
                        _commissionRepository.SaveSellUser(item.UserId, SystemType.ForsatReagent, item.Products.ReferralRight, true);
                        foreach (var item2 in UserGenList)
                        {
                            var GenTypeId = item2.GeneologyTypeId;
                            //لیست پلن های مخصوص معرف
                            var GenPlans = _unitOfWork.GeneologyPlanGR.FindBy(p => p.GeneologyTypeId == GenTypeId && p.GeneologyType.RowType == RowType.Sell).ToList();
                            foreach (var item3 in GenPlans)
                            {
                                switch (item3.Plan)
                                {
                                    case PlanEnum.Uni:
                                        _commissionRepository.UniGeneral(item3, item2, item.Products.ReferralRight, DividendAmountHistory.Id);
                                        break;
                                    case PlanEnum.Binary:
                                        _commissionRepository.BinaryGeneral(item3, item2, item.Products.ReferralRight, DividendAmountHistory.Id);
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }
                    }
                    if (item.Products.Shareholders != null)
                    {

                        if (item.Products.Shareholders.Any())
                        {
                            foreach (var item5 in item.Products.Shareholders)
                            {
                                var month = DateTime.Now.MiladiToJalaliMonth();
                                var year = DateTime.Now.MiladiToJalaliYear();
                                var Amount = item5.Percent * item5.Product.ShareholderUnitPrice;
                                var DividendAmountHistory = new DividendAmountHistory
                                {
                                    CDate = DateTime.Now,
                                    Month = month,
                                    Year = year,
                                    OriginalAmount = Amount,
                                    UserId = item5.UserId
                                };

                                //ثبت مقادیر ورودی
                                _unitOfWork.DividendAmountHistoryGR.Add(DividendAmountHistory);


                                //کوئری لیست کاربرانی که در یوز جنولوژی آی دی یوزرآیدی ورودی رو دارن و جنولوژی تایپ آنها مربوط  به معرف است
                                var UserGenList = _unitOfWork.UserGeneologyGR.FindBy(p => p.UserId == item5.UserId && (p.Geneologytype.SystemType == SystemType.ForsatShareholder)).ToList();

                                //ذخیره فروش شخصی و گروهی کاربر
                                _commissionRepository.SaveSellUser(item5.UserId, SystemType.ForsatShareholder, Amount, true);
                                foreach (var item2 in UserGenList)
                                {
                                    var GenTypeId = item2.GeneologyTypeId;
                                    //لیست پلن های مخصوص معرف
                                    var GenPlans = _unitOfWork.GeneologyPlanGR.FindBy(p => p.GeneologyTypeId == GenTypeId && p.GeneologyType.RowType == RowType.Sell).ToList();
                                    foreach (var item3 in GenPlans)
                                    {
                                        switch (item3.Plan)
                                        {
                                            case PlanEnum.Uni:
                                                _commissionRepository.UniGeneral(item3, item2, Amount, DividendAmountHistory.Id);
                                                break;
                                            case PlanEnum.Binary:
                                                _commissionRepository.BinaryGeneral(item3, item2, Amount, DividendAmountHistory.Id);
                                                break;
                                            default:
                                                break;
                                        }

                                    }
                                }
                            }

                        }
                    }
                }


            }


            var bill = _transactionRepository.ShowBill(int.Parse(model.FactorId), userid);
            if (bill.Statue != Enums.Statue.Success)
            {
                result.Message = "پرداخت انجام نشد";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            else
            {
                result.Bill = billstatus.Bill;
                result.Message = "پرداخت انجام شد";
                result.Statue = Enums.Statue.Success;
                return result;

            }

        }
        [HttpPost, Route("api/Pay")]
        public ResultPayment Pay([FromBody] Request model)
        {
            ResultPayment finalresult = new ResultPayment();

            if (model.Id != 1 && model.Id != 2)
            {
                finalresult.Statue = Enums.Statue.Failure;
                finalresult.Message = "نوع پرداخت نامشخص است";
                return finalresult;
            }

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    finalresult.Message = "لطفا وارد شوید";
                    finalresult.Statue = Enums.Statue.Failure;
                    return finalresult;
                }

                model.UserId = checkUser.User.Id;
            }

            var userid = model.UserId;
            var user = UserManager.Users.FirstOrDefault(p => p.Id == userid);

            if (model.Id == 1)
            {
                var result = _cartRepository.Payment(userid, BillType.Credit);
                if (result.Statue == Enums.Statue.Success)
                {
                    var decreaseCredit = _cartRepository.CreateCredit(userid,
                        result.Bill.TotalPrice,
                        TransactionTypeEnum.Decrease);
                    //CultureInfo.InvariantCulture
                    if (decreaseCredit.Statue == Enums.Statue.Success)
                    {

                        var u = new utility();
                        string code = u.StringGenerator(8, false, false, true, false);

                        var paymentcomplete = _cartRepository.CompeletePayment(result.Bill.Id, code);

                        if (paymentcomplete.Statue == Enums.Statue.Success)
                        {
                            var completewallet = _cartRepository.EditCredit(user, decreaseCredit.Wallet.Id, code);
                            if (completewallet.Statue == Enums.Statue.Success)
                            {
                                var EmptyCart = _cartRepository.EmptyCart(userid);

                                finalresult.Statue = Enums.Statue.Success;
                                finalresult.Message = "پرداخت از اعتبار حساب با موفقیت انجام شد";
                                return finalresult;
                            }
                            else
                            {
                                finalresult.Statue = Enums.Statue.Failure;
                                finalresult.Message = completewallet.Message;
                                return finalresult;
                            }
                        }
                        else
                        {
                            finalresult.Statue = paymentcomplete.Statue;
                            finalresult.Message = paymentcomplete.Message;
                            return finalresult;
                        }
                    }
                    else
                    {
                        finalresult.Statue = Enums.Statue.Failure;
                        finalresult.Message = decreaseCredit.Message;
                        return finalresult;
                    }
                }
                else
                {
                    finalresult.Statue = Enums.Statue.Failure;
                    finalresult.Message = result.Message;
                    return finalresult;
                }
            }
            else
            {
                var result = _cartRepository.Payment(userid, BillType.PaymentGateway);
                if (result.Statue == Enums.Statue.Success)
                {
                    if (result.Bill.TotalPrice < 1000)
                    {
                        finalresult.Statue = Enums.Statue.Failure;
                        finalresult.Message = "حداقل مبلغ قابل پرداخت 1000 تومان است";
                        return finalresult;

                    }
                    if (result.Bill.TotalPrice > 999999999)
                    {
                        finalresult.Statue = Enums.Statue.Failure;
                        finalresult.Message = "مبلغ پرداختی از حدمجاز بالاتر میباشد";
                        return finalresult;
                    }

                    //apicode منقضی شده
                    var apicode = GlobalParametrs.Zarin;

                    var payresult = _paymentRepository.RequestPayment(apicode, userid, result.Bill.TotalPrice,
                        "فروشگاه فرصت", result.Bill.Id, GetUrl, "payResult", model.ReturnFrom);

                    if (payresult.Statue == Enums.Statue.Success)
                    {
                        finalresult.UrlReturn = payresult.UrlReturn;
                        finalresult.Statue = Enums.Statue.Success;
                        finalresult.Message = payresult.Message;
                        return finalresult;
                    }
                    else
                    {

                        finalresult.Statue = payresult.Statue;
                        finalresult.Message = payresult.Message;
                        return finalresult;
                    }
                }

                else
                {

                    finalresult.Statue = result.Statue;
                    finalresult.Message = result.Message;
                    return finalresult;
                }


            }
        }

        [HttpPost, Route("api/GetTransactions")]
        public ResultTransaction gettransactions([FromBody] Request model)
        {
            //ResultTransaction result = new ResultTransaction();
            ResultTransaction result = new ResultTransaction();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            var bills = _transactionRepository.GetAll(model.UserId);


            return bills;
        }

        [HttpPost, Route("api/RePay")]
        public ResultPayment repay([FromBody] Request model)
        {
            ResultPayment result = new ResultPayment();
            ResultUser checkUser = new ResultUser();
            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            var userid = model.UserId;

            var showbill = _transactionRepository.ShowBill(model.Id.Value, userid);
            if (showbill.Bill.BillType == BillType.Credit)
            {

                var decreaseCredit = _cartRepository.CreateCredit(userid,
                    showbill.Bill.TotalPrice,
                    TransactionTypeEnum.Decrease);
                //CultureInfo.InvariantCulture
                if (decreaseCredit.Statue == Enums.Statue.Success)
                {

                    var u = new utility();
                    string code = u.StringGenerator(8, false, false, true, false);

                    var paymentcomplete = _cartRepository.CompeletePayment(showbill.Bill.Id, code);

                    if (paymentcomplete.Statue == Enums.Statue.Success)
                    {
                        var completewallet = _cartRepository.EditCredit(checkUser.User, decreaseCredit.Wallet.Id, code);
                        if (completewallet.Statue == Enums.Statue.Success)
                        {
                            var EmptyCart = _cartRepository.EmptyCart(userid);

                            result.Statue = Enums.Statue.Success;
                            result.Message = "پرداخت از اعتبار حساب با موفقیت انجام شد";
                            result.PaymentType = BillType.Credit;
                            return result;
                        }
                        else
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = completewallet.Message;
                            return result;
                        }
                    }
                    else
                    {
                        result.Statue = paymentcomplete.Statue;
                        result.Message = paymentcomplete.Message;
                        return result;
                    }
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = decreaseCredit.Message;
                    return result;
                }

            }
            else
            {

                if (showbill.Bill.TotalPrice < 1000)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "حداقل مبلغ قابل پرداخت 1000 تومان است";
                    return result;

                }
                if (showbill.Bill.TotalPrice > 999999999)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "مبلغ پرداختی از حدمجاز بالاتر میباشد";
                    return result;
                }

                //apicode منقضی شده
                var apicode = GlobalParametrs.Zarin;

                var payresult = _paymentRepository.RequestPayment(apicode, userid, showbill.Bill.TotalPrice,
                    "فروشگاه فرصت", showbill.Bill.Id, GetUrl, "payResult", model.ReturnFrom);

                if (payresult.Statue == Enums.Statue.Success)
                {
                    result.UrlReturn = payresult.UrlReturn;
                    result.Statue = Enums.Statue.Success;
                    result.Message = payresult.Message;
                    result.PaymentType = BillType.PaymentGateway;
                    return result;
                }
                else
                {

                    result.Statue = payresult.Statue;
                    result.Message = payresult.Message;
                    return result;
                }





            }
            //if (showbill.Statue == Enums.Statue.Success)
            //{
            //    var apicode = GlobalParametrs.Zarin;

            //    var payresult = _paymentRepository.RequestPayment(apicode, userid, showbill.Bill.TotalPrice,
            //            "فروشگاه فرصت", showbill.Bill.Id, GetUrl, "payResult", model.ReturnFrom);


            //    result.UrlReturn = payresult.UrlReturn;
            //    result.Statue = payresult.Statue;
            //    result.Message = payresult.Message;
            //    return result;

            //}

            //result.Statue = showbill.Statue;
            //result.Message = showbill.Message;
            //return result;
        }

        [HttpPost, Route("api/CreditCharge")]
        public ResultPayment creditcharge([FromBody] Request model)
        {
            ResultPayment result = new ResultPayment();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }

            if (Math.Abs(model.Price) < 1000)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا مبلغ را صحیح وارد نمایید";
                return result;
            }

            //apicode منقضی شده
            var apicode = GlobalParametrs.Zarin;
            var userid = model.UserId;

            var createwallet = _cartRepository.CreateCredit(userid, model.Price, TransactionTypeEnum.Increase);

            if (createwallet.Statue == Enums.Statue.Success)
            {
                var payment = _paymentRepository.RequestPayment(apicode, userid, model.Price, "شارژ کیف پول فرصت", createwallet.Wallet.Id,
                    GetUrl, "WalletCharge", Enums.ReturnFrom.Application);
                if (payment.Statue == Enums.Statue.Success)
                {
                    result.UrlReturn = payment.UrlReturn;
                    result.Statue = Enums.Statue.Success;
                    result.Message = payment.Message;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = payment.Message;
                    return result;
                }
            }
            result.Statue = createwallet.Statue;
            result.Message = createwallet.Message;
            return result;




        }

        [HttpPost, Route("api/GetWallet")]
        public ResultWallet GetWallet([FromBody] Request model)
        {
            ResultWallet result = new ResultWallet();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }

            result.Message = "لطفا وارد شوید";
            result.Statue = Enums.Statue.Failure;
            result.Wallets = _unitOfWork.WalletGR.FindBy(f => f.UserId == model.UserId).ToList();
            return result;

        }

        [HttpPost, Route("api/CreditChargeResult")]
        public ResultPayResult CreditChargeResult([FromBody] RequestPayResult model)
        {
            var result = new ResultPayResult();

            var credit = _cartRepository.GetCreditById(int.Parse(model.FactorId));
            var user = UserManager.Users.FirstOrDefault(f => f.Id == credit.Wallet.UserId);
            var apicode = GlobalParametrs.Zarin;


            Bill bill = new Bill
            {
                BillType = BillType.PaymentGateway,
                CDate = DateTime.Now,
                PaymentDate = DateTime.Now,

                Status = 1,
                TotalDiscount = 0,
                TotalPrice = double.Parse(model.TotalPrice),
                TotalUnitPrice = double.Parse(model.TotalPrice),
                UserId = user.Id

            };

            var paymentVerification = _paymentRepository.PaymentVerification(apicode, model.Authority, int.Parse(model.TotalPrice));
            if (paymentVerification.Statue == Enums.Statue.Success)
            {
                bill.RefId = paymentVerification.RefrenceCode;

                var save = _unitOfWork.BillGR.Add(bill);
                if (save.Statue == Enums.Statue.Success)
                {
                    var editcredit =
                        _cartRepository.EditCredit(user, int.Parse(model.FactorId), paymentVerification.RefrenceCode);

                    if (editcredit.Statue == Enums.Statue.Success)
                    {
                        int iddwallet = int.Parse(model.FactorId);
                        var addbillid = _unitOfWork.WalletGR.GetById(iddwallet);
                        if (addbillid != null)
                        {
                            addbillid.BillId = bill.Id;
                            _unitOfWork.WalletGR.Update(addbillid);
                        }


                    }
                }



            }
            else
            {
                _unitOfWork.BillGR.Add(bill);
                int walletid = int.Parse(model.FactorId);
                var findwallet = _unitOfWork.WalletGR.GetById(walletid);
                if (findwallet != null)
                {
                    findwallet.Statue = 0;
                    findwallet.BillId = bill.Id;
                    _unitOfWork.WalletGR.Update(findwallet);
                }
            }

            var Rbill = _transactionRepository.ShowBill(bill.Id, user.Id);
            if (Rbill.Statue != Enums.Statue.Success)
            {
                result.Bill = bill;
                result.Message = "پرداخت انجام نشد";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            else
            {
                result.Bill = bill;
                result.Message = "پرداخت انجام شد";
                result.Statue = Enums.Statue.Success;
                return result;

            }

        }

    }
}
