using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNZFSC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace NNZFSC.Repository
{
    public class RegisterMember :IRegisterMember
    {

        string connection = ConfigurationManager.ConnectionStrings["connection"].ToString();
        public int InsertMember(MemberRegistration member)

        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {

                    SqlCommand cmd = new SqlCommand("sp_AddMember", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberFirstName", member.MemberFirstName);
                    if (string.IsNullOrEmpty(member.MemberMiddleName)) { member.MemberMiddleName =""; }
                    cmd.Parameters.AddWithValue("@MemberMiddleName", member.MemberMiddleName);
                    cmd.Parameters.AddWithValue("@MemberLastName", member.MemberLastName);
                    cmd.Parameters.AddWithValue("@MemberAddress", member.MemberAddress);
                    cmd.Parameters.AddWithValue("@EmailAddress", member.EmailAddress);
                    cmd.Parameters.AddWithValue("@MembershipAmount", member.MembershipAmount);
                    cmd.Parameters.AddWithValue("@MembershipDate", member.MembershipDate);
                    cmd.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate);
                    if (string.IsNullOrEmpty(member.MemberImageName)) { member.MemberImageName =""; }
                     cmd.Parameters.AddWithValue("@MemberImageName", member.MemberImageName); 
                    if (string.IsNullOrEmpty(member.MemberImagePath)) { member.MemberImagePath =""; }
                    cmd.Parameters.AddWithValue("@MemberImagePath", member.MemberImagePath); 
                    cmd.Parameters.AddWithValue("@CreateBy", member.CreateBy);
                    con.Open();
                    
                    int insertedID = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();
                    return insertedID;

                }
              

            }

            catch(Exception ex)
            {
                throw ex;

            }

           
        }



        public int UpdateMember (MemberRegistration member)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(connection))
                {

                    SqlCommand cmd = new SqlCommand("sp_UpdateMember", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", member.MemberId);
                    cmd.Parameters.AddWithValue("@FirstName", member.MemberFirstName);
                    cmd.Parameters.AddWithValue("@MemberMiddleName", member.MemberMiddleName);
                    cmd.Parameters.AddWithValue("@MemberLastName", member.MemberLastName);
                    cmd.Parameters.AddWithValue("@MemberAddress", member.MemberAddress);
                    cmd.Parameters.AddWithValue("@EmailAddress", member.EmailAddress);
                    cmd.Parameters.AddWithValue("@MembershipAmount", member.MembershipAmount);
                    cmd.Parameters.AddWithValue("@MembershipDate", member.MembershipDate);
                    cmd.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate);
                    cmd.Parameters.AddWithValue("@MemberImageName", member.MemberImageName);
                    cmd.Parameters.AddWithValue("@MemberImagePath", member.MemberImagePath);
                    cmd.Parameters.AddWithValue("@CreatedBy", member.CreateBy);
                    con.Open();
                    //  int id = cmd.ExecuteNonQuery();
                    int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return insertedID;

                }
            }

            catch
            {
                throw new Exception("Eror in Updating Member");

            }
            

        }

        public void  DeleteMember(int ? id)
        {

            using (SqlConnection con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("sp_DeleteMember",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }


        public void DisplayMember()
        {

        }


        
    }
}