using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTimePassword.Data;
using OneTimePassword.Models;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneTimePassword.Controllers
{
    public class OneTimePasswordController : Controller
    {
        private readonly IOtpRepository _otpRepository;
        public OneTimePasswordController(IOtpRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }
        // GET: OneTimePasswordController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OneTimePasswordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OneTimePasswordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtpModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  var password = _otpRepository.Create(model);
                  TempData["password"] = password;
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      
    }
}
