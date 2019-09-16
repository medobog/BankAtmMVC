using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private BankDbContext _context;

        public HomeController(BankDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                GenerateKey generateKey = new GenerateKey();
                User newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    UserKey = generateKey.RandKey()
                };
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.Password = hasher.HashPassword(newUser, newUser.Password);

                _context.Users.Add(newUser);
                _context.SaveChanges();


                Account newAccount = new Account
                {
                    Balance = 0,
                    UserID = newUser.UserID,
                    UserKey = newUser.UserKey
                };

                _context.Accounts.Add(newAccount);
                _context.SaveChanges();

                HttpContext.Session.SetString("CurrentUserKey", newUser.UserKey);

                return RedirectToAction("Account", "Bank", new { UserKey = newUser.UserKey });
            }
            return View("Index");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("loggingIn")]
        public IActionResult LoggingIn(string loginemail, string loginpw)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User loginUser = _context.Users.Where(User => User.Email == loginemail).SingleOrDefault();
            if(loginUser != null)
            {
                var hashedPw = hasher.VerifyHashedPassword(loginUser, loginUser.Password, loginpw);
                if(hashedPw == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("CurrentUserKey", loginUser.UserKey);
                    return RedirectToAction("Account", "Bank", new { userKey = loginUser.UserKey });
                }
            }
            ViewBag.Error = "Email or Password is not matching";
            return View("Login");
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Route("voucheruse")]
        public IActionResult VoucherUse(String voucher_use)
        {
            Voucher voucher = _context.Vouchers.SingleOrDefault(e => e.VoucherKey.Equals(voucher_use));
            Account userAccount = _context.Accounts.SingleOrDefault(e => e.AccountID == voucher.AccountID);

            DateTime today = new DateTime(DateTime.UtcNow.Ticks);
            float amount = voucher.Amount;

            if(today <= voucher.Until && voucher.Used == 0)
            {
                userAccount.Balance -= voucher.Amount;
                //voucher.Account.Balance -= amount;
                //ViewBag.Balance = userAccountVoucher.Balance;
                _context.Accounts.Update(userAccount);

                voucher.Used = 1;
                _context.Vouchers.Update(voucher);
                _context.SaveChanges();
                return View("Index");

            }

            TempData["Error"] = "Voucher alredy used or doesn't exist!";
            return View("Index");

        }
    }
}
