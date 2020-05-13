using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared;
using newFace.Shared.Models;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace newFace.Server.Services.Resource
{
    public class CommissionRepository : ICommissionRepository
    {


        private int counter = 0;
        private IUnitOfWork _unitOfWork;
        HttpContext HttpContext=new DefaultHttpContext();
        private UserManager<ApplicationUser> UserManager;

        public CommissionRepository(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }
       public ResultCommission GetAll(string selfId, string subsetId)
        {
            ResultCommission result = new ResultCommission();
            try
            {

                List<Commission> list = _unitOfWork.CommissionGR.FindBy(c => c.UserId == selfId && string.IsNullOrEmpty(subsetId) ? c.Id != 0 : c.SubsetId == subsetId).ToList();
                if (list.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.CommissionList = list;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }

        }

        public Result Create(Commission commission)
        {
            Result result = new Result();

            try
            {
                _unitOfWork.CommissionGR.Add(commission);
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }
        }

        public Result ReferralCommissionCalculator(Commission commission)
        {

            //Result result = new Result();

            //try
            //{

            //    var user = UserManager.Users.FirstOrDefault(u => u.Id == commission.UserId);

            //    if (user == null)
            //    {
            //        result.Statue = Enums.Statue.Failure;
            //        result.Message = "کاربری با این مشخصات یافت نشد";
            //        return result;
            //    }

            //    commission.Fee = (commission.Amount * commission.Percent) / 100;

            //    var createResult = Create(commission);

            //    if (createResult.Statue == Enums.Statue.Success)
            //    {
            //        var increaseCreditResult = IncreaseUserCredit(commission.UserId, commission.Fee);
            //        if (increaseCreditResult.Statue == Enums.Statue.Success)
            //        {

            //            if (!string.IsNullOrEmpty(user.ParentId))
            //            {

            //                commission.Amount = commission.Fee;
            //                commission.SubsetId = commission.UserId;
            //                commission.UserId = user.ParentId;
            //                commission.Datetime = DateTime.Now;
            //                commission.CommissionType = CommissionTypeEnum.SubsetId;

            //                ReferralCommissionCalculator(commission);

            //                result.Statue = Enums.Statue.Success;
            //                return result;

            //            }
            //            else
            //            {
            //                result.Statue = Enums.Statue.Success;
            //                return result;
            //            }
            //        }
            //    }

            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "خطایی رخ داده است";
            //    return result;
            //}
            //catch (Exception e)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = e.Message;
            //    return result;
            //}
            var result = new Result();
            result.Statue = Enums.Statue.Failure;
            result.Message = "بعد از اصلاح parent از کامنت بیرون بیاد";
            return result;
        }

        public Result CachReferralCode(string parentId)
        {
            Result result = new Result();
            try
            {
                CookieOptions options = new CookieOptions()
                {
                    Expires = DateTime.Now.AddYears(10)
                };

                HttpContext.Response.Cookies.Append("ReferralCode", parentId, options);
                

                result.Statue = Enums.Statue.Success;
                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }



        }

        public string ReadReferralCode()
        {
            try

            {

                return HttpContext.Request.Cookies["ReferralCode"];
            }
            catch (Exception)
            {

                return null;
            }

        }

        public Result IncreaseUserCredit(string userId, double amount)
        {
            Result result = new Result();
            try
            {

                if (string.IsNullOrEmpty(userId))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی دریافت نشد";
                    return result;
                }
                ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.Id == userId);

                if (user != null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "یافت نشد";
                    return result;
                }

                if (amount == 0)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "مبلغ وارد شده صحیح نیست";
                    return result;
                }

                user.Credit = amount;
                //_db.Entry(user).State = EntityState.Modified;
                UserManager.UpdateAsync(user);

                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }


        #region Uni
        //-----------------------------------------------------------
        /// <summary>
        /// متد اصلی پلن یونی
        /// </summary>
        /// <param name="geneologyPlan"></param>
        /// <param name="userGeneology"></param>
        /// <param name="Amount"></param>
        public long UniGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount, int DividendAmountHistoryId)
        {
            //var Type = geneologyPlan.GeneologyType.Type;
            var PlanUni = _unitOfWork.PlanUniGR.FirstOrDefault(p => p.Id == geneologyPlan.PlanUniId.Value);
            var PlanUniLevel = _unitOfWork.PlanUniLevelGR.FindBy(p => p.PlanUniId == PlanUni.Id).ToList();
            long amountt = Convert.ToInt64((geneologyPlan.Percent / 100.0) * Amount);
            long SumDividAmount = 0;
            foreach (var item in PlanUniLevel)
            {
                var user = FindUserWithLevel(item.LevelNumber, userGeneology.UserId, userGeneology.GeneologyTypeId);
                if (user != null)
                {
                    var amount = PercentCalc(amountt, item.Percent);
                    var month = DateTime.Now.MiladiToJalaliMonth();
                    var year = DateTime.Now.MiladiToJalaliYear();
                    var commissionPerMonth = AddCommissionPerMonth(user, month, year, 0, FeeType.IndirectFees);
                    SumDividAmount += amount;
                    var commissionHistory = new CommissionHistory
                    {
                        UserGeneologyId = user.Id,
                        Year = year,
                        Month = month,
                        AmountFees = amount,
                        FeeType = FeeType.IndirectFees,
                        CommissionPerMonthId = commissionPerMonth.Id,
                        CDate = DateTime.Now,
                        PlanEnum=PlanEnum.Uni,
                        DividendAmountHistoryId = DividendAmountHistoryId
                    };
                    _unitOfWork.CommissionHistoryGR.Add(commissionHistory);
                    AddCommissionPerMonth(user, month, year, commissionHistory.AmountFees, FeeType.IndirectFees);

                    //اگر به پلنمون دلتا چسبیده بود
                    if (_unitOfWork.PlanDeltaGR.Any(p => p.GeneologyPlanId == geneologyPlan.Id))
                    {
                        DeltaGeneral(geneologyPlan, user, commissionHistory.AmountFees);
                    }
                }
            }

            //همه ی کامزد هایی که به افراد بالاسری داده رو جمع کردیم
            //و برمیگردونیم تا از مبلغ کارمزد مستقیم اصلی کم بشه
            return SumDividAmount;
        }
        //public long UniCalc(UserGeneology userGeneology, long Amount, List<PlanUniLevel> planUniLevels, int DividendAmountHistoryId)
        //{
        //    var user = FindUserWithLevel(planUniLevels[counter].LevelNumber, userGeneology.UserId, userGeneology.GeneologyTypeId);
        //    if (user != null)
        //    {
        //        var amount = PercentCalc(Amount, planUniLevels[counter].Percent);
        //        var month = DateTime.Now.MiladiToJalaliMonth();
        //        var year = DateTime.Now.MiladiToJalaliYear();
        //        var commissionPerMonth = AddCommissionPerMonth(user, month, year, 0, FeeType.IndirectFees);
        //        var commissionHistory = new CommissionHistory
        //        {
        //            UserGeneologyId = user.Id,
        //            Year = year,
        //            Month = month,
        //            AmountFees = amount,
        //            FeeType = FeeType.IndirectFees,
        //            CommissionPerMonthId = commissionPerMonth.Id,
        //            DividendAmountHistoryId = DividendAmountHistoryId
        //        };
        //        counter++;
        //        commissionHistory.AmountFees -= UniCalc(user, Amount, planUniLevels, DividendAmountHistoryId);
        //        _CommissionHistoryService.Add(commissionHistory);
        //        AddCommissionPerMonth(user, month, year, commissionHistory.AmountFees, FeeType.IndirectFees);
        //        return commissionHistory.AmountFees;
        //    }
        //    return 0;
        //}
        #endregion

        #region Delta
        //-----------------------------------------------------------
        /// <summary>
        /// متد اصلی پلن دلتا
        /// </summary>
        /// <param name="geneologyPlan"></param>
        /// <param name="userGeneology"></param>
        /// <param name="Amount"></param>
        public long DeltaGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount,int DeltaPlanId=0)
        {
            var PlanDelta = _unitOfWork.PlanDeltaGR.FirstOrDefault(p => p.GeneologyPlanId == geneologyPlan.Id);

            //اگر دلتا تو در تو باشه
            if(DeltaPlanId> 0)
            {
                PlanDelta = _unitOfWork.PlanDeltaGR.FirstOrDefault(p => p.Id == DeltaPlanId);
            }
            var amount =/* (geneologyPlan.Percent / 100) **/ Amount;

            var user = FindUserWithLevel(PlanDelta.LevelDelta, userGeneology.UserId, userGeneology.GeneologyTypeId);
            if (user != null)
            {


                amount = PercentCalc(amount, PlanDelta.PercentDelta);
                var month = DateTime.Now.MiladiToJalaliMonth();
                var year = DateTime.Now.MiladiToJalaliYear();
                var commissionPerMonth = AddCommissionPerMonth(user, month, year, 0, FeeType.IndirectFees);
                var commissionHistory = new CommissionHistory
                {
                    UserGeneologyId = user.Id,
                    Year = year,
                    Month = month,
                    AmountFees = amount,
                    FeeType = FeeType.IndirectFees,
                    CommissionPerMonthId = commissionPerMonth.Id,
                    CDate = DateTime.Now,
                    PlanEnum=PlanEnum.Delta,
                    DividendAmountHistoryId = null
                };

                //commissionHistory.AmountFees -= DeltaGeneral(geneologyPlan, user, amount, DividendAmountHistoryId);

                _unitOfWork.CommissionHistoryGR.Add(commissionHistory);
                AddCommissionPerMonth(user, month, year, commissionHistory.AmountFees, FeeType.IndirectFees);
                //اگر به خود دلتا یه دلتا دیگه چسبیده بود
                if (_unitOfWork.PlanDeltaGR.Any(p => p.PlanDeltaId == PlanDelta.Id))
                {
                    var planInSide = _unitOfWork.PlanDeltaGR.FirstOrDefault(p => p.PlanDeltaId == PlanDelta.Id);
                    DeltaGeneral(geneologyPlan, user, commissionHistory.AmountFees, planInSide.Id);
                }

                //بازگشت و ارسال مقدار جاری برای محاسبه مرحله بعدی
                DeltaGeneral(geneologyPlan, user, commissionHistory.AmountFees);
                
                return amount;
            }
            return 0;
        }
        #endregion

        #region Binary
        //-----------------------------------------------------------

        public void BinaryGeneral(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long Amount, int DividendAmountHistoryId)
        {
            var PlanBinary = _unitOfWork.PlanBinaryGR.FirstOrDefault(p => p.Id == geneologyPlan.PlanBinaryId.Value);

            var amount = Convert.ToInt64((geneologyPlan.Percent / 100.0)) * Amount;
            var month = DateTime.Now.MiladiToJalaliMonth();
            var year = DateTime.Now.MiladiToJalaliYear();
            var binaryCondition = BinaryCondition(geneologyPlan, userGeneology, PlanBinary.AmountBalanceBinary, year, month);

            //کاربر پدر رو در همون جنولوژی پیدا میکنه
            var ParentUser = _unitOfWork.UserGeneologyGR.GetSingleIncluding(p => p.UserId == userGeneology.ParentId, a => a.Geneologytype);
            if(ParentUser != null)
            {
                //تاریخچه کارمزد پدر رو میاره
                var commisionHistory = _unitOfWork.CommissionHistoryGR.FindBy(p => p.UserGeneologyId == ParentUser.Id && p.Year == year && p.Month == month && p.FeeType == FeeType.IndirectFees && p.PlanEnum == PlanEnum.Binary).ToList();

                //اگر تمام شرایط موجود در پلن باینر درست باشد و تعادل برقرار باشد
                if (binaryCondition.Count(p => p.IsEqualAmountBalance == true) >= PlanBinary.NumberOfSets &&
                    commisionHistory.Count() < PlanBinary.FrequencyOfPayments)
                {

                    //اگر چند بار بشه برای شخص پرداخت انجام داد میره تو اگر مثلا هردست 1100 فروش باشه مقدار تعادل باشه 500 میشه دوبار
                    if (binaryCondition.Where(p => p.IsEqualAmountBalance == true).GroupBy(p => p.BalanceCount).Count() == 1)
                    {

                        //نمیزاره بیشتر از تعداد مشخص در پلن پرداخت بشه
                        //ینی اگر قبلا 2 بار پرداخت شده بود و الآن هم قراره 4 بار پرداخت شه و تعداد مشخص شده دپلن 3 بار باشه پس باید 1 بار دیگه پرداخت بشه
                        var ForCount = binaryCondition.FirstOrDefault(p => p.IsEqualAmountBalance == true).BalanceCount;
                        if ((commisionHistory.Count() + binaryCondition.FirstOrDefault(p => p.IsEqualAmountBalance == true).BalanceCount) > PlanBinary.FrequencyOfPayments)
                            ForCount = binaryCondition.FirstOrDefault(p => p.IsEqualAmountBalance == true).BalanceCount - ((commisionHistory.Count() + binaryCondition.FirstOrDefault(p => p.IsEqualAmountBalance == true).BalanceCount) - PlanBinary.FrequencyOfPayments);

                        //حجم فروش رو از دست چپ و راست کم میکنیم
                        foreach (var item in binaryCondition.Where(p => p.IsEqualAmountBalance == true))
                        {
                            var referalUser = _unitOfWork.UserGeneologyGR.GetSingleIncluding(p => p.Id == item.UserGeneologyId, a => a.Geneologytype);
                            var SellPerMonth = _unitOfWork.SellPerMonthGR.FirstOrDefault(p => p.UserId == referalUser.UserId && p.SystemType == referalUser.Geneologytype.SystemType && p.Year == year && p.Month == month);
                            SellPerMonth.BinarySalesVolume -= PlanBinary.AmountBalanceBinary * binaryCondition.FirstOrDefault(p => p.IsEqualAmountBalance == true).BalanceCount;
                            _unitOfWork.SellPerMonthGR.Update(SellPerMonth);
                        }

                        for (int i = 0; i < ForCount; i++)
                        {
                            

                            //اگر فلش نخورده بود  کمیسیون رو ذخیره میکنه
                            if (commisionHistory.Sum(p => p.AmountFees) <= PlanBinary.FlashAmount)
                            {


                                var commissionPerMonth = AddCommissionPerMonth(ParentUser, month, year, 0, FeeType.IndirectFees);
                                var commissionHistory = new CommissionHistory
                                {
                                    UserGeneologyId = ParentUser.Id,
                                    Year = year,
                                    Month = month,
                                    AmountFees = PlanBinary.AmountWageBinary,
                                    FeeType = FeeType.IndirectFees,
                                    CommissionPerMonthId = commissionPerMonth.Id,
                                    CDate = DateTime.Now,
                                    PlanEnum=PlanEnum.Binary,
                                    DividendAmountHistoryId = DividendAmountHistoryId
                                };
                                _unitOfWork.CommissionHistoryGR.Add(commissionHistory);
                                AddCommissionPerMonth(ParentUser, month, year, commissionHistory.AmountFees, FeeType.IndirectFees);

                                //اگر دلتا چسبیده باشه به پلن اون هم محاسبه میشه
                                if (_unitOfWork.PlanDeltaGR.Any(p => p.GeneologyPlanId == geneologyPlan.Id))
                                {
                                    DeltaGeneral(geneologyPlan, userGeneology, commissionHistory.AmountFees);
                                }
                            }
                        }

                    }
                 

                }

            }

            //تعادل رو تا بالاترین سطح  جنولوژی چک میکنه
            var UserGeneologyParentcheckBinary = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.UserId == userGeneology.ParentId);
            if (UserGeneologyParentcheckBinary != null)
                BinaryGeneral(geneologyPlan, UserGeneologyParentcheckBinary, Amount, DividendAmountHistoryId);
        }

        public List<BinaryViewModel> BinaryCondition(GeneologyPlan geneologyPlan, UserGeneology userGeneology, long AmountBalanceBinary, string year, string month)
        {
            //دست هارو پیدا میکنه
            var LevelOneUsers = _unitOfWork.UserGeneologyGR.FindBy(p => p.ParentId == userGeneology.ParentId && p.GeneologyTypeId == userGeneology.GeneologyTypeId).ToList();

            // اینجا میخواد Geneologytype رو اینکلود کنه
            userGeneology = _unitOfWork.UserGeneologyGR.GetSingleIncluding(p => p.Id == userGeneology.Id, p => p.Geneologytype);

            // اینجا میخواد PlanBinary رو اینکلود کنه
            geneologyPlan = _unitOfWork.GeneologyPlanGR.GetSingleIncluding(p => p.Id == geneologyPlan.Id, p => p.PlanBinary);


            var BinaryResult = new List<BinaryViewModel>();
            foreach (var item in LevelOneUsers)
            {
                var BinaryClass = new BinaryViewModel();
                BinaryClass.BalanceCount = 0;
                var sellPerMonth = _unitOfWork.SellPerMonthGR.FirstOrDefault(p => p.UserId == item.UserId && p.Month == month && p.Year == year && p.SystemType == userGeneology.Geneologytype.SystemType);
                if (sellPerMonth != null)
                {
                    BinaryClass.UserGeneologyId = item.Id;
                    //if (geneologyPlan.PlanBinary.CalcMethod == CalcMethod.Self)
                    //    BinaryClass.SellCollect = sellPerMonth.SellYourself;
                    //else if (geneologyPlan.PlanBinary.CalcMethod == CalcMethod.SelfAndCollection)
                    BinaryClass.SellCollect = sellPerMonth.BinarySalesVolume;

                    if (BinaryClass.SellCollect >= AmountBalanceBinary)
                    {
                        BinaryClass.BalanceCount = Convert.ToInt32(BinaryClass.SellCollect / AmountBalanceBinary);
                        BinaryClass.IsEqualAmountBalance = true;
                        BinaryClass.ModAmount = BinaryClass.SellCollect - AmountBalanceBinary;
                    }
                    else
                    {
                        BinaryClass.IsEqualAmountBalance = false;
                        BinaryClass.ModAmount = BinaryClass.SellCollect;
                    }
                    BinaryResult.Add(BinaryClass);
                }
            }

            return BinaryResult;

        }
        #endregion

        #region Breakaway
        public void BreakGeneral(GeneologyPlan geneologyPlan)
        {
            var PlanBreak = _unitOfWork.PlanBreakAWayGR.FirstOrDefault(p => p.Id == geneologyPlan.PlanBreakAWayId.Value);
            var PlanBreakLevels = _unitOfWork.PlanBreakAWayLevelGR.FindBy(p => p.PlanBreakAWayId == geneologyPlan.PlanBreakAWayId.Value).ToList();
            var users = _unitOfWork.UserGeneologyGR.FindBy(p => p.GeneologyTypeId == geneologyPlan.GeneologyTypeId).ToList();
            var month = DateTime.Now.MiladiToJalaliMonth();
            var year = DateTime.Now.MiladiToJalaliYear();
            var BraekResult = new List<BraekViewModel>();

            foreach (var item in users)
            {
                var sellPerMonth = _unitOfWork.SellPerMonthGR.FirstOrDefault(p => p.UserId == item.UserId && p.Month == month && p.Year == year);
                var selfAndCollectSell = (sellPerMonth.SellYourself + sellPerMonth.SellSets);
                var PlanBreakLevelItem = PlanBreakLevels.OrderByDescending(p => p.AmountBreakAWay).FirstOrDefault(p => p.AmountBreakAWay <= selfAndCollectSell && p.CalcMethod == CalcMethod.SelfAndCollection);
                int UserPercent = 0;
                if (PlanBreakLevelItem != null)
                {
                    UserPercent = PlanBreakLevelItem.PercentBreakAWay;
                }
                else
                {
                    PlanBreakLevelItem = PlanBreakLevels.OrderByDescending(p => p.AmountBreakAWay).FirstOrDefault(p => p.AmountBreakAWay <= sellPerMonth.SellYourself && p.CalcMethod == CalcMethod.Self);
                    if (PlanBreakLevelItem != null)
                    {
                        UserPercent = PlanBreakLevelItem.PercentBreakAWay;
                    }
                }
                if (PlanBreakLevelItem != null)
                {
                    var BreakPrcent = PlanBreakLevels.OrderByDescending(p => p.PercentBreakAWay).FirstOrDefault().PercentBreakAWay;
                    bool isbreak = false;
                    if (PlanBreakLevelItem.PercentBreakAWay == BreakPrcent)
                    {
                        isbreak = true;
                    }
                    var parentUser = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.UserId == item.ParentId);
                    var BraekClass = new BraekViewModel();
                    if (parentUser != null)
                    {
                        BraekClass = new BraekViewModel
                        {
                            Break = isbreak,
                            UserPercent = UserPercent,
                            UserGeneologyId = item.Id,
                            UserGeneologyParentId = parentUser.Id,
                            TotalSellCollect = selfAndCollectSell
                        };
                    }
                    else
                    {
                        BraekClass = new BraekViewModel
                        {
                            Break = isbreak,
                            UserPercent = UserPercent,
                            UserGeneologyId = item.Id,
                            UserGeneologyParentId = null,
                            TotalSellCollect = selfAndCollectSell
                        };
                    }

                    BraekResult.Add(BraekClass);
                }
            }

            foreach (var userItem in BraekResult)
            {
                foreach (var childUserItem in BraekResult.Where(b => b.UserGeneologyParentId == userItem.UserGeneologyId && b.Break == false).ToList())
                {
                    int PercentageDifference = userItem.UserPercent - childUserItem.UserPercent;
                    if (PercentageDifference > 0)
                    {
                        var Wage = PercentCalc(childUserItem.TotalSellCollect, PercentageDifference);
                        var userGeneology = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.Id == userItem.UserGeneologyId);
                        var commissionPerMonth = AddCommissionPerMonth(userGeneology, month, year, 0, FeeType.IndirectFees);
                        var commissionHistory = new CommissionHistory
                        {
                            UserGeneologyId = userGeneology.Id,
                            Year = year,
                            Month = month,
                            AmountFees = Wage,
                            FeeType = FeeType.IndirectFees,
                            CommissionPerMonthId = commissionPerMonth.Id,
                            CDate = DateTime.Now,
                            PlanEnum=PlanEnum.BreakAWay,
                            DividendAmountHistoryId = null
                        };
                        _unitOfWork.CommissionHistoryGR.Add(commissionHistory);
                        AddCommissionPerMonth(userGeneology, month, year, commissionHistory.AmountFees, FeeType.IndirectFees);
                        if (_unitOfWork.PlanDeltaGR.Any(p => p.GeneologyPlanId == geneologyPlan.Id))
                        {
                            DeltaGeneral(geneologyPlan, userGeneology, commissionHistory.AmountFees);
                        }
                    }
                }

            }
        }
        #endregion

        #region Used Methods For Plans
        public void SaveSellUser(string UserId, SystemType SystemType, long Amount, bool isSelfSell = true)
        {
            var user = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.UserId == UserId && p.Geneologytype.SystemType == SystemType);
            var month = DateTime.Now.MiladiToJalaliMonth();
            var year = DateTime.Now.MiladiToJalaliYear();
            if (user != null)
            {
                long BinarySalesVolume = 0;
                long BreakSalesVolume = 0;
                if (_unitOfWork.SellPerMonthGR.Any(p => p.UserId == UserId && p.Month != month && p.Year != year && p.SystemType == SystemType && (p.BinarySalesVolume > 0 || p.BreakSalesVolume > 0)))
                {
                    var SellPerOldMonth = _unitOfWork.SellPerMonthGR.FindBy(p => p.UserId == UserId && p.Month != month && p.Year != year && p.SystemType == SystemType && (p.BinarySalesVolume > 0 || p.BreakSalesVolume > 0)).ToList();

                    foreach (var item2 in SellPerOldMonth)
                    {
                        BinarySalesVolume += item2.BinarySalesVolume;
                        BreakSalesVolume += item2.BreakSalesVolume;
                        item2.BinarySalesVolume = 0;
                        item2.BreakSalesVolume = 0;
                        _unitOfWork.SellPerMonthGR.Update(item2);
                    }

                }
                if (isSelfSell)
                {
                    if (_unitOfWork.SellPerMonthGR.Any(p => p.UserId == UserId && p.Month == month && p.Year == year && p.SystemType == SystemType))
                    {
                        var sellPerMonth = _unitOfWork.SellPerMonthGR.FirstOrDefault(p => p.UserId == UserId && p.Month == month && p.Year == year && p.SystemType == SystemType);
                        sellPerMonth.SellYourself += Amount;
                        sellPerMonth.BinarySalesVolume += Amount + BinarySalesVolume;
                        sellPerMonth.BreakSalesVolume += Amount + BreakSalesVolume;
                        _unitOfWork.SellPerMonthGR.Update(sellPerMonth);
                    }
                    else
                    {
                        var sellPerMonth = new SellPerMonth
                        {
                            CDate = DateTime.Now,
                            Month = month,
                            Year = year,
                            SellSets = 0,
                            BinarySalesVolume = Amount + BinarySalesVolume,
                            BreakSalesVolume = Amount + BreakSalesVolume,
                            UserId = UserId,
                            SellYourself = Amount,
                            SystemType = SystemType
                        };
                        _unitOfWork.SellPerMonthGR.Add(sellPerMonth);
                    }
                }
                else
                {
                    if (_unitOfWork.SellPerMonthGR.Any(p => p.UserId == UserId && p.Month == month && p.Year == year && p.SystemType == SystemType))
                    {
                        var sellPerMonth = _unitOfWork.SellPerMonthGR.FirstOrDefault(p => p.UserId == UserId && p.Month == month && p.Year == year && p.SystemType == SystemType);
                        sellPerMonth.SellSets += Amount;
                        sellPerMonth.BinarySalesVolume += Amount + BinarySalesVolume;
                        sellPerMonth.BreakSalesVolume += Amount + BreakSalesVolume;
                        _unitOfWork.SellPerMonthGR.Update(sellPerMonth);
                    }
                    else
                    {
                        var sellPerMonth = new SellPerMonth
                        {
                            CDate = DateTime.Now,
                            Month = month,
                            Year = year,
                            SellSets = Amount,
                            BinarySalesVolume = Amount + BinarySalesVolume,
                            BreakSalesVolume = Amount + BreakSalesVolume,
                            UserId = UserId,
                            SellYourself = 0,
                            SystemType = SystemType

                        };
                        _unitOfWork.SellPerMonthGR.Add(sellPerMonth);
                    }
                }
                foreach (var item in _unitOfWork.UserGeneologyGR.FindBy(p => p.UserId == UserId && p.Geneologytype.SystemType == SystemType).ToList())
                {
                    if (!string.IsNullOrEmpty(item.ParentId))
                        SaveSellUser(item.ParentId, SystemType, Amount, false);
                }

            }
        }
        public CommissionPerMonth AddCommissionPerMonth(UserGeneology userGeneology, string Month, string Year, long Amount, FeeType feeType)
        {
            var CommissionPerMonth = new CommissionPerMonth
            {
                CDate = DateTime.Now,
                UDate = DateTime.Now,
                FeeType = feeType,
                AmountFees = Amount,
                Month = Month,
                Year = Year,
                UserGeneologyId = userGeneology.Id
            };
            if (_unitOfWork.CommissionPerMonthGR.Any(p => p.UserGeneologyId == userGeneology.Id && p.Month == Month && p.Year == Year && p.FeeType == feeType))
            {
                CommissionPerMonth = _unitOfWork.CommissionPerMonthGR.FirstOrDefault(p => p.UserGeneologyId == userGeneology.Id && p.Month == Month && p.Year == Year && p.FeeType == feeType);
                CommissionPerMonth.AmountFees += Amount;
                CommissionPerMonth.UDate = DateTime.Now;
                _unitOfWork.CommissionPerMonthGR.Update(CommissionPerMonth);
                return CommissionPerMonth;

            }
            _unitOfWork.CommissionPerMonthGR.Add(CommissionPerMonth);

            return CommissionPerMonth;
        }
        //-----------------------------------------------------------
        public DividendAmountHistory AddDividendAmountHistory(string UserId, string Month, string Year, long Amount)
        {
            var DividendAmountHistory = new DividendAmountHistory
            {
                CDate = DateTime.Now,
                OriginalAmount = Amount,
                Month = Month,
                Year = Year,
                UserId = UserId
            };

            _unitOfWork.DividendAmountHistoryGR.Add(DividendAmountHistory);

            return DividendAmountHistory;
        }

        /// <summary>
        /// پیدا کردن کاربر در سطح مشخص، از روی کاربر ورودی 
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="UserId"></param>
        /// <param name="GeneologyTypeId"></param>
        /// <returns>UserGeneology</returns>
        /// 
        public UserGeneology FindUserWithLevel(int Level, string UserId, int GeneologyTypeId)
        {
            var UserGeneology = _unitOfWork.UserGeneologyGR.FindBy(p => p.GeneologyTypeId == GeneologyTypeId && p.UserId == UserId).FirstOrDefault();
            if (Level == 0)
                return UserGeneology;

            for (int i = 0; i < Level; i++)
            {
                UserGeneology = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.GeneologyTypeId == GeneologyTypeId && p.UserId == UserGeneology.ParentId);
                if (UserGeneology == null)
                    return null;
            }

            return UserGeneology;

        }

        //-----------------------------------------------------------
        /// <summary>
        /// متد محاسبه درصد
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="Percent"></param>
        /// <returns>long</returns>
        public long PercentCalc(long Amount, int Percent)
        {
            return Convert.ToInt64((Percent / 100.0) * Amount);
        }
        #endregion


    }
}