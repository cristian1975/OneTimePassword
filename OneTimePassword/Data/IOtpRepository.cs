using OneTimePassword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneTimePassword.Data
{
    public interface IOtpRepository
    {
        string Create(OtpModel otp);
        bool VerifyOtp(string otp, DateTime date);
    }
}
