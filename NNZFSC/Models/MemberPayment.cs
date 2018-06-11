using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NNZFSC.Models
{
    public class MemberPayment
    {
        public int PaymentId { get; set; }

        public int MemberId { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name ="Expiry Date")]
        public DateTime NextPaymentDate { get; set; }

        [Display(Name = "Renewal By")]
        public string PaymentBy { get; set; }

        [Display(Name = "Amount")]
        public int PaymentAmount { get; set; }

        public bool IsRenewal { get; set; }
    }
}