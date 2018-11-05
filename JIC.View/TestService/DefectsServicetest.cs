using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.View.TestService
{
    public class DefectsServicetest : IDefectsService
    {
        public SaveDefectsStatus AddCaseDefect(vw_CaseDefectData CaseDefect)
        {
            throw new NotImplementedException();
        }

        public CaseStatus CaseInFlow(int CaseID)
        {
            throw new NotImplementedException();
        }

        public DeleteDefectStatus DeleteCaseDefect(int CaseID, long DefectID, PartyTypes? DefectType)
        {
            throw new NotImplementedException();
        }

        public SaveDefectsStatus EditCaseDefect(vw_CaseDefectData CaseDefect)
        {
            throw new NotImplementedException();
        }

        public vw_CaseDefectsData GetCaseDefect(int caseID, long partyID, PartyTypes? partyType)
        {
            throw new NotImplementedException();
        }

        public List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID, int SessionID)
        {
            return new List<vw_CaseDefectsData>
            {
                new vw_CaseDefectsData
                {
                    Address = "عين شمس",
                    Age = 16,
                    Birthdate = DateTime.Now,
                    CaseID = 1,
                    JobName = "طالب",
                    NationalID ="12345678912345",
                    Name = "جون ماهر",
                    Nationality = "مصرى",
                    PersonID = 1


                },
                new vw_CaseDefectsData
                {
                    Address = "عين شمس",
                    Age = 16,
                    Birthdate = DateTime.Now,
                    CaseID = 2,
                    JobName = "طالب",
                    NationalID ="12345678912345",
                    Name = "جون ماهر",
                    Nationality = "مصرى",
                    PersonID = 2
                },
                new vw_CaseDefectsData
                {
                    Address = "عين شمس",
                    Age = 16,
                    Birthdate = DateTime.Now,
                    CaseID = 3,
                    JobName = "طالب",
                    NationalID ="12345678912345",
                    Name = "جون ماهر",
                    Nationality = "مصرى",
                    PersonID = 3
                }
            };
        }

        public List<vw_CaseDefectsData> GetDefectsByCaseID(int CaseID)
        {
            throw new NotImplementedException();
        }

        public bool IsPersonInCase(long personID, int caseID)
        {
            throw new NotImplementedException();
        }

        public bool IsPresenceSaved(int SessionID)
        {
            throw new NotImplementedException();
        }

        public SavePartSOrder SaveOrder(List<vw_CaseDefectData> CasePartylist)
        {
            throw new NotImplementedException();
        }

        public SaveDefectsStatus UpdatePresenceOfDefects(List<vw_CaseDefectsData> DefectsList, int SessionID)
        {
            throw new NotImplementedException();
        }

        public SaveDefectsStatus UpdatePresenceOfDefects(List<vw_CaseDefectsData> DefectsList, int SessionID, out vw_CaseDefectsData Defect)
        {
            throw new NotImplementedException();
        }

        public SaveDefectsStatus UpdatePresenceOfDefects(List<vw_CaseDefectsData> DefectsList, int SessionID, out List<string> Defect)
        {
            throw new NotImplementedException();
        }
    }
}