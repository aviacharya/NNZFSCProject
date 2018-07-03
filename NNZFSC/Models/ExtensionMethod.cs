using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace NNZFSC.Models
{
    public static class ExtensionMethod
    {

        public static MemberRegistration GetMemberById(this int id , int value)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["connection"].ToString();
                SqlDataReader dr;
                using (SqlConnection con = new SqlConnection(connection))
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Member where MemberId=" + id + " ", con);
                    dr = cmd.ExecuteReader();
                    MemberRegistration member = new MemberRegistration();
                    member.MemberPaymentList = new List<MemberPayment>();
                    while (dr.Read())
                    {

                        member.MemberId = Convert.ToInt32(dr["MemberId"]);
                        member.MemberFirstName = dr["MemberFirstName"].ToString();
                        member.MemberMiddleName = dr["MemberMiddleName"].ToString();
                        member.MemberLastName = dr["MemberLastName"].ToString();
                        member.MemberAddress = dr["MemberAddress"].ToString();
                        member.EmailAddress = dr["EmailAddress"].ToString();
                        member.MembershipAmount = Convert.ToInt32(dr["MembershipAmount"]);
                        member.MembershipDate = Convert.ToDateTime(dr["MembershipDate"].ToString());
                        member.MembershipExpiryDate = Convert.ToDateTime(dr["MembershipExpiryDate"].ToString());
                        member.MemberImageName = dr["MemberImageName"].ToString();
                        member.MemberImagePath = dr["MemberImagePath"].ToString();
                        member.CreateBy = dr["CreateBy"].ToString();

                    }
                    return member;
                }

            }

            catch (Exception e)
            {
                throw e;
            }

        }
    }
}