using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNZFSC.Models;
namespace NNZFSC.Repository
{
    interface IPaymentMember
    {
        int InsertMemberPayment(MemberPayment payment);
        int UpdateMemberPayment(MemberPayment payment);
        IEnumerable<MemberPayment> GetAllMemberPaymentById(int id);

        MemberPayment GetMemberPaymentById(int id);
    }
}
