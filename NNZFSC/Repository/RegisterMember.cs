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

        public int InsertMember(MemberRegistration member)

        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                {

                    SqlCommand cmd = new SqlCommand("sp_AddMember", con);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    con.Open();
                    //  int id = cmd.ExecuteNonQuery();
                    int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return insertedID;

                }
              

            }

            catch
            {
                throw new Exception("Eror in inserting Member");

            }

           
        }



        public int UpdateMember (MemberRegistration member)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
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

        public void  DeleteMember()
        {

        }


        public void DisplayMember()
        {

        }

    }
}