using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBank.Models.Entities
{
    public class CardInfo : BaseEntity
    {

        public string CardUsername { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpriyYear { get; set; }

        public decimal Limit { get; set; }
        public decimal Balance { get; set; }


    }
}