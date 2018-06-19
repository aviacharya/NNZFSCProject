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
        public IEnumerable<MemberPayment> GetAllMemberPaymentById(int id)
        {
            try
            {

                SqlDataReader dr;
                List<MemberPayment> MemberPaymentList = new List<MemberPayment>();
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Payment where MemberId= " + id + "", con);
                    dr = cmd.ExecuteReader();
                    MemberPayment MemberPayment = new MemberPayment();
                    while (dr.Read())
                    {

                        MemberPayment.MemberId = Convert.ToInt32(dr["MemberId"]);
                        MemberPayment.PaymentId = Convert.ToInt32(dr["PaymentId"]);
                        MemberPayment.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                        MemberPayment.PaymentAmount = Convert.ToInt32(dr["PaymentAmount"]);
                        MemberPayment.NextPaymentDate = Convert.ToDateTime(dr["NextPaymentDate"]);
                        // MemberPayment.IsRenewal = Convert.ToBoolean(dr["IsRenewal"]);
                        MemberPaymentList.Add(MemberPayment);
                    }

                    con.Close();
                }
                return MemberPaymentList;
            }

            catch (Exception e)
            {
                throw e;
            }
        }


        public MemberPayment GetMemberPaymentById(int id)
        {
            try
            {

                SqlDataReader dr;
                MemberPayment MemberPayment = new MemberPayment();
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from View_MemberPayment where IsRenewal='N' and MemberId= " + id + "", con);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {


                        MemberPayment.PaymentId = Convert.ToInt32(dr["PaymentId"]);
                        MemberPayment.NextPaymentDate = Convert.ToDateTime(dr["NextPaymentDate"]);
                        MemberPayment.MemberDetails = new MemberRegistration
                        {
                            MemberId = Convert.ToInt32(dr["MemberId"]),
                            MemberFirstName = dr["MemberFirstName"].ToString(),
                            MemberMiddleName = dr["MemberMiddleName"].ToString(),
                            MemberLastName = dr["MemberLastName"].ToString(),
                            MemberAddress = dr["MemberAddress"].ToString(),
                            EmailAddress = dr["EmailAddress"].ToString(),
                            Gender = dr["Gender"].ToString(),
                            MembershipAmount = Convert.ToInt32(dr["MembershipAmount"])

                        };

                    }

                    con.Close();
                }
                return MemberPayment;
            }

            catch (Exception e)
            {
                throw e;
            }
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
                    cmd.Parameters.AddWithValue("@NextPaymentDate", payment.NextPaymentDate);
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
                    cmd.Parameters.AddWithValue("@NextPaymentDate", payment.NextPaymentDate);
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


        public IEnumerable<MemberPayment> GetPaymentDetails(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlDataReader dr;
                    List<MemberPayment> PaymentList = new List<MemberPayment>();
                     con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Payment where MemberId= " + id +"",con);
                    dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        MemberPayment payment = new MemberPayment();
                        payment.PaymentId = Convert.ToInt32(dr["PaymentId"]);
                        payment.MemberId = Convert.ToInt32(dr["MemberId"]);
                        payment.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                        DateTime dt = Convert.ToDateTime(dr["PaymentDate"]);
                        payment._PaymentDateToDisplay = dt.ToShortDateString();
                        payment.PaymentAmount = Convert.ToInt32(dr["PaymentAmount"]);
                        payment.NextPaymentDate = Convert.ToDateTime(dr["NextPaymentDate"]);
                        DateTime dtNextPay = Convert.ToDateTime(dr["NextPaymentDate"]);
                        payment._NextPaymentDateToDisplay = dtNextPay.ToShortDateString();
                        payment.disable = "disabled";
                        PaymentList.Add(payment);
                    }

                    con.Close();
                    return PaymentList;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}