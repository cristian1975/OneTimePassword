using Microsoft.Extensions.Configuration;
using OneTimePassword.Data;
using OneTimePassword.Models;
using System;
using System.IO;
using Xunit;

namespace OneTimePassword.Test
{
    public class OneTimePasswordShould
    {
        [Fact]
        public void GeneratedOtpMustBeValidFor30sec()
        {
            //Arrange
            var configuration = GetConfiguration();
            var otpRepository = new OtpRepository(configuration);
            var otpModel = new OtpModel { UserId = 1234, Date = DateTime.UtcNow ,Otp ="" }; 

            //Act
            var password = otpRepository.Create(otpModel);
            System.Threading.Thread.Sleep(2000);
            bool isValid = otpRepository.VerifyOtp(password);

            //Assert
            Assert.True(isValid);
        }

        [Fact]
        public void GeneratedOtpMustExpiredAfter30sec()
        {
            var configuration = GetConfiguration();
            var otpRepository = new OtpRepository(configuration);
            var otpModel = new OtpModel { UserId = 1234, Date = DateTime.UtcNow.AddSeconds(31) };  
            
            var password = otpRepository.Create(otpModel);
            bool isValid = otpRepository.VerifyOtp(password);

            Assert.False(isValid);
        }

        [Fact]
        public void EachUserMustHaveDifferentPasswordAfter30sec()
        {
            var configuration = GetConfiguration();
            var otpRepository1 = new OtpRepository(configuration);
            var otpRepository2 = new OtpRepository(configuration);

            var otpModel1 = new OtpModel { UserId = 12345, Date = DateTime.UtcNow };
            var password1 = otpRepository1.Create(otpModel1);
            otpModel1.Otp = password1;

            var otpModel2 = new OtpModel { UserId = 12346, Date = DateTime.UtcNow.AddSeconds(31) };
            var password2 = otpRepository2.Create(otpModel2);
            otpModel2.Otp = password2;

            Assert.NotEqual(otpModel1.Otp, otpModel2.Otp);
        }
        public IConfiguration GetConfiguration()
        {

            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
