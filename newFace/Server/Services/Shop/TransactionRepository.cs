using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Shop;
using Microsoft.IdentityModel.Tokens;

namespace newFace.Server.Services.Shop
{
    public class TransactionRepository : ITransactionRepository
    {
        private IUnitOfWork _unitOfWork;

        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResultTransaction GetBillById(int? id)
        {
            ResultTransaction result = new ResultTransaction();

            if (id == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه صورتحساب را وارد نمایید";
                return result;
            }

            var bill =_unitOfWork.BillGR.GetById(id.Value);
            if (bill == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "صورتحساب یافت نشد";
                return result;
            }

            result.Bill = bill;
            result.Statue = Enums.Statue.Success;
            result.Message = "باموفقیت ارسال شد";
            return result;
        }


        public ResultTransaction ShowBill(int id, string userid)
        {
            ResultTransaction result = new ResultTransaction();

            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا وارد شوید";
                return result;
            }

            var bill =_unitOfWork.BillGR.FindBy(f => f.Id == id && f.UserId == userid).FirstOrDefault();
            if (bill != null)
            {
                result.Bill = bill;
                result.Statue = Enums.Statue.Success;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "صورتحسابی یافت نشد!";
            }


            return result;
        }

        //public int BillCount(string userid)
        //{
        //    var billcount =_unitOfWork.BillGR.Count(c => c.UserId == userid);

        //    return billcount;
        //}

        public ResultTransaction GetAll(string userid)
        {
            ResultTransaction result = new ResultTransaction();

            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا وارد شوید";
                return result;
            }

            var billscollection =_unitOfWork.BillGR.FindBy(w => w.UserId == userid).ToList();

            List<BillViewModel> bills = new List<BillViewModel>();
            var count = 1;
            foreach (var item in billscollection)
            {
                BillViewModel bill = new BillViewModel();
                bill.Id = item.Id;
                bill.Row = count;
                bill.Date = item.CDate.MiladiToJalali();
                bill.Status = item.Status == 0 ? "پرداخت ناموفق" : "پرداخت موفق";
                bill.TotalDiscount = Math.Round(item.TotalDiscount);
                bill.TotalPrice = Math.Round(item.TotalUnitPrice);
                bill.TotalPayment = Math.Round(item.TotalPrice);
                bill.PaymentStatus = item.Status;
                bills.Add(bill);

                count++;
            }

            result.BillViewModels.AddRange(bills);
            result.Statue = Enums.Statue.Success;
            return result;
        }
    }
}