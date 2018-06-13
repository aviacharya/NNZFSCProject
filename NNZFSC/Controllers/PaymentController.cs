using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNZFSC.Models;
using NNZFSC.Repository;
namespace NNZFSC.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment

        IPaymentMember objPayment;

        public PaymentController()
        {
            objPayment = new PaymentMember();
           
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //List<MemberPayment> MemberPayment = new List<MemberPayment>();
            //MemberPayment = objPayment.GetMemberPaymentById(id).ToList();

            var payment = objPayment.GetMemberPaymentById(id);
            return View(payment);
        }
    }
}