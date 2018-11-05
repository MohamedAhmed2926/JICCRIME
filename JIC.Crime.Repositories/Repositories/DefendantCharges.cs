using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{
    public class DefendantChargesRepository : EntityRepositoryBase<Cases_DefendantsCharges>, IDefendantChargesRepository
    {
        public void SyncDefendantCharges(long defendantID, List<int> crimes)
        {
            var Charges =  GetAllQuery().Where(charge => charge.DefendantID == defendantID).Select(charge=>charge.ChargeID).ToList();

            //Get New Crimes To be Added
            var NewCharges = crimes.Where(crime => !Charges.Contains(crime)).ToList();
            foreach (var Charge in NewCharges)
            {
                Add(new Cases_DefendantsCharges
                {
                    ChargeID = Charge,
                    DefendantID = defendantID
                });
                Save();
            }

            //Get Older Crimes to be Removed
            var RemoveCharges = Charges.Where(charge => !crimes.Contains(charge)).ToList();
            foreach (var Charge in RemoveCharges)
            {
                var DeleteCharge = GetAllQuery().Where(charge => charge.DefendantID == defendantID && charge.ChargeID == Charge).Select(charge=>charge).First();
                Delete(DeleteCharge);
                Save();
            }
        }
    }
}
