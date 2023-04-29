using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using WebApiBank.Models.Context;
using WebApiBank.Models.Entities;

namespace WebApiBank.Models.Inits
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
           CardInfo ci = new CardInfo();
            ci.CardUsername = "Chuck Norris";
            ci.CardNumber = "1111 1111 1111 1111";
            ci.CardExpiryMonth = 1;
            ci.CardExpriyYear = 2025;
            ci.SecurityNumber = "222";
            ci.Limit = 50000;
            ci.Balance = 50000;
            context.Cards.Add(ci);
            context.SaveChanges();

        }
    }
}