using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.views;

namespace JIC.Crime.View.TestService
{
    public class ProsecutorService : IProsecutorService
    {
        public bool AddPerson(vw_PersonData PersonData, out int PersonID)
        {
            PersonID = 0;
            return true;
        }

        public ProsecutorStatus AddProsecutor(vw_ProcecuterData ProsecuterData, out int ProsecutorID)
        {
            ProsecutorID = 0;
            return ProsecutorStatus.Failed;
        }

        public ProsecutorStatus DeleteProsecutor(int ProsecutorID)
        {
            return ProsecutorStatus.Failed;
        }

        public ProsecutorStatus EditProsecutor(vw_ProcecuterData Prosecutor)
        {
            return ProsecutorStatus.Failed;
        }

        public List<vw_Prosecution> GetProsecutions(int? CourtID)
        {
            List<vw_Prosecution> vw_ProsecutionList = new List<vw_Prosecution>();
            vw_Prosecution s = new vw_Prosecution();
            s.ProsecutionName = "القاهرة";
            s.ID = 1;
            
            vw_ProsecutionList.Add(s);
            vw_Prosecution s1 = new vw_Prosecution();
            s1.ProsecutionName = "العباسية";
            s1.ID = 2;
            vw_ProsecutionList.Add(s1);
            //todo fill list
            return vw_ProsecutionList;
        }

        public List<vw_ProcecuterData> GetProsecutor(int? CourtID)
        {
            List<vw_ProcecuterData> vw_ProsecutorList = new List<vw_ProcecuterData>();

            vw_ProcecuterData v = new vw_ProcecuterData();
            v.ID = 3;
            v.ProsecutionName = "القاهرة";
            v.NationalID = "0000";
            v.ProcecutoerName = "احمد";
            v.ProcecutionID = 1;

            vw_ProsecutorList.Add(v);
            vw_ProcecuterData C = new vw_ProcecuterData();
            C.ID = 4;
            C.ProsecutionName = "العباسية";
            C.NationalID = "0000";
            C.ProcecutoerName = "محمد";
            C.ProcecutionID = 2;
            vw_ProsecutorList.Add(C);
            //todo fill list
            return vw_ProsecutorList;
        }

        public vw_ProcecuterData GetProsecutorByID(int? ProsecutorID)
        {

            vw_ProcecuterData C = new vw_ProcecuterData();
            C.ID = 4;
            C.ProsecutionName = "العباسية";
            C.NationalID = "0000";
            C.ProcecutoerName = "محمد";
            C.ProcecutionID = 2;
            
            return C;
        }
        
    }
}