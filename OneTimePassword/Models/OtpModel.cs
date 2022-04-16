using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneTimePassword.Models
{
    public class OtpModel
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        public string Otp { get; set; }
    }
}
