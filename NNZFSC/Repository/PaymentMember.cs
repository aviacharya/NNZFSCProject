using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNZFSC.Models;

namespace NNZFSC.Repository
{
    public class PaymentMember : IPaymentMember
    {
     
        public IEnumerable<MemberPayment> GetMemberPaymentById(int id)
        {
            throw new NotImplementedException();
        }

        public int InsertMemberPayment(MemberPayment payment)
        {
            throw new NotImplementedException();
        }

        public int UpdateMemberPayment(MemberPayment payment)
        {
            throw new NotImplementedException();
        }
    }
}