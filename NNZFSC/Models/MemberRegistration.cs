using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace NNZFSC.Models
{
    public class MemberRegistration
    {
        [Key]
        public int MemberId { get; set; }

        [Display(Name = "First Name")]
        [Required (ErrorMessage ="Please enter the first Name")]
        public string MemberFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MemberMiddleName { get; set; }

        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "Please enter the Last Name")]
        public string MemberLastName { get; set; }

        [Display(Name ="Address")]
        public string MemberAddress { get; set; }

        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter the Membership Fee")]
        public int? MembershipAmount { get; set; }

        [Display(Name = "Membership Date")]
        [Required(ErrorMessage = "Please enter the Membership Date")]
        public DateTime ? MembershipDate { get; set; }

        [Display(Name = "Membership Expiry")]
        [Required(ErrorMessage = "Please enter the Membership Date")]
        public DateTime ? MembershipExpiryDate { get; set; }


        public string  MemberImageName { get; set; }

        public string  MemberImagePath { get; set; }

        public string CreateBy { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please enter the Membership Date")]
        public string Gender { get; set; }
    }
}