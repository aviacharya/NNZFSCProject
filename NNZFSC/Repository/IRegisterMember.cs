using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNZFSC.Models;
using System.Data.SqlClient;
using System.Data;

namespace NNZFSC.Repository
{
    interface IRegisterMember
    {
        int InsertMember(MemberRegistration member);

        int UpdateMember(MemberRegistration member);

        IEnumerable<MemberRegistration> AllMemberDetails();

        void DeleteMember(int ? id);
    }
}
