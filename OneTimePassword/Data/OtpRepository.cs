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
        
        public OtpRepository(IConfiguration config)
        {
            _config = config;
            secretKey = Base32Encoding.ToBytes(_config.GetValue<string>("OtpKey"));
            correction = new TimeCorrection(DateTime.Now);
        }
        public string Create(OtpModel otp)
        {
            
            var totp = new Totp(secretKey, step:30, timeCorrection:correction);          
            string password = totp.ComputeTotp();
            otp.Otp = password;
            //TODO save in db otp model
            return password;         
        }

        public bool VerifyOtp(string otp, DateTime date)
        {
            var correction = new TimeCorrection(date);
            var totp = new Totp(secretKey, timeCorrection: correction);
            return totp.VerifyTotp(otp, out long timeWindowUsed);          
        }
    }
}
