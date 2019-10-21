using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace TestTask.Services
{
    public static class Hmac
    {
       
          
            public static byte[] ComputeHMAC_SHA256(byte[] data)
            {

             using (SHA384 shaM = new SHA384Managed())
                {
                    return shaM.ComputeHash(data);
                }

            }
        

    }
}