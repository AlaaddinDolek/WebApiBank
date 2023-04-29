using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBank.DesignPatterns.SingletonPattern;
using WebApiBank.Models.Context;
using WebApiBank.Models.Entities;
using WebApiBank.RequestModel;

namespace WebApiBank.Controllers
{
    public class PaymentController : ApiController
    {

        MyContext _db;

        public PaymentController()
        {
            _db = DBTool.DBInstance;
        }

        //Aşağıdaki action sadece development testi içindir. API canlıya çıkacağı zaman kesinlikle açık bırakılmamalıdır
        //[HttpGet]
        //public List<PaymentResponseModel> GetAll()
        //{
        //    return _db.Cards.Select(x => new PaymentResponseModel
        //    {
        //        CardExpiryMonth = x.CardExpiryMonth,
        //        CardExpiryYear = x.CardExpriyYear,
        //        CardNumber = x.CardNumber
        //    }).ToList();
        //}

        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentRequestModel item)
        {
            CardInfo ci = _db.Cards.FirstOrDefault(x=> x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUsername == item.CardUsername && x.CardExpriyYear == item.CardExpiryYear && x.CardExpiryMonth == item.CardExpiryMonth);

            if(ci != null)
            {
                if (ci.CardExpriyYear < DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");
                } 
                else if(ci.CardExpriyYear == DateTime.Now.Year)
                {
                    if(ci.CardExpiryMonth< DateTime.Now.Month)
                    {
                        return BadRequest("Expired Card(Month)");
                    }
                    if(ci.Balance >= item.ShoppingPrice)
                    {
                        SetBalance(item, ci);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Balance exceeded");
                    }
                } 
                else if(ci.Balance >= item.ShoppingPrice)
                {
                    SetBalance(item, ci);
                    return Ok();
                }
                return BadRequest("Balance exceeded");
            }
            return BadRequest("Card Info Wrong");
        }

        private void SetBalance(PaymentRequestModel item, CardInfo ci)
        {
            ci.Balance -= item.ShoppingPrice;
            //ShoppingPrice' tan yüzdelik komisyon alınıp kalan miktar alacaklının hesabına aktarılır..
            _db.SaveChanges();
        }


    }
}
