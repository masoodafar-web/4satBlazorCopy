using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Shop;

using Microsoft.AspNetCore.Identity;
using ZarinpalSandbox;


namespace newFace.Server.Services.Shop
{
    public class PaymentRepository : IPaymentRepository
    {
        private UserManager<ApplicationUser> UserManager;
        public PaymentRepository(UserManager<ApplicationUser> userManager)
        {
            UserManager= userManager;
        }

        public ResultPayment PaymentVerification(string apicode, string Authority, int TotalPayment)
        {
            ResultPayment result = new ResultPayment();
            var payment = new Payment(TotalPayment);
            var res=payment.Verification(Authority);
            if (res.Result.Status==100)
            {
                result.RefrenceCode = res.Result.RefId.ToString();
                result.Statue = Enums.Statue.Success;
                result.Message = "پرداخت تایید شد";
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "پرداخت تایید نشد";
                return result;
            }
        
        }

        public ResultPayment RequestPayment(string apiCode, string userId, double TotalPayment, string Description, int factorId, string WebsiteUrl, string CallbackUrl, Enums.ReturnFrom returnfrom)
        {
            ResultPayment result = new ResultPayment();
            if (TotalPayment > 999999999)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "مبلغ پرداختی از حدمجاز بالاتر میباشد";
                return result;
            }
            var user = UserManager.FindByIdAsync(userId).Result;
            var payment = new Payment((int)TotalPayment);
            var res = payment.PaymentRequest(Description, WebsiteUrl + "/" + CallbackUrl + "?FactorId=" + factorId + "&TotalPrice=" + TotalPayment + "&returnfrom=" + (int)returnfrom, user.Email, user.PhoneNumber);
            if (res.Result.Status == 100)
            {
                result.UrlReturn = "https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority;
                result.Statue = Enums.Statue.Success;
                result.Message = "درانتظار اتصال به درگاه پرداخت";
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "خطای اتصال به درگاه پرداخت.لطفا دوباره تلاش کنید";
                return result;
            }
        }

        //public ResultPayment RequestPayment(string apiCode, string userId, double TotalPayment , string Description, int factorId, string WebsiteUrl ,  string CallbackUrl , Enums.ReturnFrom returnfrom)
        //{

        //    ResultPayment result = new ResultPayment();

        //    if (TotalPayment > 999999999)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "مبلغ پرداختی از حدمجاز بالاتر میباشد";
        //        return result;
        //    }

        //    var user =UserManager.FindByIdAsync(userId).Result;


        //    System.Net.ServicePointManager.Expect100Continue = false;

        //    zarinpal.ServiceReference.PaymentGatewayImplementationServicePortTypeClient Zarin = new PaymentGatewayImplementationServicePortTypeClient();
        //    int price = Convert.ToInt32(TotalPayment);
        //    string Auth;
        //    int status = Zarin.PaymentRequest(apiCode, price, Description, user.Email, user.PhoneNumber, WebsiteUrl+"/"+CallbackUrl+"?FactorId="+factorId + "&TotalPrice=" + TotalPayment + "&returnfrom=" + returnfrom, out Auth);

        //    if (status == 100)
        //    {
        //        result.UrlReturn = "https://www.zarinpal.com/pg/StartPay/" + Auth;
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "درانتظار اتصال به درگاه پرداخت";
        //        return result;
        //    }
        //    else
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "خطای اتصال به درگاه پرداخت.لطفا دوباره تلاش کنید";
        //        return result;
        //    }

        //}

        //public ResultPayment PaymentVerification(string apicode, string Authority, string TotalPayment)
        //{
        //    ResultPayment result = new ResultPayment();

        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    long Refid = 0;
        //    double price =Math.Round(Convert.ToDouble(TotalPayment));
        //    zarinpal.ServiceReference.PaymentGatewayImplementationServicePortTypeClient Zarin = new PaymentGatewayImplementationServicePortTypeClient();
        //    var PaymentStatus = Zarin.PaymentVerification(apicode, Authority, (int)price, out Refid);
        //    if (PaymentStatus == 100)
        //    {
        //        result.RefrenceCode = Refid.ToString();
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "پرداخت تایید شد";
        //        return result;
        //    }
        //    else
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "پرداخت تایید نشد";
        //        return result;
        //    }
        //}
    }
}