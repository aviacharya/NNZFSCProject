using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNZFSC.Models;

namespace NNZFSC.Repository
{
    interface IRegisterMember
    {
        int InsertMember(MemberRegistration member);

        int UpdateMember(MemberRegistration member);

        void DisplayMember();

        void DeleteMember();
    }
}
