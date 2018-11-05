using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class DefectsService : ServiceBase, IDefectsService
    {
        public DefendantsComponent DefentantsComponent { get { return GetComponent<DefendantsComponent>(); } }
        public SessionsComponent  SessionComp { get { return GetComponent<SessionsComponent >(); } }
        public VictimsComponent VictimsComponent { get { return GetComponent<VictimsComponent>(); } }
        public DefendantsSessionLogComponent DefendantsSessionLogComp { get { return GetComponent<DefendantsSessionLogComponent>(); } }
        public VictimsSessionLogComponent VictimsSessionLogComp { get { return GetComponent<VictimsSessionLogComponent>(); } }
        public DefectsService(CaseType caseType) : base(caseType)
        {

        }

        internal bool DeleteCaseDefects(int caseID)
        {
            var Defendants = DefentantsComponent.GetDefendantsByCaseID(caseID);
            foreach (var Defendant in Defendants)
            {
                if (DefentantsComponent.DeleteDefendant(caseID, Defendant.ID) == DeleteDefectStatus.NotDeleted)
                    return false;
            }
            var Victims = VictimsComponent.GetVictimsByCaseID(caseID);
            foreach (var Victim in Victims)
            {
                if (VictimsComponent.DeleteVictim(caseID, Victim.ID) == DeleteDefectStatus.NotDeleted)
                    return false;
            }
            return true;
        }

        public List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID, int SessionID)
        {
            List<vw_CaseDefectsData> DefectsList = new List<vw_CaseDefectsData>();
            DefectsList.AddRange(DefentantsComponent.GetDefendantsByCaseID(CaseID,SessionID ));
            DefectsList.AddRange(VictimsComponent.GetVictimsByCaseID(CaseID,SessionID ));

            DefectsList = DefectsList.GroupBy(e => new { e.PersonID , e.DefectType  }).Select(e => e.OrderBy(d => d.PersonID).ThenBy(c => c.DefectType).LastOrDefault()).Distinct().ToList();
            return DefectsList;
        }

        public SaveDefectsStatus UpdatePresenceOfDefects(List<vw_CaseDefectsData> DefectsList,int SessionID , out List<string> Defect )
        {

            if (SessionComp.IsSentToJudge(SessionID))
            {
                Defect = new List<string>();
                return SaveDefectsStatus.SessionSentToJudge;
            }

            List<SaveDefectsStatus> StatusList= new List<SaveDefectsStatus> ();
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    Defect = new List<string>();
                    foreach (vw_CaseDefectsData D in DefectsList)
                    {
                        if (D.DefectType == PartyTypes.Defendant)
                        {
                            if (DefendantsSessionLogComp.ISPresentedBefore(D.ID,SessionID) && D.Presence == PresenceStatus.AbsenceAttendance)
                            {

                                Defect.Add(DefentantsComponent.GetName((int)D.ID));

                            }
                        }
                    }
                    if (Defect.Count != 0)
                    {
                        return SaveDefectsStatus.DefendantsPresenceFailed;
                    }


                    foreach (vw_CaseDefectsData D in DefectsList)
                    {
                        if (D.DefectType == PartyTypes.Defendant)
                        {
                            StatusList.Add( DefendantsSessionLogComp.UpdatePresence(D, SessionID));
                        }

                        else if (D.DefectType == PartyTypes.Victim)
                        {
                            StatusList.Add(VictimsSessionLogComp.UpdatePresence(D, SessionID));
                        }

                    }
                    if (Transaction != null)
                        Transaction.Commit();

                 
                  //  Defect.Clear();
                    foreach (SaveDefectsStatus SD in StatusList)
                    {
                        if (SD == SaveDefectsStatus.Saved_Before)
                        { return SaveDefectsStatus.Saved_Before; }
                    }
                    return SaveDefectsStatus.Saved;
                }
              
            }
            catch(Exception ex)
            {
                HandleException(ex);
                Defect = null;
                return SaveDefectsStatus.Failed_To_Save;
            }
        }

        public List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID)
        {
            List<vw_CaseDefectsData> DefectsList = new List<vw_CaseDefectsData>();
            DefectsList.AddRange(DefentantsComponent.GetDefendantsByCaseID(CaseID));
            DefectsList.AddRange(VictimsComponent.GetVictimsByCaseID(CaseID));
            return DefectsList;
        }
      public  SavePartSOrder SaveOrder(List<vw_CaseDefectData> CasePartylist)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {foreach(var party in CasePartylist)
                    switch (party.DefectType)
                    {
                        case PartyTypes.Victim:
                            SaveOrderVictim(party);
                            break;
                        case PartyTypes.Defendant:
                            SaveOrderDefect(party);
                            break;
                        case PartyTypes.VictimAndDefendant:
                                SaveOrderVictim(party);
                                SaveOrderDefect(party);
                            break;
                    }
                    if (Transaction != null)
                        Transaction.Commit();
                    return SavePartSOrder.SavedOrder;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return SavePartSOrder.Faild_To_Save;
            }
        }

        private SavePartSOrder SaveOrderDefect(vw_CaseDefectData party)
        {
            return DefentantsComponent.SaveOrderDefect(party);  
        }

        private SavePartSOrder SaveOrderVictim(vw_CaseDefectData party)
        {
            return VictimsComponent.SaveOrderVictim(party);
        }

        public SaveDefectsStatus AddCaseDefect(vw_CaseDefectData CaseDefect)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    switch (CaseDefect.DefectType)
                    {
                        case PartyTypes.Victim:
                            AddVictimDefect(CaseDefect);
                            break;
                        case PartyTypes.Defendant:
                            AddDefendantDefect(CaseDefect);
                            break;
                        case PartyTypes.VictimAndDefendant:
                            AddVictimDefect(CaseDefect);
                            AddDefendantDefect(CaseDefect);
                            break;
                    }
                    if (Transaction != null)
                        Transaction.Commit();
                    return SaveDefectsStatus.Saved;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return SaveDefectsStatus.Failed_To_Save;
            }
            
        }

        public SaveDefectsStatus EditCaseDefect(vw_CaseDefectData CaseDefect)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    switch (CaseDefect.DefectType)
                    {
                        case PartyTypes.Victim:
                            EditVictimDefect(CaseDefect);
                            break;
                        case PartyTypes.Defendant:
                            EditDefendantDefect(CaseDefect);
                            break;
                        case PartyTypes.VictimAndDefendant:
                            EditVictimDefect(CaseDefect);
                            EditDefendantDefect(CaseDefect);
                            break;
                    }
                    if (Transaction != null)
                        Transaction.Commit();
                    return SaveDefectsStatus.Saved;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);

                return SaveDefectsStatus.Failed_To_Save;
            }
        }

        public DeleteDefectStatus DeleteCaseDefect(int CaseID, long DefectID, PartyTypes? DefectType)
        {
            try
            {
                using (var Transaction = BeginDatabaseTransaction())
                {
                    switch (DefectType)
                    {
                        case PartyTypes.Victim:
                            DeleteVictimDefect(CaseID, DefectID);
                            break;
                        case PartyTypes.Defendant:
                            DeleteDefendantDefect(CaseID, DefectID);
                            break;
                        case PartyTypes.VictimAndDefendant:
                            DeleteVictimDefect(CaseID, DefectID);
                            DeleteDefendantDefect(CaseID, DefectID);
                            break;
                    }
                    if (Transaction != null)
                        Transaction.Commit();
                    return DeleteDefectStatus.Deleted ;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return DeleteDefectStatus.NotDeleted ;
        }

        private SaveDefectsStatus AddDefendantDefect(vw_CaseDefectData CaseDefect)
        {
            return DefentantsComponent.AddDefendant(CaseDefect.CaseID, new vw_DefendantData
            {
                PersonID = CaseDefect.PersonID,
                Crimes = CaseDefect.Crimes,
                IsCivilRights = CaseDefect.IsCivilRightProsecutor,
                DefendantStatus = CaseDefect.DefendantStatus.Value,
                Order = (DefentantsComponent.GetLatestDefendantOrder(CaseDefect.CaseID) + 1)
            });
        }
       
        private SaveDefectsStatus AddVictimDefect(vw_CaseDefectData CaseDefect)
        {
            return VictimsComponent.AddVictim(CaseDefect.CaseID, new vw_VictimData
            {
                PersonID = CaseDefect.PersonID,
                IsCivilRights = CaseDefect.IsCivilRightProsecutor,
                Order = (VictimsComponent.GetLatestVictimOrder(CaseDefect.CaseID) + 1)
            });
        }

        private SaveDefectsStatus EditDefendantDefect(vw_CaseDefectData CaseDefect)
        {
            return DefentantsComponent.EditDefendant(CaseDefect.CaseID, new vw_DefendantData
            {
                PersonID = CaseDefect.PersonID,
                Crimes = CaseDefect.Crimes,
                IsCivilRights = CaseDefect.IsCivilRightProsecutor,
                DefendantStatus = CaseDefect.DefendantStatus.Value,
                Order=CaseDefect.Order,
             //   Order = (DefentantsComponent.GetLatestDefendantOrder(CaseDefect.CaseID) + 1),
                DefendantID = CaseDefect.ID
            });
        }

        private SaveDefectsStatus EditVictimDefect(vw_CaseDefectData CaseDefect)
        {
            return VictimsComponent.EditVictim(CaseDefect.CaseID, new vw_VictimData
            {
                VictimID = CaseDefect.ID,
                PersonID = CaseDefect.PersonID,
                IsCivilRights = CaseDefect.IsCivilRightProsecutor,
                Order = CaseDefect.Order
            });
        }
        private DeleteDefectStatus DeleteDefendantDefect(int CaseID, long DefendantsID)
        {
            return DefentantsComponent.DeleteDefendant(CaseID,DefendantsID);
        }

        private DeleteDefectStatus DeleteVictimDefect(int CaseID, long VictimID)
        {
            return VictimsComponent.DeleteVictim(CaseID,VictimID);
        }

        public vw_CaseDefectsData GetCaseDefect(int caseID, long partyID, PartyTypes? partyType)
        {
            switch (partyType)
            {
                case PartyTypes.Victim:
                    return VictimsComponent.GetCaseDefect(caseID, partyID);
                case PartyTypes.Defendant:
                    return DefentantsComponent.GetCaseDefect(caseID, partyID);
            }
            return null;
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            return DefentantsComponent.IsPersonInCase(personID, caseID) || VictimsComponent.IsPersonInCase(personID, caseID);
        }
        public CaseStatus CaseInFlow(int CaseID)
        {
            return DefentantsComponent.CaseInFlow(CaseID);
        }
    public bool IsPresenceSaved(int SessionID)
        {
            if (!DefendantsSessionLogComp.IsPresenceSaved(SessionID) || !VictimsSessionLogComp.IsPresenceSaved(SessionID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
