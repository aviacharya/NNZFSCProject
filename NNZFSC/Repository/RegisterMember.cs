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
                    if (string.IsNullOrEmpty(member.Gender)) { member.Gender = ""; }
                    cmd.Parameters.AddWithValue("@Gender", member.Gender);
                    cmd.Parameters.Add("@MemberId", SqlDbType.Int, 0, "MemberId");
                    cmd.Parameters["@MemberId"].Direction = ParameterDirection.Output;
                    con.Open();
                    
                    cmd.ExecuteNonQuery();
                    int insertedID = (int)cmd.Parameters["@MemberId"].Value;
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
                    cmd.Parameters.AddWithValue("@MemberFirstName", member.MemberFirstName);
                    if (string.IsNullOrEmpty(member.MemberMiddleName)) { member.MemberMiddleName = ""; }
                     cmd.Parameters.AddWithValue("@MemberMiddleName", member.MemberMiddleName);
                    cmd.Parameters.AddWithValue("@MemberLastName", member.MemberLastName);
                    cmd.Parameters.AddWithValue("@MemberAddress", member.MemberAddress);
                    cmd.Parameters.AddWithValue("@EmailAddress", member.EmailAddress);
                    cmd.Parameters.AddWithValue("@MembershipAmount", member.MembershipAmount);
                    cmd.Parameters.AddWithValue("@MembershipDate", member.MembershipDate);
                    cmd.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate);
                    if (string.IsNullOrEmpty(member.MemberImageName)) { member.MemberImageName = ""; }
                     cmd.Parameters.AddWithValue("@MemberImageName", member.MemberImageName);
                    if (string.IsNullOrEmpty(member.MemberImagePath)) { member.MemberImagePath = ""; }
                      cmd.Parameters.AddWithValue("@MemberImagePath", member.MemberImagePath);
                    cmd.Parameters.AddWithValue("@CreateBy", member.CreateBy);
                    con.Open();
                    //  int id = cmd.ExecuteNonQuery();
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


        public IEnumerable<MemberRegistration> AllMemberDetails()
         {
            try
            {

                SqlDataReader dr;
                List<MemberRegistration> MemberList = new List<MemberRegistration>();

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Tbl_Member", con);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        MemberRegistration member = new MemberRegistration();
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

                        MemberList.Add(member);

                    }
                    con.Close();
                }
                return MemberList;
            }

            catch(Exception e)
            {
                throw e;
            }
        }



        
    }
}