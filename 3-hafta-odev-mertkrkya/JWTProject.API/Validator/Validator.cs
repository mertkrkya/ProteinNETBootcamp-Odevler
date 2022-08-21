using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JWTProject.Core.Dto;
using JWTProject.Core.Entities;
using JWTProject.Core.Models;
using JWTProject.Core.Repositories;
using JWTProject.Core.Services;
using JWTProject.Data.Context;
using JWTProject.Data.Repositories;

namespace JWTProject.API.Validator
{
    public static class Validator
    {
        public static string LoginValidator(LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return "Giriş bilgileri boş gönderilemez.";
            }

            if (string.IsNullOrWhiteSpace((loginRequest.UserName)))
            {
                return "UserName boş gönderilemez.";
            }
            if (string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return "Şifre boş gönderilemez.";
            }

            return "";
        }
        public static string AccountValidator(AccountDto request, AppDbContext dbctx=null)
        {
            if (request == null)
            {
                return "Account nesnesi boş gönderilemez.";
            }

            if (dbctx == null)
                dbctx = new AppDbContext();
            if (string.IsNullOrWhiteSpace(request.UserName))
                return "UserName boş gönderilemez.";
            var controlUserName = dbctx.Accounts.FirstOrDefault(r => r.UserName == request.UserName);
            if (controlUserName != null)
                return "Bu UserName daha önce alınmıştır.";
            if (string.IsNullOrWhiteSpace(request.Password))
                return "Password boş gönderilemez.";
            if (string.IsNullOrWhiteSpace(request.Email))
                return "Email boş gönderilemez.";
            if (!IsValidEmail(request.Email))
                return "Geçersiz Email";
            if (string.IsNullOrWhiteSpace(request.Name))
                return "Name boş gönderilemez.";
            return "";
        }
        public static string PersonValidator(PersonDto request, int accountId, AppDbContext dbctx = null)
        {
            if (request == null)
            {
                return "Person nesnesi boş gönderilemez.";
            }

            if (dbctx == null)
                dbctx = new AppDbContext();
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return "FirstName boş gönderilemez.";
            if (string.IsNullOrWhiteSpace(request.LastName))
                return "LastName boş gönderilemez.";
            if (accountId != request.AccountId)
                return "Yetkisiz AccountId.";
            var controlAccount = dbctx.Accounts.FirstOrDefault(r => r.Id == request.AccountId);
            if (controlAccount == null)
                return "Bu Account bulunamamıştır.";
            if (!string.IsNullOrWhiteSpace(request.Email))
                if (!IsValidEmail(request.Email))
                    return "Geçersiz Email";
            if(!string.IsNullOrWhiteSpace(request.Phone))
                if (!IsPhoneNumber(request.Phone))
                    return "Geçersiz Telefon Numarası";
            return "";
        }
        public static string PersonDeleteValidator(int personId, int accountId, AppDbContext dbctx = null)
        {
            if (dbctx == null)
                dbctx = new AppDbContext();
            var person = dbctx.People.FirstOrDefault(r => r.Id == personId);
            if (person == null)
                return "Geçersiz Person";
            if (person.AccountId != accountId)
                return "Yetkisiz Account";
            return "";
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool IsPhoneNumber(string number)
        {
            if (number.Length < 10 || number.Length > 13)
                return false;
            number = number.Replace("+", "");
            if (!ulong.TryParse(number, out ulong n))
                return false;
            return true;
        }

    }
}
