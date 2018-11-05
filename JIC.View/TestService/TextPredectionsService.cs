using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class TextPredectionsService : ITextPredectionsService
    {
        public AddTextStatus AddTextPredections(vw_TextPredections TextPredections)
        {
            return AddTextStatus.FailedToAdd;
        }

        public bool DeleteTextByID(int TextID)
        {
            return false;
        }

        

        public EditTextStatus EditText(vw_TextPredections TextPredections)
        {
            return EditTextStatus.EditSuccefull;
        }

        

        public vw_TextPredections GetTextByID(int TextID)
        {
            vw_TextPredections vw_TextPredections = new vw_TextPredections();
          //  vw_TextPredections.CrimeTypeID =1;
         //   vw_TextPredections.CrimeTypeName ="efds";
            vw_TextPredections.TextID =54;
            vw_TextPredections.TextPredectionsDescription ="ddsfas";
            vw_TextPredections.TextTitle ="dsaffds";
            return vw_TextPredections;
        }

        public List<vw_TextPredections> GetTextPredections(int? CourtID)
        {
            List<vw_TextPredections> GetTextPredectionsList = new List<vw_TextPredections>();
            vw_TextPredections vw_TextPredections = new vw_TextPredections();
          //  vw_TextPredections.CrimeTypeID = 1;
          //  vw_TextPredections.CrimeTypeName = "efds";
            vw_TextPredections.TextID = 54;
            vw_TextPredections.TextPredectionsDescription = "ddsfas";
            vw_TextPredections.TextTitle = "dsaffds";
            GetTextPredectionsList.Add(vw_TextPredections);
            vw_TextPredections TextPredections = new vw_TextPredections();
        //    TextPredections.CrimeTypeID = 2;
        //    TextPredections.CrimeTypeName = "efds";
            TextPredections.TextID = 22;
            TextPredections.TextPredectionsDescription = "ddserefas";
            TextPredections.TextTitle = "dsafftreds";
            GetTextPredectionsList.Add(TextPredections);
            return GetTextPredectionsList;

        }

        public List<vw_TextPredections> GetTextPredections(int? userID, int CrimeTypeID)
        {
            throw new NotImplementedException();
        }

        public List<vw_TextPredections> GetTextPredections(List<vw_KeyValue> CircuitID)
        {
            throw new NotImplementedException();
        }
    }
}