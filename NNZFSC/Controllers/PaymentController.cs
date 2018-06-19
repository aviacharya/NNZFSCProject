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
            
            var payment = objPayment.GetMemberPaymentById(id);
            return View(payment);
        }

        [HttpPost]
        public ActionResult Edit(MemberPayment payment)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {

            var payment = objPayment.GetMemberPaymentById(id);
            return View(payment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(MemberPayment payment)
        {
            try
            {
                if (string.IsNullOrEmpty(payment.PaymentDate.ToString()))
                    ModelState.AddModelError("Error", "Please enter Payment Date");

                else if (string.IsNullOrEmpty(payment.NextPaymentDate.ToString()))
                    ModelState.AddModelError("Error", "Please enter Next Payment Date");

                else if (string.IsNullOrEmpty(payment.PaymentAmount.ToString()))
                    ModelState.AddModelError("Error", "Please enter Amount");
                else
                {
                    payment.MemberId = payment.MemberDetails.MemberId;
                    payment.IsRenewal = true;
                    payment.PaymentBy = "Avi";
                    int PaymentId = objPayment.InsertMemberPayment(payment);
                    if (PaymentId > 0)
                    {
                        ViewBag.Text = "Member Created Successfully.";
                    }


                    else
                    {
                        TempData["Message"] = "Some thing went wrong while Member Created .";
                    }

                    return RedirectToAction("create");
                }

                
                return View(payment);
            }

           
            catch (Exception e)
            {
                throw e;
            }

           
        }
    }
}