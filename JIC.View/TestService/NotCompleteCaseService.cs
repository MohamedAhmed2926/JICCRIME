using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class NotCompleteCaseService : INotCompleteCasesService
    {
        public bool DeleteNotCompleteCase(int CaseID)
        {
            return false;
        }

        public vw_unCompletCase GetNotComplateCaseByID(int CaseID)
        {
            vw_unCompletCase vw_UnCompletCase = new vw_unCompletCase();
            vw_UnCompletCase.CaseId = 1;
            vw_UnCompletCase.CaseTitle = "ااااا";
            vw_UnCompletCase.CrimName = "بببببب";
           
          //  vw_UnCompletCase.NotCompleteStatus = NotCompleteStatus.Defendent;

            return vw_UnCompletCase;

        }
 

        public List<vw_unCompletCase> GetNotCompleteCase(int? CourtId)
        {
            List<vw_unCompletCase> listUnCompletCase = new List<vw_unCompletCase>();
            vw_unCompletCase vw_UnCompletCase = new vw_unCompletCase();
            vw_UnCompletCase.CaseId = 1;
            vw_UnCompletCase.CaseTitle = "ااااا";
            vw_UnCompletCase.CrimName = "بببببتب";
            
          //  vw_UnCompletCase.NotCompleteStatus = NotCompleteStatus.Defendent;
            listUnCompletCase.Add(vw_UnCompletCase);
            vw_unCompletCase vw_UnCompletCase2 = new vw_unCompletCase();
            vw_UnCompletCase2.CaseId = 2;
            vw_UnCompletCase2.CaseTitle = "ااتتتت";
            vw_UnCompletCase2.CrimName = "بيببثسيقثيث";
             
        //    vw_UnCompletCase2.NotCompleteStatus = NotCompleteStatus.Document;
            listUnCompletCase.Add(vw_UnCompletCase2);
            vw_unCompletCase vw_UnCompletCase3 = new vw_unCompletCase();
            vw_UnCompletCase3.CaseId = 3;
            vw_UnCompletCase3.CaseTitle = "جغاغتغ";
            vw_UnCompletCase3.CrimName = "حغاغفغ";
          
//vw_UnCompletCase3.NotCompleteStatus = NotCompleteStatus.Defendent;
            listUnCompletCase.Add(vw_UnCompletCase3);
            return listUnCompletCase;
        }

        public IQueryable<vw_unCompletCase> GetNotCompleteCase(int CourtId)
        {
            throw new NotImplementedException();
        }
    }
}