using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NNZFSC.Models;


namespace NNZFSC.Repository
{
    public class AccountRepository : IAccount
    {
        
        string  connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private NNZFSCDataContextDataContext db = new NNZFSCDataContextDataContext();
        public string GetUserIDByUserName(string EmailId)
        {
            try
            {
               
                string userId ="";
                var Acc = db.AspNetUsers.Where(a => a.Email == EmailId);
                foreach(var item in Acc)
                {
                    userId = item.Id;
                }
               
                return userId;
                
            }

            catch(Exception e)
            {
                throw e;
            }


           
        }

        public string GetRoleByUserID(string UserId)
        {
            try
            {
                string s = "";
              

                var Role =
                            from Roles in db.AspNetRoles
                            join UserRoles in db.AspNetUserRoles on Roles.Id  equals UserRoles.RoleId
                            where UserRoles.UserId == UserId
                            select new { RoleType = Roles.Name };


              foreach(var item in Role)
                {
                    s = item.RoleType;
                }

                return s;

            }

            catch(Exception e)
            {
                throw e;
            }
        }
    }
}