using System.Web;
using Microsoft.AspNetCore.Http;

namespace newFace.Server
{
    public static class GlobalParametrs
    {
        //----------------------------------------------------------------------
        #region Url

        //public static string GetUrl { get; set; } = "http://4100sat.com/";
        //public static string GetUrl { get; set; } = "https://4satt.com/";
        public static string GetUrl { get; set; } = "https://localhost:44318";

        #endregion
        //----------------------------------------------------------------------
        #region PostChangeRequestCount

        public static int GetLevelCount { get; set; } = 10;
        public static int GetIsExistCount { get; set; } = 10;
        public static int GetCategoryIdCount { get; set; } = 10;

        #endregion
        //----------------------------------------------------------------------

        #region CommissionPercent

        public static int GetCommissionPercent { get; set; } = 10;

        #endregion
        //----------------------------------------------------------------------
        //#region Owin
        //static ApplicationUserManager _userManager;
        //static ApplicationSignInManager _signInManager;

        //public static ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        //public static ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();

        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //#endregion

        //----------------------------------------------------------------------
        #region ZarinpalApiCode

        public static string Zarin { get; set; } = "609af8e2-e512-11e9-be0d-000c295eb8fc";

        #endregion
        //----------------------------------------------------------------------
    }

}