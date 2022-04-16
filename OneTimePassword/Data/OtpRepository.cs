using Microsoft.Extensions.Configuration;
using OneTimePassword.Models;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneTimePassword.Data
{
    public class OtpRepository : IOtpRepository
    {
        private readonly IConfiguration _config;
        private readonly byte[] secretKey; 
        public readonly TimeCorrection correction;
        public readonly Totp totp;

        public OtpRepository(IConfiguration config)
        {
            _config = config;
            secretKey = Base32Encoding.ToBytes(_config.GetValue<string>("OtpKey"));
            correction = new TimeCorrection(DateTime.Now);
            totp = new Totp(secretKey, timeCorrection: correction);
        }
        public string Create(OtpModel otp)
        {
            
                    
            string password = totp.ComputeTotp(otp.Date);
            otp.Otp = password;
            //TODO save in db otp model
            return password;         
        }

        public bool VerifyOtp(string otp)
        {
            return totp.VerifyTotp(otp, out long timeWindowUsed);          
        }
    }
}
