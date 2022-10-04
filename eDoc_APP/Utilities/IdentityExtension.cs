using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace eDoc_APP.Utilities
{
    public static class IdentityExtension
    {
        public static string GetNameIdentifier(this IPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                return userIdClaim?.Value;
            }
            return null;
        }
        /// <summary>
        /// Hàm lấy Claim đã Add khi Login dựa vào key
        /// </summary>
        /// <param name="User"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetClaimByType(this IPrincipal User, string type)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == type);

                return userIdClaim?.Value;
            }
            return null;
        }
        /// <summary>
        /// Hàm kiểm tra file docx
        /// </summary>
        /// <param name="User"></param>
        /// <param name="type"></param>
        /// <returns>true nếu tất cả file đều là .docx</returns>
        public static bool CheckIsDocxFileOrEmptyFile(this HttpFileCollectionBase files)
        {
            if(files.Count==0) return false;
            foreach (HttpPostedFileBase file in files)
            {
                if (Path.GetExtension(file.FileName) != ".docx")
                {
                    return false;
                }
            }
            return true;
        }
    }
}