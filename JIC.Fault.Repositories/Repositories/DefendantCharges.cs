using JIC.Base.Interfaces;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class DefendantChargesRepository : EntityRepositoryBase<Cases_DefendantsCharges>, IDefendantChargesRepository
    {
        public void SyncDefendantCharges(long defendantID, List<int> crimes)
        {
            var DefendantCaseLog = DataContext.Cases_DefendatnsCaseLog.Where(defendant => defendant.DefendantID == defendantID).OrderByDescending(defendant => defendant.FromDate)
                .Select(defendant => defendant.ID).First();
            var Charges =  GetAllQuery().Where(charge => charge.DefendantCaseLogID == DefendantCaseLog).Select(charge=>charge.ChargeID).ToList();

            //Get New Crimes To be Added
            var NewCharges = crimes.Where(crime => !Charges.Contains(crime)).ToList();
            foreach (var Charge in NewCharges)
            {
                Add(new Cases_DefendantsCharges
                {
                    ChargeID = Charge,
                    DefendantCaseLogID = DefendantCaseLog
                });
                Save();
            }

            //Get Older Crimes to be Removed
            var RemoveCharges = Charges.Where(charge => !crimes.Contains(charge)).ToList();
            foreach (var Charge in RemoveCharges)
            {
                var DeleteCharge = GetAllQuery().Where(charge => charge.DefendantCaseLogID == DefendantCaseLog && charge.ChargeID == Charge).Select(charge=>charge).First();
                Delete(DeleteCharge);
                Save();
            }
        }
    }
}
