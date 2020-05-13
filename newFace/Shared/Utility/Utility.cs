
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace newFace.Shared
{
    //public class MemoryPostedFile : HttpPostedFileBase
    //{
    //    private readonly byte[] fileBytes;

    //    public MemoryPostedFile(byte[] fileBytes, string fileName = null)
    //    {
    //        this.fileBytes = fileBytes;
    //        this.FileName = fileName;
    //        this.InputStream = new MemoryStream(fileBytes);
    //    }

    //    public override int ContentLength => fileBytes.Length;

    //    public override string FileName { get; }

    //    public override Stream InputStream { get; }
    //}
    public class utility
    {
        
        public string StringGenerator(int lenght, bool UseUppercaseCharacters, bool UseLowerCaseCharacters, bool UseNumbers, bool UseSpecialCharacters)
        {
            string result = string.Empty;

            string allowedChars = string.Empty;
            string temp = "";
            //string result = "";
            if (UseLowerCaseCharacters)
            {
                allowedChars += "a,b,c,d,e,g,h,i,j,k,l,m,n,o,p,q,r,t,u,v,w,y,z,";
            }

            if (UseUppercaseCharacters)
            {
                allowedChars += "A,B,C,D,E,G,H,I,J,K,L,M,N,O,P,Q,R,T,U,V,W,Y,Z,";

            }
            if (UseNumbers)
            {
                allowedChars += "1,2,3,4,5,6,7,8,9,0";
            }

            if (UseSpecialCharacters)
            {
                allowedChars += "!,@,#,$,%,^,&,*";
            }
            char[] sep = { ',' };

            string[] arr = allowedChars.Split(sep);
            Random rand = new Random();
            for (int i = 0; i < lenght; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                result += temp;
            }


            return result;
        }
        //public ApplicationUser loginUser(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        userId = HttpContext.Current.User.Identity.GetUserId();
        //    }
        //    var user = UserManager.Users.Include(p => p.UserCategorys.Select(i=>i.Category)).FirstOrDefault(p => p.Id == userId);
        //    return user;
        //}


    }
}