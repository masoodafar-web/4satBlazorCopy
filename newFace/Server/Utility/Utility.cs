using newFace.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace newFace.Server.Utility
{
    public class MemoryPostedFile : IFormFile
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public int ContentLength => fileBytes.Length;

        public string FileName { get; }

        public Stream InputStream { get; }

        public string ContentDisposition => throw new NotImplementedException();

        public string ContentType => throw new NotImplementedException();

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public long Length => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }
    public class utility
    {
        private UserManager<ApplicationUser> UserManager;
        HttpContext HttpContext=new DefaultHttpContext();
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
        public ApplicationUser loginUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var user = UserManager.Users.Include(p => p.UserCategorys.Select(i=>i.Category)).FirstOrDefault(p => p.Id == userId);
            return user;
        }


    }
}