using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BankAccounts.Controllers
{
    public class BankController : Controller
    {

        private BankDbContext _context;

        public BankController(BankDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("bank/account/{userKey}")]
        public IActionResult Account(string userKey)
        {
            if (!CheckLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            Account userAccount = _context.Accounts.SingleOrDefault(e => e.UserKey == userKey);
            User currentUser = _context.Users.SingleOrDefault(e => e.UserKey == userKey);
            var transactions = _context.Transactions.Where(e => e.AccountID == userAccount.AccountID).OrderBy(e => e.Created_at).ToList();
            transactions.Reverse();
            ViewBag.History = transactions;
            ViewBag.UserName = currentUser.FirstName;
            ViewBag.Balance = userAccount.Balance;
            ViewBag.UserId = currentUser.UserID;
            ViewBag.UserKey = currentUser.UserKey;
            return View("Account");
        }

        [HttpPost]
        [Route("bank/account/process")]
        public IActionResult Process(float amount)
        {
            if (!CheckLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            string userKey = (string)HttpContext.Session.GetString("CurrentUserKey");
            Account userAccount = _context.Accounts.SingleOrDefault(e => e.UserKey == userKey);

            if (amount < 0 && userAccount.Balance < Math.Abs(amount))
            {
                TempData["Error"] = "The Balance is insufficient.";
                return RedirectToAction("Account", "Bank", new { userKey = userKey });
            }
            else
            {
                Transaction newTransaction = new Transaction
                {
                    Amount = amount,
                    AccountID = userAccount.AccountID
                };

                userAccount.Balance += amount;
                _context.Transactions.Add(newTransaction);
                _context.SaveChanges();
            }

            return RedirectToAction("Account", "Bank", new { userKey = userKey });
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool CheckLoggedIn()
        {
            if (HttpContext.Session.GetString("CurrentUserKey") == null)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        [Route("bank/account/submitvoucher")]
        public IActionResult SubmitVoucher(string voucher_name, float voucher_amount, float voucher_duration)
        {
            if(!CheckLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            string userKey = (string)HttpContext.Session.GetString("CurrentUserKey");
            Account userAccount = _context.Accounts.SingleOrDefault(e => e.UserKey == userKey);
            //User currentUser = _context.Users.SingleOrDefault(e => e.UserKey == userKey);

            Voucher newVoucher = new Voucher
            {
                VoucherKey = voucher_name,
                Amount = voucher_amount,
                Duration = voucher_duration,
                AccountID = userAccount.AccountID
            };
            newVoucher.Until = newVoucher.Created_at.AddDays(voucher_duration);

            _context.Vouchers.Add(newVoucher);
            _context.SaveChanges();


            return RedirectToAction("Account", "Bank", new { userKey = userKey});
        }
    }
}
