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
            var otpModel = new OtpModel { UserId = 1234, Date = DateTime.Now }; 

            //Act
            var password = otpRepository.Create(otpModel);
            bool isValid = otpRepository.VerifyOtp(password, DateTime.Now.AddSeconds(20));

            //Assert
            Assert.True(isValid);
        }

        [Fact]
        public void GeneratedOtpMustExpiredAfter30sec()
        {
            var configuration = GetConfiguration();
            var otpRepository = new OtpRepository(configuration);
            var otpModel = new OtpModel { UserId = 1234, Date = DateTime.Now };  
            
            var password = otpRepository.Create(otpModel);
            bool isValid = otpRepository.VerifyOtp(password, DateTime.Now.AddSeconds(31));

            Assert.False(isValid);
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
