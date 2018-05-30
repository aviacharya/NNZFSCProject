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


        public RegisterMemberController()
        {
            objRegisterMember = new RegisterMember();
        }
        public ActionResult Index()
        {
            List<MemberRegistration> member = new List<MemberRegistration>();
            member= objRegisterMember.AllMemberDetails().ToList();
            return View(member);
        }


        [HttpGet]
        public ActionResult create()
        {
            MemberRegistration MemberRegistration = new MemberRegistration();
           return View(MemberRegistration);
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

                int MemberId = objRegisterMember.InsertMember(ObjMember);
                if (MemberId > 0)
                {
                   ViewBag.Text = "Member Created Successfully.";
                }

                else
                {
                    TempData["Message"] = "Some thing went wrong while Member Created.";
                }
                return RedirectToAction("Create");
            }
            return View(ObjMember);
        }
    }
}