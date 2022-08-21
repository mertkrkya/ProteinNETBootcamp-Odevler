using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CacheProject.Core.Dto;
using CacheProject.Data.Context;

namespace JWTProject.API.Validator
{
    public static class Validator
    {
        public static string PersonValidator(PersonDto request, AppDbContext dbctx = null)
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
            if (!string.IsNullOrWhiteSpace(request.Email))
                if (!IsValidEmail(request.Email))
                    return "Geçersiz Email";
            if(!string.IsNullOrWhiteSpace(request.Phone))
                if (!IsPhoneNumber(request.Phone))
                    return "Geçersiz Telefon Numarası";
            return "";
        }
        public static string PersonDeleteValidator(int personId, AppDbContext dbctx = null)
        {
            if (dbctx == null)
                dbctx = new AppDbContext();
            var person = dbctx.People.FirstOrDefault(r => r.Id == personId);
            if (person == null)
                return "Geçersiz Person";
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
