using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNZFSC.Models;
using NNZFSC.Repository;
using System.Data;


namespace NNZFSC.Controllers
{
    public class RegisterMemberController : Controller
    {
        // GET: RegisterMember

        IRegisterMember objRegisterMember;
        IPaymentMember objPaymentMember;


        public RegisterMemberController()
        {
            objRegisterMember = new RegisterMember();
            objPaymentMember = new PaymentMember();
        }
        public ActionResult Index()
        {
            List<MemberRegistration> member = new List<MemberRegistration>();
            member = objRegisterMember.AllMemberDetails().ToList();
            return View(member);
        }


        [HttpGet]
        public ActionResult create()
        {
            MemberRegistration MemberRegistration = new MemberRegistration();
            return View(MemberRegistration);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            objRegisterMember.DeleteMember(id);
            return RedirectToAction("index");
        }


        [HttpGet]
        public ActionResult Details( int id)
        {
            MemberRegistration MemberRegistration = new MemberRegistration();
            
            var memberDetails = id.GetMemberById(id);
            var paymentDetails = objPaymentMember.GetPaymentDetails(id);
            memberDetails.IsReadOnly = true;
            //TO display Memberdetails from dashboard
            if(memberDetails == null || memberDetails.MemberId ==null)
            {
                memberDetails.IsReadOnly = false;
            }
           
            if (paymentDetails.Count() == 0 && memberDetails.MemberId == null)
            {
                return View(memberDetails);

            }
            int maxId = paymentDetails.Max(x => x.PaymentId);
                     
            foreach(var item in paymentDetails)
            {
                if(item.PaymentId == maxId) { item.disable = "enabled"; }
                memberDetails.MemberPaymentList.Add(new MemberPayment
                {
                    MemberId = item.MemberId,
                    PaymentId = item.PaymentId,
                    PaymentAmount = item.PaymentAmount,
                    PaymentDate = item.PaymentDate,
                    NextPaymentDate = item.NextPaymentDate,
                    _NextPaymentDateToDisplay = item._NextPaymentDateToDisplay,
                    _PaymentDateToDisplay = item._PaymentDateToDisplay,
                    disable= item.disable

                });
               
            }
            return View(memberDetails);
        }



        [HttpPost]
        public ActionResult create(MemberRegistration ObjMember)
        {
            //  MemberRegistration MemberRegistration = new MemberRegistration();
            //  UpdateModel(MemberRegistration);
            if (string.IsNullOrEmpty(ObjMember.MemberFirstName))
                ModelState.AddModelError("Error", "Please enter First Name");

            else if (string.IsNullOrEmpty(ObjMember.MemberLastName))
                ModelState.AddModelError("Error", "Please enter Last Name");

            else if (string.IsNullOrEmpty(ObjMember.MemberAddress))
                ModelState.AddModelError("Error", "Please enter Member Address");


            else if (string.IsNullOrEmpty(ObjMember.MembershipAmount.ToString()))
                ModelState.AddModelError("Error", "Please enter Membership Amount");

            else if (string.IsNullOrEmpty(Convert.ToString(ObjMember.MembershipDate)))
                ModelState.AddModelError("Error", "Please enter Membership Date");

            else
            {

                //  ObjMember.CreatedBy = Session["UserName"].ToString();
                ObjMember.CreateBy = "Avi";
                ObjMember.Gender = "M";

                int MemberId = objRegisterMember.InsertMember(ObjMember);

                if (MemberId > 0)
                {


                    int ResultPayment = InsertPayment(ObjMember, MemberId);

                    if (ResultPayment > 0)
                    {
                        ViewBag.Text = "Member Created Successfully.";
                    }

                    else
                    {
                        TempData["Message"] = "Some thing went wrong while Member Created .";
                    }

                }
                else
                {
                    TempData["Message"] = "Some thing went wrong while Member Created.";
                }
                return RedirectToAction("Create");
            }
            return View(ObjMember);
        }

        [NonAction]
        public int InsertPayment(MemberRegistration ObjMember, int MemberId)
        {
            try {
                MemberPayment ObjMemberPayment = new MemberPayment();
                ObjMemberPayment.MemberId = MemberId;
                ObjMemberPayment.PaymentDate = ObjMember.MembershipDate;
                ObjMemberPayment.NextPaymentDate = ObjMember.MembershipExpiryDate;
                ObjMemberPayment.PaymentAmount = ObjMember.MembershipAmount;
                ObjMemberPayment.PaymentBy = "Avi";
                ObjMemberPayment.IsRenewal = false;
                int paymentid = objPaymentMember.InsertMemberPayment(ObjMemberPayment);
                return paymentid;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            var model = id.GetMemberById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MemberRegistration objMember)
        {

            if (string.IsNullOrEmpty(objMember.MemberFirstName))
                ModelState.AddModelError("Error", "Please enter First Name");

            else if (string.IsNullOrEmpty(objMember.MemberLastName))
                ModelState.AddModelError("Error", "Please enter Last Name");

            else if (string.IsNullOrEmpty(objMember.MemberAddress))
                ModelState.AddModelError("Error", "Please enter Member Address");


            else if (string.IsNullOrEmpty(objMember.MembershipAmount.ToString()))
                ModelState.AddModelError("Error", "Please enter Membership Amount");

            else if (string.IsNullOrEmpty(Convert.ToString(objMember.MembershipDate)))
                ModelState.AddModelError("Error", "Please enter Membership Date");

            else
            {

                //  ObjMember.CreatedBy = Session["UserName"].ToString();
                objMember.CreateBy = "Avi";

                int MemberId = objRegisterMember.UpdateMember(objMember);
                if (MemberId > 0)
                {
                    ViewBag.Text = "Member updated Successfully.";
                }

                else
                {
                    TempData["Message"] = "Some thing went wrong while Member updated.";
                }
                return RedirectToAction("Edit");
            }

            return View(objMember);

        }
        [HttpPost]
        public JsonResult GetExpirydate(DateTime date)
        {
            string ExpiryDate = date.AddYears(1).ToShortDateString();
            return Json(ExpiryDate, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetMemberInfo(int id)
        {

            var member = id.GetMemberById(id);
            var MemberDate = member.MembershipDate.Value.ToShortDateString();
            var MemberExpriyDate = member.MembershipExpiryDate.Value.ToShortDateString();

            member._MembershipDate = MemberDate;
            member._MembershipExpiryDate = MemberExpriyDate;

            return Json(member, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateRenewMember(MemberPayment payment)
        {
            try
            {
                payment.IsRenewal = true;
                payment.PaymentBy = "Avi";
                int PaymentId = objPaymentMember.UpdateMemberPayment(payment);
                if (PaymentId > 0)
                {
                    ViewBag.Text = "Payment updated Successfully.";

                }

                else
                {
                    TempData["Message"] = "Some thing went wrong while Member updated.";
                }

                return Json(payment, JsonRequestBehavior.AllowGet);
            }


            catch (Exception e)
            {
                throw e;
            }
           
        }
    }
}