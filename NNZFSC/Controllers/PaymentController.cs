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

        [HttpPost]
        public ActionResult Edit(MemberPayment MemberPayment)
        {
            MemberPayment.PaymentBy = "Avi";
            if (string.IsNullOrEmpty(MemberPayment.PaymentDate.ToString()))
                ModelState.AddModelError("Error", "Please enter Payment Date");
            else if (string.IsNullOrEmpty(MemberPayment.PaymentAmount.ToString()))
                ModelState.AddModelError("Error", "Please enter Payment Amount");
            else if (string.IsNullOrEmpty(MemberPayment.PaymentBy.ToString()))
                ModelState.AddModelError("Error", "Please enter Payment By");

            else
            { 
                int payment = objPayment.UpdateMemberPayment(MemberPayment);
                if (payment > 0)

                {

                    ViewBag.Text = "Payment Updaed Successfully.";
                }


                else
                {
                    TempData["Message"] = "Some thing went wrong while Payment Updated .";
                }

                return RedirectToAction("Edit");


        }

        
            return View(MemberPayment);
    }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {

            var payment = objPayment.GetMemberPaymentById(id);
            payment.IsReadOnly = true;
            if(payment.MemberDetails == null)
            {
                payment.MemberDetails = new MemberRegistration();
                payment.MemberDetails.MemberId = 0;
                payment.IsReadOnly = false;
           
            }
            return View(payment);
        }
        [HttpPost]
        public JsonResult GetMemberInfo(int id)
        {

            var member = id.GetMemberById(id);
            var MemberDate = member.MembershipDate.Value.ToShortDateString();
            var MemberExpriyDate = member.MembershipExpiryDate.Value.ToShortDateString();
           
            member._MembershipDate = MemberDate;
            member._MembershipExpiryDate = MemberExpriyDate;

            var paymentDetails = objPayment.GetPaymentDetails(id);
            

            int maxId = paymentDetails.Max(x => x.PaymentId);

            foreach (var item in paymentDetails)
            {
                if (item.PaymentId == maxId) { item.disable = "enabled"; }
                member.MemberPaymentList.Add(new MemberPayment
                {
                    MemberId = item.MemberId,
                    PaymentId = item.PaymentId,
                    PaymentAmount = item.PaymentAmount,
                    PaymentDate = item.PaymentDate,
                    NextPaymentDate = item.NextPaymentDate,
                    _NextPaymentDateToDisplay = item._NextPaymentDateToDisplay,
                    _PaymentDateToDisplay = item._PaymentDateToDisplay,
                    disable = item.disable

                });

            }
           

            return Json(member, JsonRequestBehavior.AllowGet);
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
                    payment.MemberId = payment.MemberDetails.MemberId.Value;
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