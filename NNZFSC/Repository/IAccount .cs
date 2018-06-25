using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNZFSC.Repository
{
    interface IAccount
    {
        string GetUserIDByUserName(string UserId);
        string GetRoleByUserID(string Role);
    }
}
