using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNZFSC.Models;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
namespace NNZFSC.Repository
{
    public class PaymentMember : IPaymentMember
    {

        string connection = ConfigurationManager.ConnectionStrings["connection"].ToString();
        public IEnumerable<MemberPayment> GetMemberPaymentById(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertMemberPayment(MemberPayment payment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_AddPayment", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", payment.MemberId);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    if (string.IsNullOrEmpty(payment.PaymentBy)) { payment.PaymentBy = ""; }
                    cmd.Parameters.AddWithValue("@PaymentBy", payment.PaymentBy);
                    cmd.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
                    cmd.Parameters.AddWithValue("@IsRenewal", payment.IsRenewal);

                    con.Open();
                    int paymentid = cmd.ExecuteNonQuery();
                    con.Close();
                    return paymentid;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateMemberPayment(MemberPayment payment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdatePayment", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    if (string.IsNullOrEmpty(payment.PaymentBy)) { payment.PaymentBy = ""; }
                    cmd.Parameters.AddWithValue("@PaymentBy", payment.PaymentBy);
                    cmd.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
                    con.Open();
                    int paymentid = cmd.ExecuteNonQuery();
                    con.Close();
                    return paymentid;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}